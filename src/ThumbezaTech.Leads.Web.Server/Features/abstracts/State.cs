namespace ThumbezaTech.Leads.Web.Server.Features.abstracts;
internal abstract class State<TViewModel>
  where TViewModel : class
{
  private readonly List<TViewModel> _items = new();

  public IReadOnlyCollection<TViewModel> Items => _items;

  public void SetStateItems(ICollection<TViewModel> items)
  {
    _items.Clear();
    _items.AddRange(items);
  }
}
