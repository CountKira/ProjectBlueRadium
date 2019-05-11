Feature: GameIntegration
	Run the game as an integration test to find error on the top level

Background:
	Given I start a new game in the test dungeon

Scenario: Drink from poisonous bottle
	When I enter drink bottle
	Then I die by poison
	And The game is over

Scenario: Character knows no spells at the beginning
	When I enter spells
	Then I do not know any spells

Scenario Outline: Pick up bottle where case is unimportant
	When I enter <command>
	Then I got <item>

	Examples:
		| command    | item   |
		| get bottle | bottle |
		| get Bottle | bottle |

Scenario Outline: Looking at different items
	When I enter multiple <commands>
	Then The output text shows <itemName>

	Examples:
		| commands           | itemName                                                  |
		| look bottle        | This is a glass bottle, with a green substance inside it. |
		| look book          | The book contains the story of boatmurdered.              |
		| go west,look sword | A sharp sword.                                            |

Scenario Outline: Getting different item
	When I enter multiple <commands>
	Then I got <itemName>

	Examples:
		| commands          | itemName |
		| get bottle        | bottle   |
		| get book          | book     |
		| go west,get sword | sword    |

Scenario: Game ends when entering exit
	When I enter exit
	Then The game is over

Scenario Outline: When I try to get the same item twice I entered an invalid command
	When I enter get <itemName>
	And I enter get <itemName>
	Then The item <itemName> can not be found

	Examples:
		| itemName |
		| book     |
		| bottle   |

Scenario: The endboss has a description
	When I enter go north
	And I enter look evil guy
	Then The output text shows The evil threat of the campaign.

Scenario: Looking at myself
	When I enter me
	Then I see a description of myself

Scenario: Looking at picked up book shows its content
	When I enter get book
	And I enter look book
	Then The output text shows The book contains the story of boatmurdered.

Scenario:  Can learn spells
	When I enter read fireball spell book
	Then I have learned fireball
	When I enter spells
	Then I know spells

Scenario: Entering an unknown command
	When I enter an unknown command
	Then The command is unknown

Scenario: Attacking the end boss without anything ends the game
	When I enter go north
	And I enter attack evil guy
	Then The output text shows Since you do not wield any weapons, the evil guy can easily kill you.
	And The game is over

Scenario: A Book that is not in the same room can not be read
	When I enter go north
	And I enter read fireball spell book
	Then The item fireball spell book can not be found

Scenario:  Can not go into a non existing room
	When I enter go saberwooky
	Then The passage saberwooky can not be found

Scenario: Looking at an item that does not exist can not be found
	When I enter look sasquatchIsMyFather
	Then The entity sasquatchIsMyFather can not be found

Scenario: At the start of the game the character has nothing equipped
	When I enter equipment
	Then I have nothing equipped

Scenario: The bad evil guy can only be attacked when the player is in the same room
	When I enter attack evil guy
	Then The entity evil guy can not be found

Scenario: Try drinking from not available bottle
	When I enter go north
	And I enter drink bottle
	Then The item bottle can not be found
	And The game is running

Scenario: Looking into the main room
	When I enter look
	Then The room is described as "You are in an empty room. The walls are smooth."
	And I see the items "bottle,book,fireball spell book"
	And I see the passages "north,west"

Scenario: Looking into the room north
	When I enter go north
	And I enter look
	Then The room is described as "You are in a dark room."
	And I see no items
	And I see the passages "south"
	And I see the creature "Evil guy"

Scenario: Looking into the room west
	When I enter go west
	And I enter look
	Then The room is described as "You are in a bright room."
	And I see the item "sword"
	And I see the passage "east"

Scenario: Leaving the main room and then going back gives the description of the main room
	When I enter go west
	And I enter go east
	And I enter look
	Then The room is described as "You are in an empty room. The walls are smooth."
	And I see the items "bottle,book,fireball spell book"
	And I see the passages "north,west"

Scenario: When i pick up some items they are in my inventory
	When I enter get bottle
	And I enter get book
	And I enter inventory
	Then I have "bottle,book" in my inventory

Scenario: The character does not have any items at the beginning of the game
	When I enter inventory
	Then I have no items in my inventory

Scenario Outline: When a character takes an item out of the room it can not be seen in the room
	When I enter get <item>
	And I enter look
	Then I see the items "<remaining items>"

	Examples:
		| item   | remaining items            |
		| book   | bottle,fireball spell book |
		| bottle | book,fireball spell book   |

Scenario: When the player gets all the items he can see no items in the room
	When I enter get book
	And I enter get bottle
	And I enter get fireball spell book
	And I enter look
	Then I see no items