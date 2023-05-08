/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LickerInventory : MonoBehaviour
{
	public Dictionary<ItemType, ItemType> lickableItems = new Dictionary<ItemType, ItemType>()
	{
		{ItemType.SugarCane, ItemType.SharpSugarcane},
		{ItemType.Weight, ItemType.HeavyWeight},
		{ItemType.Mushroom, ItemType.PoisenousMushroom}
	};

	public GameObject inventory;
	public InventoryObject inventoryObject;
	public ScriptableObject database;

	public void Update()
	{
		for (int i = 0; i < inventoryObject.Slots.Length; i++)
		{
			var slot = inventoryObject.Slots[i];
			var itemObject = slot.ItemObject;

			if (itemObject.Id >= 0)
			{
				//var newItemType = lickableItems[item.Type];
				//slot.item.Type = newItemType;

				GroundItem.Create(slot.ItemObject);
				slot.RemoveItem();
				////var amount = lickerInventory.GetAmount(i);
				//item.Type = newItemType;

			}
		}
	}

	public void GetNewItemData(ItemObject itemObject)
	{
		if (itemObject.Id >= 0)
		{
			
		}
		//normalDatabase.ItemObjects[inputItem].item
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		inventory.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		inventory.SetActive(false);
		//InventoryContainer.Clear();
	}
}