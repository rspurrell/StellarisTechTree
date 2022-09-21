using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StellarisTechCore.Models;

namespace StellarisTechCore.Common
{
    internal static class Extensions
    {
        public static string ToString(this TechCategory category)
        {
            return category switch
            {
                TechCategory.Computing => "computing",
                TechCategory.FieldManipulation => "field_manipulation",
                TechCategory.Particles => "particles",
                TechCategory.Biology => "biology",
                TechCategory.MilitaryTheory => "military_theory",
                TechCategory.NewWorlds => "new_worlds",
                TechCategory.Psionics => "psionics",
                TechCategory.Statecraft => "statecraft",
                TechCategory.Industry => "industry",
                TechCategory.Materials => "materials",
                TechCategory.Propulsion => "propulsion",
                TechCategory.Voidcraft => "voidcraft",
                _ => throw new ArgumentException("Unknown category", nameof(category)),
            };
        }
    }
}
