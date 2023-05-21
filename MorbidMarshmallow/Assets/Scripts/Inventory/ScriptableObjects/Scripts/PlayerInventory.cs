/*
* Grobros
* https://github.com/GroBro-s
*/

using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
	public class PlayerInventory : MonoBehaviour
	{
		public InventorySO playerInventory;
		public InventorySO equipmentInventory;
		public InventorySO lickerInventory;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			SetGroundItemToInventorySlot(collision);
		}

		private void OnApplicationQuit()
		{
			playerInventory.ClearSlots();
			equipmentInventory.ClearSlots();
			lickerInventory.ClearSlots();
		}

		private void SetGroundItemToInventorySlot(Collider2D collision) //TransferGroundItemToInventory
		{
			if (collision.TryGetComponent<GroundItem>(out var groundItem))
			{
				var itemObject = new ItemSO(groundItem.itemSO);
				//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
				//als dit niet meer het geval is moet dit systeem aangepast worden.
				if (playerInventory.CanAddItem(itemObject))
				{
					playerInventory.AddItem(itemObject);
					Destroy(collision.gameObject);
				}
			}
		}
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