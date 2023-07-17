/*
* Grobros
* https://github.com/GroBro-s
*/

using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
	public class PlayerInventory : ParentInventory
	{
		#region variables
		public InventorySO inventorySO;

		#endregion

		private void OnTriggerEnter2D(Collider2D collision)
		{
			SetGroundItemToInventorySlot(collision);
		}

		private void SetGroundItemToInventorySlot(Collider2D collision) //TransferGroundItemToInventory
		{
			if (collision.TryGetComponent<GroundItem>(out var groundItem))
			{
				//var collidedItemSO = new ItemSO(groundItem.itemSO);
				//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
				//als dit niet meer het geval is moet dit systeem aangepast worden.
				var itemObject = new ItemObject(groundItem.itemSO);
				if (inventorySO.AddItem(itemObject))
				{
					Destroy(collision.gameObject);
				}
			}
		}

		private void OnApplicationQuit()
		{
			ClearSlots(this.inventorySO);
		}


		//private void OnApplicationQuit()
		//{
		//	playerInventory.ClearSlots();
		//	//equipmentInventory.ClearSlots();
		//	lickerInventory.ClearSlots();
		//}
	}
}

//Opslaan en laden van de inventory
		//private void Update()
		//{
		//	if (Input.GetKeyDown(KeyCode.LeftShift))
		//	{
		//		playerInventory.Save();
		//		equipmentInventory.Save();
		//	}

		//	if (Input.GetKeyDown(KeyCode.RightShift))
		//	{
		//		playerInventory.Load();
		//		equipmentInventory.Load();
		//	}
		//}





//Checken of dit over moet worden gezet?