namespace C3xPAWM.Models.ViewModel
{
    public interface IPagination
    {
        string Search { get; }
        string Luogo { get; }
        int TotalResults { get; }
        int Page { get; }
        string OrderBy { get; }
        bool Ascending { get; }
        int Limit { get; }
        bool Tipologia { get; }
        bool Citta { get;  }
        bool Nome { get;  }

        bool Paginare {get;}
    }
}