using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLogic.SemanticTypes;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Creature : Entity
	{
		readonly ItemCollection equipment = new();
		readonly ItemCollection inventory = new();

		public Creature(string name,
			string description,
			HealthPoints healthPoints,
			Damage damage,
			IEnumerable<Item>? inventory = null,
			IEnumerable<ITag>? tags = null) :
			base(tags ?? Enumerable.Empty<ITag>())
		{
			Name = name;
			Description = description;
			HealthPoints = healthPoints;
			Damage = damage;
			if (inventory != null)
				foreach (var item in inventory)
					this.inventory.Add(item);
		}

		public string Name { get; }
		public string Description { get; }

		public bool IsDead => HealthPoints.ZeroOrBelow;
		public bool CanWieldWeapon => equipment.Any(i => i.HasTag<WeaponTag>());

		public HealthPoints HealthPoints { get; }

		public Damage Damage { get; }
		public bool HasSpell { get; set; }

		public void Equip(Item item)
		{
			VerifyHasItemInInventory(item);
			equipment.Add(item);
		}

		[Conditional("DEBUG")]
		void VerifyHasItemInInventory(Item item)
		{
			if (!inventory.HasItem(item))
				throw new InvalidOperationException("Can not equip a item that is not in the inventory");
		}

		public bool RemoveItem(Item item) => inventory.Remove(item);

		public void PutIntoInventory(Item item) => inventory.Add(item);

		public bool HasItem(string itemName, out Item? item)
		{
			item = inventory.GetItem(itemName);
			return item != null;
		}

		public bool HasKey(LockTag lockTag) => inventory.Any(i => i.GetTag<KeyTag>()?.LockId == lockTag.LockId);

		public Item? GetItem(string itemName) => inventory.GetItem(itemName);

		public bool Unwield(Item weapon) => equipment.Remove(weapon);

		public Damage GetDamage()
		{
			var weaponDamage = GetEquippedWeaponDamage();
			return weaponDamage ?? Damage;
		}

		Damage? GetEquippedWeaponDamage()
		{
			foreach (var item in equipment)
				if (item.GetTag<WeaponTag>() is { } weapon)
					return weapon.Damage;

			return null;
		}

		public Item? WieldsItem(string itemName) => equipment.GetItem(itemName);

		public IEnumerable<Item> GetEquippedItems() => equipment;
		public IEnumerable<Item> GetInventoryItems() => inventory;
	}
}