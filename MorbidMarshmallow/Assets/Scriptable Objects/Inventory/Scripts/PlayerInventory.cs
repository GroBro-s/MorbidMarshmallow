using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public InventoryObject inventory;
	public InventoryObject equipment;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var item = collision.GetComponent<GroundItem>();
		if(item)
		{
			if(inventory.AddItem(new Item(item.item), 1))
			{
				Destroy(collision.gameObject);
			}
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			inventory.Save();
			equipment.Save();
		}

		if(Input.GetKeyDown(KeyCode.RightShift))
		{
			inventory.Load();
			equipment.Load();
		}
	}

	private void OnApplicationQuit()
	{
		inventory.Container.Clear();
		equipment.Container.Clear();
	}
}
