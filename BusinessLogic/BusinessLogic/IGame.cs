namespace BusinessLogic
{
	public interface IGame
	{
		Creature Player { get; }
		void Stop();
		Item? GetItemObjectInRoom(string item);
		void PickUpItem(Item item);
		bool TryGetPortal(string portalName, out Portal? portal);
		void GoToRoomById(RoomId roomId);
		string? GetLocalAvailableEntityDescription(string entity);
		Item? GetItemFromPlayerInventory(string itemName);
		void WieldWeapon(Item item);
		bool UnwieldWeapon(string itemName);
		void LearnSpell();
		void HandleAttacking(string enemy);
		void HasActed();
		Room GetCurrentRoom();
	}
}