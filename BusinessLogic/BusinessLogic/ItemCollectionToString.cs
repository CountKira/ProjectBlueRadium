using System.Linq;
using System.Text;

namespace BusinessLogic
{
	public static class ItemCollectionToString
	{
		public static string GetItemNameConcat(ItemCollection items)
		{
			return items.Count() switch
			{
				0 => "",
				1 => $"a {items.First().Name}",
				2 => $"a {string.Join(" and a ", items.Select(i => i.Name))}",
				_ => $"a {string.Join(", a ", items.Take(items.Count() - 1).Select(i => i.Name))} and a {items.Last().Name}",
			};
		}
	}
}