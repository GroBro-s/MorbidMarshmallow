using System.Collections.Generic;
using UnityEngine;

public class Licker : MonoBehaviour
{
	public GameObject inventory;

	public InventoryObject playerInventory;
	public InventoryObject lickerInventory;

	public ScriptableObject database;

	private ScriptableObject normalDatabase;
	private ScriptableObject weaponsDatabase;

	public void Start()
	{
		//normalDatabase = database.ItemObjects[];
		//weaponsDatabase = database.weaponObjects[];
	}
	public void Update()
	{
		for (int i = 0; i < lickerInventory.GetSlots.Length; i++)
		{
			if (lickerInventory.GetSlots[i].item.Id >= 0)
			{
				var itemType = lickerInventory.GetSlots[i].item.Type;
				switch (itemType)
				{
					case ItemType.Food: return;
					case ItemType.SugarCane: return;
					case ItemType.SharpSugarcane: return;

				}
			}
		}
		//for (int i = 0; i < lickerInventory.GetSlots.Length; i++)
		//{
		//	if (lickerInventory.GetSlots[i].item.Id >= 0)
		//	{
		//		lickerInventory.GetSlots[i].item.Id += 1;
		//	}
		//}
	}

	public void GetNewItemData(InventorySlot inputItem)
	{
		if(inputItem.item.Id >= 0)
		{
			//In
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
	}
}