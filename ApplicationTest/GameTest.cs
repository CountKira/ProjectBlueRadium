﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using SharedTestingResources;
using SharedTestingResources.Items;
using Xunit;

namespace ApplicationTest
{
	public class GameTest
	{
		private class ItemComparer : IEqualityComparer<Item>
		{
			/// <inheritdoc />
			public bool Equals(Item x, Item y) => x.Name == y.Name;

			/// <inheritdoc />
			public int GetHashCode(Item obj) => obj.GetHashCode();
		}

		private class PassageComparer : IEqualityComparer<Passage>
		{
			/// <inheritdoc />
			public bool Equals(Passage x, Passage y) => x.RoomGuid == y.RoomGuid;

			/// <inheritdoc />
			public int GetHashCode(Passage obj) => obj.GetHashCode();
		}

		private class CreatureComparer : IEqualityComparer<Creature>
		{
			/// <inheritdoc />
			public bool Equals(Creature x, Creature y) => x.Name == y.Name &&
			                                              x.Description == y.Description;

			/// <inheritdoc />
			public int GetHashCode(Creature obj) => obj.GetHashCode();
		}

		private static (Game game, TestWriter writer) GetCommonGame()
		{
			var testWriter = new TestWriter();
			var game = new Game(testWriter, new TestingRoomRepository());
			return (game, testWriter);
		}

		private const string RoomDescription = "You are in an empty room. The walls are smooth.";

		public static IEnumerable<object[]> LookAtItemOnFloorData =>
			new List<object[]>
			{
				new object[] {new[] {"look bottle"}, "This is a glass bottle, with a green substance inside it."},
				new object[] {new[] {"look book"}, "The book contains the story of boatmurdered."},
				new object[] {new[] {"go west", "look sword"}, "A sharp sword."}
			};

		[Theory]
		[MemberData(nameof(LookAtItemOnFloorData))]
		public void LookAtItemOnFloor(string[] commands, string expectedResult)
		{
			var (game, testWriter) = GetCommonGame();
			foreach (var command in commands) game.EnterCommand(command);
			var result = testWriter.TextOutput;
			Assert.Equal(expectedResult, result);
		}

		public static IEnumerable<object[]> GetItemOnFloorData =>
			new List<object[]>
			{
				new object[] {new[] {"get bottle"}, "bottle"},
				new object[] {new[] {"get book"}, "book"},
				new object[] {new[] {"go west", "get sword"}, "sword"}
			};


		[Theory]
		[MemberData(nameof(GetItemOnFloorData))]
		public void GetItemOnFloor(string[] commands, string expectedItemName)
		{
			var (game, testWriter) = GetCommonGame();
			foreach (var command in commands) game.EnterCommand(command);
			var result = testWriter.Action;
			Assert.Equal(Verb.Get, result.Verb);
			Assert.Equal(expectedItemName, result.Specifier);
		}

		[Fact]
		public void AttackingTheBadEvilGuyWithoutAnythingEndsTheGame()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go north");
			game.EnterCommand("attack evil guy");
			var actual = testWriter.TextOutput;
			Assert.Equal("Since you do not wield any weapons, the evil guy can easily kill you.", actual);
			Assert.False(game.IsRunning);
		}

