﻿using System;
using System.Linq;
using BusinessLogic;
using SharedTestingResources;
using TechTalk.SpecFlow;
using Xunit;

namespace ProjectBlueRadium.Spec
{
    [Binding]
    public class GameIntegrationSteps
    {
		private readonly TestWriter writer = new TestWriter();
		private Game game;

		[Given(@"I start a new game in the test dungeon")]
		public void GivenIStartANewGameInTheTestDungeon()
		{
			game = new Game(writer, new TestingRoomRepository());
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

		[Then(@"I die by poison")]
		public void ThenIDieByPoison()
		{
			Assert.True(writer.DiedPyPoison);
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
		public void IGot(string itemName)
		{
			var result = writer.Action;
			Assert.Equal(Verb.Get, result.Verb);
			Assert.Equal(itemName, result.Specifier);
		}

		[Then(@"The output text shows (.*)")]
		public void TheOutputTextShows(string expectedDescription)
		{
			var actualDescription = writer.TextOutput;
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
			Assert.Equal("It is you.", result);
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

		[Then(@"The passage (.*) can not be found")]
		public void PassageNotFound(string passageName)
		{
			var result = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.PassageNotFound, result.CommandType);
			Assert.Equal(passageName, result.Specifier);
		}

		[Then(@"I have nothing equipped")]
		public void ThenIHaveNothingEquipped()
		{
			var actual = writer.HasNothingEquipped;
			Assert.True(actual);
		}

		[Then(@"The enemy (.*) can not be found")]
		public void ThenTheEnemyEvilGuyCanNotBeFound(string enemyName)
		{
			var result = writer.InvalidCommand;
			Assert.Equal(InvalidCommandType.EnemyNotFound, result.CommandType);
			Assert.Equal(enemyName, result.Specifier);
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
			var s = new string[0];
			var result = writer.SeenThings;
			Assert.Equal(items, result.Items.Select(i => i.Name).ToArray());
		}

		[Then(@"I see no items")]
		public void ThenISeeNoItems()
		{
			ThenISeeTheItems(Array.Empty<string>());
		}

		[Then(@"I see the passages ""(.*)""")]
		[Then(@"I see the passage ""(.*)""")]
		public void ThenISeeThePassages(string[] passages)
		{
			var result = writer.SeenThings;
			Assert.Equal(passages, result.Passages.Select(p => p.DisplayName).ToArray());
		}

		[Then(@"I see the creatures ""(.*)""")]
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

		[StepArgumentTransformation]
		public string[] TransformToStringArray(string str) => str.Split(',');
	}
}
