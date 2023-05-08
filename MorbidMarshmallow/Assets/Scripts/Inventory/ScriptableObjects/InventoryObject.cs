/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
	[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
	public class InventoryObject : ScriptableObject
	{
		public string savePath;
		public ItemDatabaseObject database;
		public InventorySlot[] Slots = new InventorySlot[24];


		public bool AddItemToInventory(ItemObject _itemObject, int _amount)
		{
			InventorySlot slot = FindItemOnInventory(_itemObject);

			if (!database.ItemObjects[_itemObject.Id].Stackable || slot == null)
			{
				return CanAddUnstackableItem(_itemObject, _amount);
			}
			else
			{
				return CanAddStackableItem(slot,_amount);
			}
		}

		public bool CanAddUnstackableItem(ItemObject _itemObject, int _amount)
		{
			if (CountEmptySlots() == 0)
				return false;
			else
			{
				FillNewSlot(_itemObject, _amount);
				return true;
			}
		}

		public bool CanAddStackableItem(InventorySlot slot, int _amount)
		{
			if(true)
			{
				slot.AddAmount(_amount);
				return true;
			}
			//als er ooit een maximum op het aantal stackable objecten komt.
		}

		public int CountEmptySlots()
		{
			int counter = 0;
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject.Id <= -1)
				{
					counter++;
				}
			}
			return counter;
		}

		public InventorySlot FindItemOnInventory(ItemObject _itemObject)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject.Id == _itemObject.Id)
				{
					return Slots[i];
				}
			}
			return null;
		}

		public InventorySlot FillNewSlot(ItemObject _itemObject, int _amount)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject.Id <= -1)
				{
					FillSlot(Slots[i], _itemObject, _amount);
				}
			}
			//negeer item als de inventory vol is.
			return null;
		}

		public InventorySlot FillSlot(InventorySlot slot, ItemObject _itemObject, int _amount)
		{
			slot.UpdateSlot(_itemObject, _amount);
			return slot;
		}

		public void SwapItems(InventorySlot item1, InventorySlot item2)
		{
			if(database.ItemObjects[item1.ItemObject.Id].Stackable)
			{
				SwapStackableItems(item1, item2);
			}
			else
			{
				SwapUnstackableItems(item1, item2);
			}
		}

		public void SwapStackableItems(InventorySlot item1, InventorySlot item2)
		{
			if (item1.ItemObject.Id == item2.ItemObject.Id  && item1 != item2) //item1 != item2 kan misschien verkeerde uitkomst geven omdat hij op onbekende manier vergelijkt.
			{
				item1.AddAmount(item2.amount);// werkt dit?
				item2.RemoveItem();
				return;
			}
		}

		public void SwapUnstackableItems(InventorySlot item1, InventorySlot item2)
		{
			if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
			{
				InventorySlot temp = new InventorySlot(item2.ItemObject, item2.amount);
				item2.UpdateSlot(item1.ItemObject, item1.amount);
				item1.UpdateSlot(temp.ItemObject, temp.amount);
			}
		}

		public void ClearSlots()
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				Slots[i].RemoveItem();
			}
		}

		public void RemoveItem(ItemObject _itemObject)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject == _itemObject)
				{
					Slots[i].UpdateSlot(null, 0);
				}
			}
		}

		public int GetAmount(int index)
		{
			return Slots[index].amount;
		}


		//[ContextMenu("Save")]
		//public void Save()
		//{
		//	IFormatter formatter = new BinaryFormatter();
		//	Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
		//	formatter.Serialize(stream, Container);
		//	stream.Close();
		//}

		//[ContextMenu("load")]
		//public void Load()
		//{
		//	if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
		//	{
		//		IFormatter formatter = new BinaryFormatter();
		//		Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
		//		InventoryContainer newContainer = (InventoryContainer)formatter.Deserialize(stream);
		//		for (int i = 0; i < Slots.Length; i++)
		//		{
		//			Slots[i].UpdateSlot(newContainer.Slots[i].ItemObject, newContainer.Slots[i].amount);
		//		}
		//		stream.Close();
		//	}
		//}
	}

	public delegate void SlotUpdated(InventorySlot _slot);

	public static class ExtensionMethods
	{
		public static void UpdateSlotsDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
		{
			foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
			{
				var slot = _slot.Value;
				slot.UpdateSlotDisplay();
			}
		}

		public static void UpdateSlotDisplay(this InventorySlot slot)
		{
			var slotImage = slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>();
			var slotText = slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>();

			if (slot.ItemObject.Id >= 0)
			{
				slotImage.sprite = slot.ItemObject.UiDisplay;
				slotImage.color = new Color(1, 1, 1, 1);
				slotText.text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
			}
			else
			{
				slotImage.sprite = null;
				slotImage.color = new Color(0, 0, 0, 0);
				slotText.text = "";
			}
		}
	}
}

//kan SwapStackableItems() zo gewoon werken?