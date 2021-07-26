namespace C3xPAWM.Models.Options
{
    public class ElencoOptions
    {
        public int PerPage { get; set; }

        public ElencoOrderOptions Order { get; set; }
    }

    public class ElencoOrderOptions{
        public string By { get; set; }

        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}