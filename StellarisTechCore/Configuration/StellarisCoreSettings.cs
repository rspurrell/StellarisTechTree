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

        public string TechPath { get; set; }
        public string TechRegex { get; set; }
        public List<StellarisConfigFile> TechFiles { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StellarisCoreSettings() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    public class StellarisConfigFile
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Tag { get; set; }
        public string FileName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
