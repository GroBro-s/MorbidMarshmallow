/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class LickerMB : ParentInventoryMB
	{
		public Dictionary<ItemType, ItemType> lickableItems = new Dictionary<ItemType, ItemType>()
		{
			{ItemType.SugarCane, ItemType.SharpSugarcane},
			{ItemType.Weight, ItemType.HeavyWeight},
			{ItemType.Mushroom, ItemType.PoisenousMushroom}
		};

		public GameObject lickerInventory;
		public InventorySO inventorySO;
		public ScriptableObject database;

		public void Update()
		{
			for (int i = 0; i < inventorySO.slots.Length; i++)
			{
				var slot = inventorySO.slots[i];
				var itemObject = slot.ItemObject;

				if (itemObject != null && itemObject.Item.Id >= 0)
				{
					//MakeGroundItem(slot);
				}
			}
		}

		private void OnApplicationQuit()
		{
			ClearSlots(this.inventorySO);
		}

		private void MakeGroundItem(InventorySlot slot)
		{
			GroundItemMB.Create(slot.ItemObject.Item.ItemSO);
			slot.ClearSlot();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			lickerInventory.SetActive(true);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			lickerInventory.SetActive(false);
		}
	}
}
