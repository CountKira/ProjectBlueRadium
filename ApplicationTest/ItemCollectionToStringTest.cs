using BusinessLogic;
using BusinessLogic.Items;
using Xunit;

namespace ApplicationTest
{
	public class ItemCollectionToStringTest
	{
		[Fact]
		public void EmptyItems()
		{
			var itemCollection = new ItemCollection();
			var text = ItemCollectionToString.GetItemNameConcat(itemCollection);
			Assert.Equal("", text);
		}

		[Fact]
		public void SingleItem()
		{
			var itemCollection = new ItemCollection()
			{
				new DummyItem("Thing", "TestThing")
			};
			var text = ItemCollectionToString.GetItemNameConcat(itemCollection);
			Assert.Equal("a Thing", text);
		}

		[Fact]
		public void TwoItems()
		{
			var itemCollection = new ItemCollection()
			{
				new DummyItem("Thing", "TestThing"),
				new DummyItem("Thing2", "TestThing"),
			};
			var text = ItemCollectionToString.GetItemNameConcat(itemCollection);
			Assert.Equal("a Thing and a Thing2", text);
		}

		[Fact]
		public void ThreeOrMoreItems()
		{
			var itemCollection = new ItemCollection()
			{
				new DummyItem("Thing", "TestThing"),
				new DummyItem("Thing2", "TestThing"),
				new DummyItem("Thing3", "TestThing"),
			};
			var text = ItemCollectionToString.GetItemNameConcat(itemCollection);
			Assert.Equal("a Thing, a Thing2 and a Thing3", text);
		}

		private class DummyItem : Item
		{
			/// <inheritdoc />
			public DummyItem(string name, string description) : base(name, description)
			{
			}

			/// <inheritdoc />
			public override void Act(Verb verb, IGame game)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
