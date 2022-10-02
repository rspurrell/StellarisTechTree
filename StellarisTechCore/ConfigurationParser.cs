namespace StellarisTechCore
{
    using StellarisTechCore.Common;
    using StellarisTechCore.Models;
    using System.Text.RegularExpressions;
    using static StellarisTechCore.Common.Constants;

    public class ConfigurationParser
    {
        private static readonly Regex _emptyLineOrCommentRegex = new(@"\s*(?:#.*)?\r?$", RegexOptions.Multiline);
        private static readonly Regex _variablesRegex = new(@"^(?<key>@\w+) = (?<value>\w+)\r?$\n", RegexOptions.Multiline);

        private readonly Dictionary<string, int?> _variables;
        private readonly Regex _techRegex;

        public ConfigurationParser(string techRegexPattern)
            : this(techRegexPattern, null)
        { }

        public ConfigurationParser(string techRegexPattern, IReadOnlyDictionary<string, int>? defaultVariables)
        {
            _techRegex = new Regex(techRegexPattern, RegexOptions.Multiline);
            _variables = defaultVariables?.ToDictionary(kvp => kvp.Key, kvp => (int?)kvp.Value) ?? new Dictionary<string, int?>();
        }

        /// <summary>
        /// <para>Parses and sets global variable substitutions for use when parsing technologies. Variables should be supplied before parsing technologies.</para>
        /// </summary>
        /// <param name="value">The raw text of variables from a Stellaris configuration file.</param>
        public void ParseSetVariables(string value)
        {
            MatchCollection matches = _variablesRegex.Matches(value);
            if (matches.Count == 0)
                return;
            SetVariables(matches, _variables);
        }

        private static IReadOnlyDictionary<string, int?> ParseOverrides(string value, IReadOnlyDictionary<string, int?> variables)
        {
            MatchCollection matches = _variablesRegex.Matches(value);
            if (matches.Count == 0)
                return variables;
            var results = variables?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) ?? new Dictionary<string, int?>();
            SetVariables(matches, results);
            return results;
        }

        private static void SetVariables(MatchCollection matches, Dictionary<string, int?> variables)
        {
            foreach (Match m in matches)
            {
                var k = m.Groups["key"].Value;
                var v = m.Groups["value"].Value;
                variables[k] = string.IsNullOrEmpty(v) ? null : int.Parse(v);
            }
        }

        /// <summary>
        /// Parses technologies from a Stellaris configuration file.
        /// </summary>
        /// <param name="value">The raw technology text from a Stellaris configuration file.</param>
        /// <returns>A list of technologies successfully parsed from value.</returns>
        public List<Technology> ParseTechnologies(string value)
        {
            // clean-up and normalize
            value = _emptyLineOrCommentRegex
                .Replace(value.Replace("\r\n", "\n"), string.Empty)
                .Replace("\n", "\r\n");

            var localOverrides = ParseOverrides(value, _variables);
            return _techRegex.Matches(value)
                .Select(m => ParseTechnology(m.Groups, localOverrides))
                .ToList();
        }

        private static Technology ParseTechnology(GroupCollection groups, IReadOnlyDictionary<string, int?> variables)
        {
            var tier = ParseAndSubstitute<int>(groups[TechRegexGroup.TIER].Value, variables);
            var cost = ParseAndSubstitute<int>(groups[TechRegexGroup.COST].Value, variables);
            var levels = ParseAndSubstitute<int?>(groups[TechRegexGroup.LEVELS].Value, variables);
            var weight = ParseAndSubstitute<int>(groups[TechRegexGroup.WEIGHT].Value, variables);
            var prereqs = ParsePrereqs(groups[TechRegexGroup.PREREQUISITES].Value);
            var wm = ParseWeightModifier(groups[TechRegexGroup.WEIGHT_MODIFIER].Value);

            return new Technology(
                groups[TechRegexGroup.ID].Value,
                groups[TechRegexGroup.ID].Value,
                Utilities.GetArea(groups[TechRegexGroup.AREA].Value),
                Utilities.GetCategory(groups[TechRegexGroup.CATEGORY].Value),
                cost,
                groups[TechRegexGroup.START_TECH].Value == "yes",
                tier,
                weight)
            {
                AIUpdateType = groups[TechRegexGroup.AI_UPDATE_TYPE].Value,
                AIWeight = groups[TechRegexGroup.AI_WEIGHT].Value,
                FeatureFlags = groups[TechRegexGroup.FEATURE_FLAGS].Value,
                Gateway = groups[TechRegexGroup.GATEWAY].Value,
                Icon = groups[TechRegexGroup.ICON].Value,
                IsDangerous = groups[TechRegexGroup.IS_DANGEROUS].Value == "yes",
                IsRare = groups[TechRegexGroup.IS_RARE].Value == "yes",
                IsReverseEngineerable = groups[TechRegexGroup.IS_REVERSE_ENGINEERABLE].Value == "yes",
                Levels = levels,
                Modifier = groups[TechRegexGroup.MODIFIER].Value,
                ModWeightIfGroupPicked = groups[TechRegexGroup.MOD_WEIGHT_IF_GROUP_PICKED].Value,
                Potential = groups[TechRegexGroup.POTENTIAL].Value,
                PrereqForDesc = groups[TechRegexGroup.PREREQFOR_DESC].Value,
                Prerequisites = prereqs,
                WeightGroups = groups[TechRegexGroup.WEIGHT_GROUPS].Value,
                WeightModifier = wm
            };
        }

        private static TResult? ParseAndSubstitute<TResult>(string value, IReadOnlyDictionary<string, int?> variables)
        {
            var t = typeof(TResult);
            if (value.StartsWith("@"))
            {
                if (!variables.ContainsKey(value))
                {
                    throw new ArgumentException($"No substitution variable found for {value}. Provide variables by calling {nameof(ParseSetVariables)} before parsing technologies.", nameof(value));
                }
                return (TResult?)Convert.ChangeType(variables[value], Nullable.GetUnderlyingType(t) ?? t);
            }
            if (!string.IsNullOrEmpty(value))
            {
                return (TResult)Convert.ChangeType(value, Nullable.GetUnderlyingType(t) ?? t);
            }
            return default;
        }

        private static List<string>? ParsePrereqs(string sPrereqs)
        {
            List<string>? prereqs = null;
            if (!string.IsNullOrEmpty(sPrereqs))
            {
                prereqs = new List<string>();
                foreach (var v in sPrereqs.Trim().TrimStart('"').TrimEnd('"').Split("\" \""))
                {
                    prereqs.Add(v);
                }
            }
            return prereqs;
        }

        private static WeightModifier? ParseWeightModifier(string sWeightModifier)
        {
            WeightModifier? wm = null;
            if (!string.IsNullOrEmpty(sWeightModifier))
            {
                wm = new WeightModifier(sWeightModifier);
            }
            return wm;
        }
    }
}
