/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using Inventory;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


namespace Inventory
{
	public class ParentInventory : MonoBehaviour
	{
		#region Variables

		//public InventorySO equipmentInventory;


		#endregion

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

//Opslaan en laden van de inventory
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
