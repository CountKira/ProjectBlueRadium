using System;

namespace BusinessLogic
{
	public interface IGame
	{
		void Stop();
		Item? GetItemObjectInRoom(string item);
		void PickUpItem(Item item);
		bool TryGetConnectedRoom(string passageName, out int roomId);
		void GoToRoomById(int roomId);
		object GetLocalAvailableEntity(string entity);
		Item GetItemFromPlayerInventory(string itemName);
		void EquipWeapon(Item item);
		void LearnSpell();
		void HandleAttacking(string enemy);
		void HasActed();
		Player Player { get; }
	}
}