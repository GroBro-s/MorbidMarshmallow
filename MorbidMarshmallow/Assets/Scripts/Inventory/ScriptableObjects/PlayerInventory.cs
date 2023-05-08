/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	public class PlayerInventory : MonoBehaviour
	{
		public InventoryObject playerInventory;
		public InventoryObject equipmentInventory;
		public InventoryObject lickerInventory;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var groundItem = collision.GetComponent<GroundItem>();
			if (groundItem)
			{
				//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
				//als dit niet meer het geval is moet dit systeem aangepast worden.
				int amount = 1;
				if (playerInventory.AddItemToInventory(new ItemObject(groundItem.itemObject), amount))
				{
					Destroy(collision.gameObject);
				}
			}
		}

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

		private void OnApplicationQuit()
		{
			playerInventory.ClearSlots();
			equipmentInventory.ClearSlots();
			lickerInventory.ClearSlots();
		}
	}
}