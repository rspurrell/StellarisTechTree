namespace StellarisTechCore.Common
{
    internal static class Constants
    {
        public static class TechRegexGroup
        {
            public const string ID = "id";
            public const string AI_UPDATE_TYPE = "ai_update_type";
            public const string AI_WEIGHT = "ai_weight";
            public const string AREA = "area";
            public const string CATEGORY = "category";
            public const string COST = "cost";
            public const string COST_PER_LEVEL = "cost_per_level";
            public const string FEATURE_FLAGS = "feature_flags";
            public const string GATEWAY = "gateway";
            public const string ICON = "icon";
            public const string IS_DANGEROUS = "is_dangerous";
            public const string IS_RARE = "is_rare";
            public const string IS_REVERSE_ENGINEERABLE = "is_reverse_engineerable";
            public const string LEVELS = "levels";
            public const string MOD_WEIGHT_IF_GROUP_PICKED = "mod_weight_if_group_picked";
            public const string MODIFIER = "modifier";
            public const string POTENTIAL = "potential";
            public const string PREREQFOR_DESC = "prereqfor_desc";
            public const string PREREQUISITES = "prerequisites";
            public const string START_TECH = "start_tech";
            public const string TIER = "tier";
            public const string WEIGHT = "weight";
            public const string WEIGHT_GROUPS = "weight_groups";
            public const string WEIGHT_MODIFIER = "weight_modifier";
        }

        public static class TechVariableRegexGroup
        {
            public const string KEY = "key";
            public const string VALUE = "value";
        }

        public static class TechLocalizationRegexGroup
        {
            public const string ID = "id";
            public const string NAME = "name";
        }

        public static class RegexPattern
        {
            public const string EMPTY_LINE_OR_COMMENT = @"\s*(?:#.*)?\r?$";
            public const string LOCALIZATIONS = $@"^ ?(?<{TechLocalizationRegexGroup.ID}>(?:(?!(?:_[Dd][Ee][Ss][Cc][:_])|(?:_[Ee][Ff][Ff][Ee][Cc][Tt]:))\w)+):\d ""(?<{TechLocalizationRegexGroup.NAME}>[\w .,%$:\-]+)""";
            public const string VARIABLES = $@"^(?<{TechVariableRegexGroup.KEY}>@\w+) = (?<{TechVariableRegexGroup.VALUE}>\w+)\r?$\n";
        }
    }
}
