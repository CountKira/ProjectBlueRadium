namespace BusinessLogic;

public interface IGame
{
	Creature Player { get; }
	void Stop();
	Item? GetItemObjectInRoom(ItemName item);
	void PickUpItem(Item item);
	bool TryGetPortal(PortalName portalName, out Portal? portal);
	void GoToRoomById(RoomId roomId);
	string? GetLocalAvailableEntityDescription(string entity);
	Item? GetItemFromPlayerInventory(ItemName itemName);
	void WieldWeapon(Item item);
	bool UnwieldWeapon(ItemName itemName);
	void LearnSpell();
	void HandleAttacking(CreatureName enemy);
	void HasActed();
	Room GetCurrentRoom();
}