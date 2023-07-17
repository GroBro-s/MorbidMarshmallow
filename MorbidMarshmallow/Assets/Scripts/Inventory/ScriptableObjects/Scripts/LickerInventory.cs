/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class LickerInventory : ParentInventory
	{
		public Dictionary<ItemType, ItemType> lickableItems = new Dictionary<ItemType, ItemType>()
		{
			{ItemType.SugarCane, ItemType.SharpSugarcane},
			{ItemType.Weight, ItemType.HeavyWeight},
			{ItemType.Mushroom, ItemType.PoisenousMushroom}
		};

		public GameObject lickerInventory;
		public InventorySO inventorySO;

		public InventorySO inventoryObject;
		public ScriptableObject database;

		public void Update()
		{
			for (int i = 0; i < inventoryObject.slots.Length; i++)
			{
				var slot = inventoryObject.slots[i];
				var itemObject = slot.ItemObject;

				if (itemObject != null && itemObject.Item.Id >= 0)
				{
					MakeGroundItem(slot);

					//var newItemType = lickableItems[item.Type];
					//slot.item.Type = newItemType;

					////var amount = lickerInventory.GetAmount(i);
					//item.Type = newItemType;

				}
			}
		}

		private void OnApplicationQuit()
		{
			ClearSlots(this.inventorySO);
		}

		private void MakeGroundItem(InventorySlot slot)
		{
			GroundItem.Create(slot.ItemObject.Item.ItemSO);
			slot.ClearSlot();
		}

		//public void GetNewItemData(ItemSO itemObject)
		//{
		//	if (itemObject.id >= 0)
		//	{

		//	}
		//	//normalDatabase.ItemObjects[inputItem].item
		//}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			lickerInventory.SetActive(true);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			lickerInventory.SetActive(false);
			//InventoryContainer.Clear();
		}
	}
}
