/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory
{
	public class ParentInventoryMB : MonoBehaviour
	{
		#region variables
		[SerializeField] protected ItemDatabaseSO itemDatabaseSO;
		#endregion

		#region Unity Methods
		private void Start()
		{
			var gameController = GameObject.Find("GameController");
			itemDatabaseSO = gameController.GetComponent<GameStatsMB>().ItemDatabaseSO;
		}
		#endregion
	}
}

//Opslaan en laden van de inventory

//public string savePath { get; set; }
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