		[Fact]
		public void CanNotReadNonExistingItem()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go north");
			game.EnterCommand("read fireball spell book");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.ItemNotFound, result.CommandType);
			Assert.Equal("fireball spell book", result.Specifier);
		}

		[Fact]
		public void DrinkBottle()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("drink bottle");
			Assert.True(testWriter.DiedPyPoison);
			Assert.False(game.IsRunning);
		}

		[Fact]
		public void EmptySpellBook()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("spells");
			Assert.False(testWriter.SpellKnown);
		}

		[Fact]
		public void Exit()
		{
			var (game, _) = GetCommonGame();
			game.EnterCommand("exit");
			Assert.False(game.IsRunning);
		}

		[Fact]
		public void GetBookAgain()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get book");
			game.EnterCommand("get book");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.ItemNotFound, result.CommandType);
			Assert.Equal("book", result.Specifier);
		}

		[Fact]
		public void GetBottleAgain()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get bottle");
			game.EnterCommand("get bottle");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.ItemNotFound, result.CommandType);
			Assert.Equal("bottle", result.Specifier);
		}

		[Fact]
		public void GetBottleLowerCase()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get bottle");
			var result = testWriter.Action;
			Assert.Equal(Verb.Get, result.Verb);
			Assert.Equal("bottle", result.Specifier);
		}

		[Fact]
		public void GetBottleUpperCase()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get Bottle");
			var result = testWriter.Action;
			Assert.Equal(Verb.Get, result.Verb);
			Assert.Equal("bottle", result.Specifier);
		}

		[Fact]
		public void GoingIntoANonExistingRoom()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go saberwook");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.PassageNotFound, result.CommandType);
			Assert.Equal("saberwook", result.Specifier);
		}

		[Fact]
		public void GoingNorthAndGettingDescription()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go north");
			game.EnterCommand("look");
			var result = testWriter.SeenThings;
			Assert.Equal("You are in a dark room.", result.EntityDescription);
			var items = new ItemCollection();
			Assert.Equal(items, result.Items, new ItemComparer());
			var passages = new[]
			{
				new Passage(0, "south")
			};
			Assert.Equal(passages, result.Passages, new PassageComparer());
			var creatures = new[]
			{
				new Creature("Evil guy", "The evil threat of the campaign.")
			};
			Assert.Equal(creatures, result.Creatures, new CreatureComparer());
		}

		[Fact]
		public void GoingWestAndGettingDescription()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go west");
			game.EnterCommand("look");
			var result = testWriter.SeenThings;
			Assert.Equal("You are in a bright room.", result.EntityDescription);
			var items = new ItemCollection
			{
				new Sword()
			};
			Assert.Equal(items, result.Items, new ItemComparer());
			var passages = new[]
			{
				new Passage(0, "east")
			};
			Assert.Equal(passages, result.Passages, new PassageComparer());
		}

		[Fact]
		public void GoingWestAndRightBackTheLooking()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go west");
			game.EnterCommand("go east");
			game.EnterCommand("look");
			var result = testWriter.SeenThings;
			Assert.Equal(RoomDescription, result.EntityDescription);
			var items = new ItemCollection
			{
				new Bottle(),
				new Book(),
				new FireballSpellBook()
			};
			Assert.Equal(items, result.Items, new ItemComparer());
			var passages = new[]
			{
				new Passage(1, "north"),
				new Passage(2, "west")
			};
			Assert.Equal(passages, result.Passages, new PassageComparer());
		}

		[Fact]
		public void Inventory()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get bottle");
			game.EnterCommand("get book");
			game.EnterCommand("inventory");
			var items = new ItemCollection
			{
				new Bottle(),
				new Book()
			};
			var actual = testWriter.Inventory;
			Assert.Equal(items, actual, new ItemComparer());
		}

		[Fact]
		public void Look()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("look");
			var result = testWriter.SeenThings;
			Assert.Equal(RoomDescription, result.EntityDescription);
			var items = new ItemCollection
			{
				new Bottle(),
				new Book(),
				new FireballSpellBook()
			};
			Assert.Equal(items, result.Items, new ItemComparer());
			var passages = new[]
			{
				new Passage(1, "north"),
				new Passage(2, "west")
			};
			Assert.Equal(passages, result.Passages, new PassageComparer());
		}

		[Fact]
		public void LookAtNonExistingItem()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("look sasquatchIsMyFather");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.ItemNotFound, result.CommandType);
			Assert.Equal("sasquatchismyfather", result.Specifier);
		}

		[Fact]
		public void LookAtPickedUpItem()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get book");
			game.EnterCommand("look book");
			var result = testWriter.TextOutput;
			Assert.Equal("The book contains the story of boatmurdered.", result);
		}


		[Fact]
		public void LookEnemy()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go north");
			game.EnterCommand("look evil guy");
			var result = testWriter.TextOutput;
			Assert.Equal("The evil threat of the campaign.", result);
		}

		[Fact]
		public void Me()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("me");
			var result = testWriter.Me;
			Assert.Equal("It is you.", result);
		}

		[Fact]
		public void NothingEquipped()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("equipment");
			var actual = testWriter.HasNothingEquipped;
			Assert.True(actual);
		}

		[Fact]
		public void ReadSpellBook()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("read fireball spell book");
			Assert.True(testWriter.FireballLearned);
			game.EnterCommand("spells");
			Assert.True(testWriter.SpellKnown);
		}

		[Fact]
		public void RoomDescriptionAfterTakingBook()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get book");
			game.EnterCommand("look");
			var result = testWriter.SeenThings;
			Assert.Equal(RoomDescription, result.EntityDescription);
			var items = new ItemCollection
			{
				new Bottle(),
				new FireballSpellBook()
			};
			Assert.Equal(items, result.Items, new ItemComparer());
			var passages = new[]
			{
				new Passage(1, "north"),
				new Passage(2, "west")
			};
			Assert.Equal(passages, result.Passages, new PassageComparer());
		}

		[Fact]
		public void RoomDescriptionAfterTakingBottle()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("get bottle");
			game.EnterCommand("look");
			var result = testWriter.SeenThings;
			Assert.Equal(RoomDescription, result.EntityDescription);
			var items = new ItemCollection
			{
				new Book(),
				new FireballSpellBook()
			};
			Assert.Equal(items, result.Items, new ItemComparer());
			var passages = new[]
			{
				new Passage(1, "north"),
				new Passage(2, "west")
			};
			Assert.Equal(passages, result.Passages, new PassageComparer());
		}

		[Fact]
		public void TheBadEvilGuyCanOnlyBeAttackedWhenThePlayerIsInTheSameRoom()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("attack evil guy");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.EnemyNotFound, result.CommandType);
			Assert.Equal("evil guy", result.Specifier);
		}

		[Fact]
		public void TryDrinkingFromNotAvailableBottle()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("go north");
			game.EnterCommand("drink bottle");
			var result = testWriter.InvalidCommand;
			Assert.Equal(InvalidCommandType.ItemNotFound, result.CommandType);
			Assert.Equal("bottle", result.Specifier);
			Assert.True(game.IsRunning);
		}

		[Fact]
		public void UnknownCommand()
		{
			var (game, testWriter) = GetCommonGame();
			game.EnterCommand("This is not a real command");
			var result = testWriter.InvalidCommand.CommandType;
			Assert.Equal(InvalidCommandType.UnknownCommand, result);
		}
	}
}