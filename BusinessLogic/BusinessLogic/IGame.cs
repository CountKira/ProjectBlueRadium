using System;

namespace BusinessLogic
{
	public interface IGame
	{
		void Stop();
		Item? GetItemObjectInRoom(string item);
		void PickUpItem(Item item);
		bool TryGetPassage(string passageName, out Passage passage);
		void GoToRoomById(int roomId);
		object? GetLocalAvailableEntity(string entity);
		Item? GetItemFromPlayerInventory(string itemName);
		void WieldWeapon(Item item);
		void UnwieldWeapon(Item item);
		void LearnSpell();
		void HandleAttacking(string enemy);
		void HasActed();
		Player Player { get; }
		Item? GetItemFromPlayerEquipment(string itemName);
	}
}