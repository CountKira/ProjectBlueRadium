using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
	public static class ItemCollectionToString
	{
		public static string GetItemNameConcat(IEnumerable<Item> items)
		{
			var ar = items.ToArray();
			return ar.Length switch
			{
				0 => "",
				1 => $"a {ar[0].Name}",
				_ => $"a {string.Join(", a ", ar[..^1].Select(item => item.Name))} and a {ar[^1].Name}",
			};
		}
	}
}