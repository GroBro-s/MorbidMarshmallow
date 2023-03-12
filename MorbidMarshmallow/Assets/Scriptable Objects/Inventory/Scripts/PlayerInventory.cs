using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public InventoryObject inventory;
	public InventoryObject equipment;
	public InventoryObject lickerInventory;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var groundItem = collision.GetComponent<GroundItem>();
		if(groundItem)
		{
			if(inventory.AddItem(new Item(groundItem.itemObject.item), 1))
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
		lickerInventory.Container.Clear();
	}
}
