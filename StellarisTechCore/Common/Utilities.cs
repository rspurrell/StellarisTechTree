using StellarisTechCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisTechCore.Common
{
    internal static class Utilities
    {
        public static TechCategory GetCategory(string category)
        {
            return category switch
            {
                "computing" => TechCategory.Computing,
                "field_manipulation" => TechCategory.FieldManipulation,
                "particles" => TechCategory.Particles,
                "biology" => TechCategory.Biology,
                "military_theory" => TechCategory.MilitaryTheory,
                "new_worlds" => TechCategory.NewWorlds,
                "psionics" => TechCategory.Psionics,
                "statecraft" => TechCategory.Statecraft,
                "industry" => TechCategory.Industry,
                "materials" => TechCategory.Materials,
                "propulsion" => TechCategory.Propulsion,
                "voidcraft" => TechCategory.Voidcraft,
                _ => throw new ArgumentException("Unknown technology category", nameof(category)),
            };
        }

        public static TechArea GetArea(string area)
        {
            return area switch
            {
                "engineering" => TechArea.Engineering,
                "physics" => TechArea.Physics,
                "society" => TechArea.Society,
                _ => throw new ArgumentException("Unknown technology area", nameof(area)),
            };
        }
    }
}
