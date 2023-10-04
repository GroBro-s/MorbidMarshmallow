/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	//De parentinventory moet worden opgedeelt in 2 groepen van inventories die grounditems op kunnen pakken en
	//inventories die dat niet kunnen.
	public class PlayerInventoryMB : ParentInventoryMB
	{
		#region variables
		//public InventorySO inventorySO;
		public GameObject playerInventoryUI;
		#endregion

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent<GroundItemMB>(out var groundItem))
			{
				SetGroundItemToInventorySlot(collision, groundItem);
			}
		}

		private void SetGroundItemToInventorySlot(Collider2D collision, GroundItemMB groundItem) //TransferGroundItemToInventory
		{
			//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
			//als dit niet meer het geval is moet dit systeem aangepast worden.
			var itemObject = new ItemObject(groundItem.itemSO);
			if (playerInventoryUI.GetComponent<DynamicSlotsMB>().AddItem(itemObject))
			{
				Destroy(collision.gameObject);
			}
		}

		//private void OnApplicationQuit()
		//{
		//	ClearSlots(this.inventorySO);
		//}
	}
}