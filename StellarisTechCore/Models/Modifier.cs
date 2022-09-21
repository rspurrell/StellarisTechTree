namespace StellarisTechCore.Models
{
    public class Modifier
    {
        public double Factor { get; set; }
        public List<Condition>? Or { get; set; }
        public List<Condition>? Nor { get; set; }
        public List<Condition>? Not { get; set; }
    }
}
