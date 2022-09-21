namespace StellarisTechCore.Models
{
    public class WeightModifier
    {
        private readonly string _value;
        public double Factor { get; set; }
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();

        public WeightModifier(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
