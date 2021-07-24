namespace C3xPAWM.Models.Options
{
    public partial class NegoziOptions
    {
        public int PerPage { get; set; }

        public NegoziOrderOptions Order { get; set; }
    }

    public partial class NegoziOrderOptions{
        public string By { get; set; }

        public bool Ascending { get; set; }

        public string Allow { get; set; }
    }
}