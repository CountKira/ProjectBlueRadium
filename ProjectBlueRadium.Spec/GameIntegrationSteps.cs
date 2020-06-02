using System;
using System.Linq;
using BusinessLogic;
using SharedTestingResources;
using TechTalk.SpecFlow;
using Xunit;

// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global

namespace ProjectBlueRadium.Spec
{
	[Binding]
	public class GameIntegrationSteps
	{
		class MockRandom:IRandom
		{
			/// <inheritdoc />
			public int Next(int i) => 1;
		}

		readonly TestWriter writer = new TestWriter();
		Game game;

		[Given(@"I start a new game in the test dungeon with (.*) health")]
		public void GivenIStartANewGameInTheTestDungeon(int health)
		{
			game = new Game(writer, new TestingRoomRepository(), new MockRandom());
			game.Player.HealthPoints = health;
		}


		[When(@"I enter (?!multiple)(.*)")]
		public void WhenIEnter(string command)
		{
			game.EnterCommand(command);
		}

		[When(@"I enter multiple (.*)")]
		public void WhenIDoMultiple(string[] commands)
		{
			foreach (var command in commands) game.EnterCommand(command);
		}

		[Then(@"The game is over")]
		public void ThenTheGameIsOver()
		{
			Assert.False(game.IsRunning);
		}

		[Then(@"I do not know any spells")]
		public void ThenIDoNotKnowAnySpells()
		{
			Assert.False(writer.SpellKnown);
		}

		[Then(@"I know spells")]
		public void ThenIKnowSpells()
		{
			Assert.True(writer.SpellKnown);
		}

		[Then(@"I got (.*)")]
		public void GotItem(string itemName)
		{
			var result = writer.Action;
			Assert.Equal(VerbEnum.Get, result.Verb);
			Assert.Equal(itemName, result.Specifier);
		}

		[Then(@"The output text shows (.*)")]
		public void TheOutputTextShows(string expectedDescription)
		{
			var actualDescription = writer.TextOutput.Dequeue();
			Assert.Equal(expectedDescription, actualDescription);
		}

		[Then(@"The item (.*) can not be found")]
		public void TheItemCanNotBeenFound(string specifier)
		{
			var result = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.ItemNotFound, result.CommandType);
			Assert.Equal(specifier, result.Specifier);
		}

		[Then(@"I see a description of myself")]
		public void ThenISeeADescriptionOfMyself()
		{
			var result = writer.Me;
			Assert.Equal("The hero of our story", result);
		}

		[Then(@"I have learned fireball")]
		public void ThenIHaveLearnedFireball()
		{
			Assert.True(writer.FireballLearned);
		}

		[Then(@"The command is unknown")]
		public void UnknownCommand()
		{
			var result = writer.InvalidCommand.CommandType;
			Assert.Equal(InvalidCommandType.UnknownCommand, result);
		}

		[Then(@"The portal (.*) can not be found")]
		public void EntryWayNotFound(string entryWayName)
		{
			var result = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.PortalNotFound, result.CommandType);
			Assert.Equal(entryWayName, result.Specifier);
		}

		[Then(@"I have nothing equipped")]
		public void ThenIHaveNothingEquipped()
		{
			var actual = writer.Equipment.Any();
			Assert.False(actual);
		}

		[Then(@"The entity (.*) can not be found")]
		public void ThenEntityCanNotBeFound(string entity)
		{
			var result = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.EntityNotFound, result.CommandType);
			Assert.Equal(entity, result.Specifier);
		}

		[Then(@"The game is running")]
		public void ThenTheGameIsRunning()
		{
			Assert.True(game.IsRunning);
		}

		[Then(@"The room is described as ""(.*)""")]
		public void ThenTheRoomIsDescribedAs(string roomDescription)
		{
			var result = writer.SeenThings;
			Assert.Equal(roomDescription, result.EntityDescription);
		}

		[Then(@"I see the items ""(.*)""")]
		[Then(@"I see the item ""(.*)""")]
		public void ThenISeeTheItems(string[] items)
		{
			var result = writer.SeenThings;
			Assert.Equal(items, result.Items.Select(i => i.Name).ToArray());
		}

		[Then(@"I see no items")]
		public void ThenISeeNoItems()
		{
			ThenISeeTheItems(Array.Empty<string>());
		}

		[Then(@"I see the portals? ""(.*)""")]
		public void ThenISeeTheEntryWays(string[] entryWays)
		{
			var result = writer.SeenThings;
			Assert.Equal(entryWays, result.Portals.Select(p => p.DisplayName).ToArray());
		}

		[Then(@"I see the creature ""(.*)""")]
		public void ThenISeeTheCreatures(string[] creatures)
		{
			var result = writer.SeenThings;
			Assert.Equal(creatures, result.Creatures.Select(c => c.Name));
		}

		[Then(@"I have no items in my inventory")]
		public void ThenIHaveNoItemsInMyInventory()
		{
			ThenIHaveInMyInventory(Array.Empty<string>());
		}

		[Then(@"I have ""(.*)"" in my inventory")]
		public void ThenIHaveInMyInventory(string[] items)
		{
			var actual = writer.Inventory;
			Assert.Equal(items, actual.Select(i => i.Name));
		}

		[Then(@"I have (?!nothing)(.*) equipped")]
		public void ThenIHaveSwordEquipped(string item)
		{
			var actual = writer.Equipment;
			Assert.Contains(item, actual.Select(e => e.Name));
		}

		[Then(@"I equipped sword")]
		public void ThenIEquippedSword()
		{
			var actual = writer.Action;
			Assert.Equal(VerbEnum.Wield, actual.Verb);
			Assert.Equal("sword", actual.Specifier);
		}

		[Then(@"I get notified that the (.*) can not be equipped")]
		public void ThenIGetNotifiedThatTheItemCanNotBeEquipped(string item)
		{
			var actual = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.CanNotWield, actual.CommandType);
			Assert.Equal(item, actual.Specifier);
		}

		[Then(@"I get notified that something is already equipped")]
		public void ThenIGetNotifiedThatSomethingIsAlreadyEquipped()
		{
			var actual = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.AlreadyWielding, actual.CommandType);
		}

		[Then(@"I have (.*) hp")]
		public void ThenIHaveHp(int healthPoints)
		{
			Assert.Equal(healthPoints, game.Player.HealthPoints);
		}


		[StepArgumentTransformation]
		public string[] TransformToStringArray(string str) => str.Split(',');
	}
}