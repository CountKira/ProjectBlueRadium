using System.Linq;
using System.Text;

namespace BusinessLogic
{
	public static class ItemCollectionToString
	{
		public static string GetItemNameConcat(ItemCollection items)
		{
			switch (items.Count())
			{
				case 0: return "";
				case 1: return $"a {items.First().Name}";
				case 2: return $"a {string.Join(" and a ", items.Select(i => i.Name))}";
				default:
				{
					return
						$"a {string.Join(", a ", items.Take(items.Count() - 1).Select(i => i.Name))} and a {items.Last().Name}";
				}
			}
		}
	}
}