namespace C3xPAWM.Models.ViewModel
{
    public interface IPagination
    {
        string Search { get; }
        int TotalResults { get; }
        int Page { get; }
        string OrderBy { get; }
        bool Ascending { get; }
        int Limit { get; }

    }
}