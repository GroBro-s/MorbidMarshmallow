/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using UnityEngine;

namespace Inventory
{
	public class ParentInventoryMB : MonoBehaviour
	{
		#region Unity Methods

		public void ClearSlots(InventorySO inventory)
		{
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				inventory.slots[i].ClearSlot();
			}
		}
		#endregion
	}
}

//Opslaan, laden en clearen van de inventory
//private void Update()
//{
//	if (Input.GetKeyDown(KeyCode.LeftShift))
//	{
//		playerInventory.Save();
//		equipmentInventory.Save();
//		lickerInventory.Save();
//	}

//	if (Input.GetKeyDown(KeyCode.RightShift))
//	{
//		playerInventory.Load();
//		equipmentInventory.Load();
//		lickerInventory.Load();
//	}
//}

//private void OnApplicationQuit()
//{
//	playerInventory.ClearSlots();
//	//equipmentInventory.ClearSlots();
//	lickerInventory.ClearSlots();
//}