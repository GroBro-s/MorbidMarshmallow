using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
	public string savePath;
	public ItemDatabaseObject database;
	public InventoryContainer Container;
	public InventorySlot[] GetSlots { get { return Container.Slots; } }

	public bool AddItem(Item _item, int _amount)
	{
		InventorySlot slot = FindItemOnInventory(_item);

		if (!database.ItemObjects[_item.Id].stackable || slot == null)
		{
			if (EmptySlotCount <= 0)
				return false;
			else
			{
				SetEmptySlot(_item, _amount);
				return true;
			}
		}
		else
		{
			slot.AddAmount(_amount);
			return true;
		}
	}

	public int EmptySlotCount
	{
		get
		{
			int counter = 0;
			for (int i = 0; i < GetSlots.Length; i++)
			{
				if (GetSlots[i].item.Id <= -1)
					counter++;
			}
			return counter;
		}
	}

	public InventorySlot FindItemOnInventory(Item _item)
	{
		for (int i = 0; i < GetSlots.Length; i++)
		{
			if (GetSlots[i].item.Id == _item.Id)
			{
				return GetSlots[i];
			}
		}
		return null;
	}
	public InventorySlot SetEmptySlot(Item _item, int _amount)
	{
		for (int i = 0; i < GetSlots.Length; i++)
		{
			if (GetSlots[i].item.Id <= -1)
			{
				GetSlots[i].UpdateSlot(_item, _amount);
				return GetSlots[i];
			}
		}
		//set up functionallity when inventory is full
		return null;
	}

	public void SwapItems(InventorySlot item1, InventorySlot item2)
	{
		if (item1.item.Id == item2.item.Id && database.ItemObjects[item1.item.Id].stackable)
		{
			item1.amount += item2.amount;
			item2.RemoveItem();
		}
		if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
		{
			InventorySlot temp = new InventorySlot(item2.item, item2.amount);
			item2.UpdateSlot(item1.item, item1.amount);
			item1.UpdateSlot(temp.item, temp.amount);
		}
	}

	public void RemoveItem(Item _item)
	{
		for (int i = 0; i < GetSlots.Length; i++)
		{
			if (GetSlots[i].item == _item)
			{
				GetSlots[i].UpdateSlot(null, 0);
			}
		}
	}

	[ContextMenu("Save")]
	public void Save()
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
		formatter.Serialize(stream, Container);
		stream.Close();
	}

	[ContextMenu("load")]
	public void Load()
	{
		if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
			InventoryContainer newContainer = (InventoryContainer)formatter.Deserialize(stream);
			for (int i = 0; i < GetSlots.Length; i++)
			{
				GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
			}
			stream.Close();
		}
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
		Container.Clear();
	}
}

[System.Serializable]
public class InventoryContainer
{
	public InventorySlot[] Slots = new InventorySlot[24];
	public void Clear()
	{
		for (int i = 0; i < Slots.Length; i++)
		{
			Slots[i].RemoveItem();
		}
	}
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
	public ItemType[] AllowedItems = new ItemType[0];
	[System.NonSerialized]
	public UserInterface parent;
	[System.NonSerialized]
	public GameObject slotDisplay;
	[System.NonSerialized]
	public SlotUpdated OnAfterUpdate;
	[System.NonSerialized]
	public SlotUpdated OnBeforeUpdate;
	public Item item = new Item();
	public int amount;

	public ItemObject ItemObject
	{
		get
		{
			if (item.Id >= 0)
			{
				return parent.inventory.database.ItemObjects[item.Id];
			}
			return null;
		}
	}
	public InventorySlot()
	{
		UpdateSlot(new Item(), 0);
	}
	public InventorySlot(Item _item, int _amount)
	{
		UpdateSlot(_item, _amount);
	}

	public void UpdateSlot(Item _item, int _amount)
	{
		if (OnBeforeUpdate != null)
			OnBeforeUpdate.Invoke(this);

		item = _item;
		amount = _amount;

		if (OnAfterUpdate != null)
			OnAfterUpdate.Invoke(this);
	}

	public void RemoveItem()
	{
		UpdateSlot(new Item(), 0);
	}

	public void AddAmount(int value)
	{
		UpdateSlot(item, amount += value);
	}

	public bool CanPlaceInSlot(ItemObject _itemObject)
	{
		if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
			return true;
		for (int i = 0; i < AllowedItems.Length; i++)
		{
			if (_itemObject.type == AllowedItems[i])
				return true;
		}
		return false;
	}
}