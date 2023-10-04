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
		[SerializeField] private GameObject lickerInventoryUI;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			lickerInventoryUI.SetActive(true);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			lickerInventoryUI.SetActive(false);
		}
	}
}




#region Mislukt beginsel licker

//public Dictionary<ItemType, ItemType> lickableItems = new Dictionary<ItemType, ItemType>()
//{
//	{ ItemType.SugarCane, ItemType.SharpSugarcane},
//	{ ItemType.Weight, ItemType.HeavyWeight},
//	{ ItemType.Mushroom, ItemType.PoisenousMushroom}
//};

//public void Update()
//{
//	for (int i = 0; i < slots.Length; i++)
//	{
//		var slot = slots[i];
//		var itemObject = slot.ItemObject;

//		if (itemObject != null && itemObject.Item.Id >= 0)
//		{
//			//MakeGroundItem(slot);
//		}
//	}
//}

//private void MakeGroundItem(InventorySlot slot)
//{
//	GroundItemMB.Create(slot.ItemObject.Item.ItemSO);
//	slot.ClearSlot();
//}
#endregion

//public ScriptableObject database;
//public InventorySO inventorySO;

//private void OnApplicationQuit()
//{
//	ClearSlots(this.inventorySO);
//}