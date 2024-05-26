namespace Manero_Backoffice.Generics;

public class TableModel<TItem>
{
    public IEnumerable<TItem>? Items { get; set; } = [];
    public Dictionary<string, string> Columns { get; set; } = new Dictionary<string, string>();

}
