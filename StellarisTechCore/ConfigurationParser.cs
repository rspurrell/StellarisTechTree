namespace StellarisTechCore
{
    using StellarisTechCore.Common;
    using StellarisTechCore.Models;
    using System.Text.RegularExpressions;
    using static StellarisTechCore.Common.Constants;

    public class ConfigurationParser
    {
        private static readonly Regex _commentRegex = new Regex(@"\s*#.+\r?$\n", RegexOptions.Multiline);
        private static readonly Regex _emptyLinesRegex = new Regex(@"^\s*\r?$\n", RegexOptions.Multiline);
        private readonly Regex _techRegex;

        public ConfigurationParser(string techRegexPattern)
        {
            _techRegex = new Regex(techRegexPattern, RegexOptions.Multiline);
        }

        public List<Technology> ParseTechnologies(string value, Dictionary<string, string> substitutions)
        {
            value = value.Replace("\r\n", "\n");
            value = _commentRegex.Replace(value, "\n");
            value = _emptyLinesRegex.Replace(value, string.Empty);
            value = value.Replace("\n", "\r\n");

            var matches = _techRegex.Matches(value);
            var technologies = new List<Technology>();
            foreach (Match m in matches)
            {
                technologies.Add(ParseTechnology(m.Groups, substitutions));
            }
            return technologies;
        }

        private static Technology ParseTechnology(GroupCollection groups, Dictionary<string, string> substitutions)
        {
            int iTier = ParseAndSubstitute<int>(groups[TechProperty.TIER].Value, substitutions);
            int iCost = ParseAndSubstitute<int>(groups[TechProperty.COST].Value, substitutions);
            int? iLevels = ParseAndSubstitute<int?>(groups[TechProperty.LEVELS].Value, substitutions);
            int iWeight = ParseAndSubstitute<int>(groups[TechProperty.WEIGHT].Value, substitutions);
            Dictionary<string, Technology>? prereqs = ParsePrereqs(groups[TechProperty.PREREQUISITES].Value);
            WeightModifier? wm = ParseWeightModifier(groups[TechProperty.WEIGHT_MODIFIER].Value);

            return new Technology(
                groups[TechProperty.ID].Value,
                groups[TechProperty.ID].Value,
                Utilities.GetArea(groups[TechProperty.AREA].Value),
                Utilities.GetCategory(groups[TechProperty.CATEGORY].Value),
                iCost,
                groups[TechProperty.START_TECH].Value == "yes",
                iTier,
                iWeight)
            {
                AIUpdateType = groups[TechProperty.AI_UPDATE_TYPE].Value,
                AIWeight = groups[TechProperty.AI_WEIGHT].Value,
                FeatureFlags = groups[TechProperty.FEATURE_FLAGS].Value,
                Gateway = groups[TechProperty.GATEWAY].Value,
                Icon = groups[TechProperty.ICON].Value,
                IsDangerous = groups[TechProperty.IS_DANGEROUS].Value == "yes",
                IsRare = groups[TechProperty.IS_RARE].Value == "yes",
                IsReverseEngineerable = groups[TechProperty.IS_REVERSE_ENGINEERABLE].Value == "yes",
                Levels = iLevels,
                Modifier = groups[TechProperty.MODIFIER].Value,
                ModWeightIfGroupPicked = groups[TechProperty.MOD_WEIGHT_IF_GROUP_PICKED].Value,
                Potential = groups[TechProperty.POTENTIAL].Value,
                PrereqForDesc = groups[TechProperty.PREREQFOR_DESC].Value,
                Prerequisites = prereqs,
                WeightGroups = groups[TechProperty.WEIGHT_GROUPS].Value,
                WeightModifier = wm
            };
        }

        public static TResult? ParseAndSubstitute<TResult>(string value, Dictionary<string, string> substitutions, TResult? defaultValue = default(TResult))
        {
            var t = typeof(TResult);
            if (value.StartsWith("@"))
            {
                if (!substitutions.ContainsKey(value))
                {
                    throw new ArgumentException($"No substitution found for {value}", nameof(value));
                }
                value = substitutions[value];
            }
            if (!string.IsNullOrEmpty(value))
            {
                return (TResult)Convert.ChangeType(value, Nullable.GetUnderlyingType(t) ?? t);
            }
            return defaultValue;
        }

        private static Dictionary<string, Technology>? ParsePrereqs(string sPrereqs)
        {
            Dictionary<string, Technology>? prereqs = null;
            if (!string.IsNullOrEmpty(sPrereqs))
            {
                prereqs = new Dictionary<string, Technology>();
                foreach (var v in sPrereqs.Trim().TrimStart('"').TrimEnd('"').Split("\" \""))
                {
                    prereqs[v] = null; // TODO: relate prereq technologies
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
