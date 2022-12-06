namespace StellarisTechCore.Configuration
{
    using Microsoft.Extensions.Configuration;

    public sealed class StellarisCoreSettings
    {
        private static StellarisCoreSettings _settings;
        public static StellarisCoreSettings Settings => _settings;

        static StellarisCoreSettings()
        {
            var configSection = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", false, true)
                .Build()
                .GetRequiredSection(nameof(StellarisCoreSettings));
            _settings = configSection.Get<StellarisCoreSettings>();

            //configSection.GetReloadToken().RegisterChangeCallback(o =>
            //{
            //    _settings = configSection.Get<StellarisCoreSettings>();
            //}, _settings);
        }
        public string RootPath { get; set; }

        private string _techLocalizationFilesPath;
        public string TechLocalizationFilesPath
        {
            get => string.Concat(RootPath, _techLocalizationFilesPath);
            set => _techLocalizationFilesPath = value;
        }

        public List<StellarisConfigFile> TechFiles { get; set; }

        private string _techPath;
        public string TechPath
        {
            get => string.Concat(RootPath, _techPath);
            set => _techPath = value;
        }

        public string TechRegex { get; set; }

        private string _variablesPath;
        public string VariablesPath
        {
            get => string.Concat(RootPath, _variablesPath);
            set => _variablesPath = value;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StellarisCoreSettings() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    public sealed class StellarisConfigFile
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Tag { get; set; }
        public string FileName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
