using System;

namespace BusinessLogic
{
	public interface IGame
	{
		void AddToPlayerInventory(Item item);
		void YouDiedByPoison();
		void Stop();
		void WriteAction(ActionDTO actionDTO);
		Item? GetItemObjectInRoom(string item);
		void PickUpItem(Item item);
		bool TryGetConnectedRoom(string passageName, out int roomId);
		void GoToRoomById(int roomId);
		object GetLocalAvailableEntity(string entity);
		Item GetItemFromPlayerInventory(string itemName);
		void EquipWeapon(Item item);
		void LearnSpell();
		void HandleAttackingTheEvilGuy(string enemy);
		bool HasActed();
		Player Player { get; }
	}
}