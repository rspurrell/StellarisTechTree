namespace StellarisTechCore.Models
{
    public class Technology
    {
        /*
ai_update_type
ai_weight
feature_flags
gateway
icon
mod_weight_if_group_picked
potential
prereqfor_desc
weight_groups
         */

        public string? AIUpdateType { get; set; }
        public string? AIWeight { get; set; }
        public TechArea Area { get; set; }
        public TechCategory Category { get; set; }
        public int Cost { get; set; }
        public int? CostPerLevel { get; set; }
        public string? FeatureFlags { get; set; }
        public string? Gateway { get; set; }
        public string? Icon { get; set; }
        public string ID { get; set; }
        public bool IsDangerous { get; set; }
        public bool IsReverseEngineerable { get; set; }
        public bool IsRare { get; set; }
        public int? Levels { get; set; }
        public string? Modifier { get; set; }
        public string? ModWeightIfGroupPicked { get; set; }
        public string Name { get; set; }
        public string? Potential { get; set; }
        public string? PrereqForDesc { get; set; }
        public List<string>? Prerequisites { get; set; }
        public bool StartingTech { get; set; }
        public int Tier { get; set; }
        public int Weight { get; set; }
        public string? WeightGroups { get; set; }
        public WeightModifier? WeightModifier { get; set; }

        public Technology(string id, string name, TechArea area, TechCategory category, int cost, bool startingTech, int tier, int weight)
        {
            Area = area;
            Category = category;
            Cost = cost;
            ID = id;
            //IsDangerous = isDangerous;
            //IsReverseEngineerable = isReverseEngineerable;
            //IsRare = isRare;
            //Modifier = modifier;
            Name = name;
            StartingTech = startingTech;
            Tier = tier;
            Weight = weight;
        }

        public override string ToString()
        {
            return ID;
        }
    }
}
