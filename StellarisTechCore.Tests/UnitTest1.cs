using StellarisTechCore.Configuration;

namespace StellarisTechCore.Tests
{
    [TestClass]
    public class UnitTest1
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static Dictionary<string, int> SUBSTITUTIONS;
        private static Regex TECH_COUNTER = new Regex(@"^\w+", RegexOptions.Multiline);

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        [TestInitialize]
        public void Init()
        {
            SUBSTITUTIONS = new Dictionary<string, int>()
            {
                { "@tier0cost1", 1 },
            //    { "@tier1cost1", 11 },
            //    { "@tier1cost2", 12 },
            //    { "@tier1cost3", 13 },
            //    { "@tier2cost1", 21 },
            //    { "@tier2cost2", 22 },
            //    { "@tier2cost3", 23 },
            //    { "@tier3cost1", 31 },
            //    { "@tier3cost2", 32 },
            //    { "@tier3cost3", 33 },
            //    { "@tier4cost1", 41 },
            //    { "@tier4cost2", 42 },
            //    { "@tier4cost3", 43 },
            //    { "@tier5cost1", 51 },
            //    { "@tier5cost2", 52 },
            //    { "@tier5cost3", 53 },

            //    { "@tier1weight1", 11 },
            //    { "@tier1weight2", 12 },
            //    { "@tier1weight3", 13 },
            //    { "@tier2weight1", 21 },
            //    { "@tier2weight2", 22 },
            //    { "@tier2weight3", 23 },
            //    { "@tier3weight1", 31 },
            //    { "@tier3weight2", 32 },
            //    { "@tier3weight3", 33 },
            //    { "@tier4weight1", 41 },
            //    { "@tier4weight2", 42 },
            //    { "@tier4weight3", 43 },
            //    { "@tier5weight1", 51 },
            //    { "@tier5weight2", 52 },
            //    { "@tier5weight3", 53 },

                { "@repeatableTechBaseCost", -1 },
            //    { "@repatableTechFactor", -1 }, // yes, "repatable"
            //    { "@repeatableTechFactor", -1 }, // incase they fix the spelling
            //    { "@repeatableTechLevelCost", -1 },
                { "@repeatableTechTier", 6 },
                { "@repeatableTechWeight", -1 },

                { "@fallentechcost", 66 },
                { "@fallentechtier", 6 },

                { "@horizontechcost1", 66 },
                { "@horizontechcost2", 66 },
                { "@horizontechtier", 6 },

                { "@guardiantechcost", 66 },
                { "@guardiantechtier", 6 },
            };
        }

        [TestMethod]
        public void IntegrationTest()
        {
            var parser = new ConfigurationParser(StellarisCoreSettings.Settings.TechRegex, SUBSTITUTIONS);
            parser.ParseSetVariables(File.ReadAllText($"{StellarisCoreSettings.Settings.VariablesPath}"));
            var techs = new List<Technology>();
            var partitionedTechs = new Dictionary<string, List<Technology>>();
            foreach (StellarisConfigFile file in StellarisCoreSettings.Settings.TechFiles)
            {
                var techData = File.ReadAllText($"{StellarisCoreSettings.Settings.TechPath}{file.FileName}");
                var parsedTechs = parser.ParseTechnologies(techData);
                Assert.AreEqual(TECH_COUNTER.Matches(techData).Count, parsedTechs.Count);
                techs.AddRange(parsedTechs);
                partitionedTechs[file.Tag] = parsedTechs;
            }

            //bool area = false;
            //bool category = false;
            //bool cost = false;
            bool prerequisites = false;
            //bool start_tech = false;
            //bool tier = false;
            //bool weight = false;

            bool ai_update_type = false;
            bool ai_weight = false;
            bool feature_flags = false;
            bool gateway = false;
            bool icon = false;
            bool is_dangerous = false;
            bool is_rare = false;
            bool is_reverse_engineerable = false;
            bool mod_weight_if_group_picked = false;
            bool modifier = false;
            bool potential = false;
            bool prereqfor_desc = false;
            bool weight_groups = false;
            bool weight_modifier = false;

            TechCategory currentCategory = (TechCategory)(-1);
            foreach (var t in techs.OrderBy(t => t.Area).ThenBy(t => t.Category).ThenBy(t => t.ID))
            {
                if (!string.IsNullOrEmpty(t.AIUpdateType))
                {
                    ai_update_type = true;
                }
                if (!string.IsNullOrEmpty(t.AIWeight))
                {
                    ai_weight = true;
                }
                if (!string.IsNullOrEmpty(t.FeatureFlags))
                {
                    feature_flags = true;
                }
                if (!string.IsNullOrEmpty(t.Gateway))
                {
                    gateway = true;
                }
                if (!string.IsNullOrEmpty(t.Icon))
                {
                    icon = true;
                }
                if (t.IsDangerous)
                {
                    is_dangerous = true;
                }
                if (t.IsRare)
                {
                    is_rare = true;
                }
                if (t.IsReverseEngineerable)
                {
                    is_reverse_engineerable = true;
                }
                if (!string.IsNullOrEmpty(t.ModWeightIfGroupPicked))
                {
                    mod_weight_if_group_picked = true;
                }
                if (!string.IsNullOrEmpty(t.Modifier))
                {
                    modifier = true;
                }
                if (!string.IsNullOrEmpty(t.Potential))
                {
                    potential = true;
                }
                if (!string.IsNullOrEmpty(t.PrereqForDesc))
                {
                    prereqfor_desc = true;
                }
                if (t.Prerequisites!= null)
                {
                    prerequisites = true;
                }
                if (!string.IsNullOrEmpty(t.WeightGroups))
                {
                    weight_groups = true;
                }
                if (t.WeightModifier != null)
                {
                    weight_modifier = true;
                }

                if (t.Category != currentCategory)
                {
                    currentCategory = t.Category;
                    Console.WriteLine(t.Category.ToString());
                }

                Console.WriteLine($"\t{t.ID}{(t.Levels != null ? $": Repeatable {t.Levels}" : "")}");
            }

            Assert.IsTrue(ai_update_type);
            Assert.IsTrue(ai_weight);
            Assert.IsTrue(feature_flags);
            Assert.IsTrue(gateway);
            Assert.IsTrue(icon);
            Assert.IsTrue(is_dangerous);
            Assert.IsTrue(is_rare);
            Assert.IsTrue(is_reverse_engineerable);
            Assert.IsTrue(mod_weight_if_group_picked);
            Assert.IsTrue(modifier);
            Assert.IsTrue(potential);
            Assert.IsTrue(prereqfor_desc);
            Assert.IsTrue(prerequisites);
            Assert.IsTrue(weight_groups);
            Assert.IsTrue(weight_modifier);
        }
    }
}