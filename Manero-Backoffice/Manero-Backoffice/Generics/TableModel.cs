namespace Manero_Backoffice.Generics;

public class TableModel<TItem>
{
    public List<TItem>? Items { get; set; } = new List<TItem>();
    public Dictionary<string, string> Columns { get; set; } = new Dictionary<string, string>();
}

