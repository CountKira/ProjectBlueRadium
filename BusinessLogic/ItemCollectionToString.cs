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
					var sb = new StringBuilder("a ");
					foreach (var item in items.Take(items.Count() - 1))
					{
						sb.Append(item.Name);
						sb.Append(", a ");
					}

					sb.Remove(sb.Length - 4, 4);
					sb.Append(" and a ");
					sb.Append(items.Last().Name);
					return sb.ToString();
				}
			}
		}
	}
}