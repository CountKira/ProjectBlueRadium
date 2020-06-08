using System;

namespace BusinessLogic
{
	public interface IGame
	{
		void Stop();
		Item? GetItemObjectInRoom(string item);
		void PickUpItem(Item item);
		bool TryGetPortal(string portalName, out Portal portal);
		void GoToRoomById(int roomId);
		string? GetLocalAvailableEntityDescription(string entity);
		Item? GetItemFromPlayerInventory(string itemName);
		void WieldWeapon(Item item);
		void UnwieldWeapon(Item item);
		void LearnSpell();
		void HandleAttacking(string enemy);
		void HasActed();
		Creature Player { get; }
		Item? GetItemFromPlayerEquipment(string itemName);
	}
}