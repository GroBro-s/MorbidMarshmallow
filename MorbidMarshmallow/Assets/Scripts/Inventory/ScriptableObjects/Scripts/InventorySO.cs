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
	public class InventorySO : ScriptableObject
	{
		public string savePath;
		public ItemDatabaseObject database;
		public InventorySlot[] Slots = new InventorySlot[24];

		
		public bool CanAddItem(ItemObject itemObject)
		{
			InventorySlot slot = FindItemOnInventory(itemObject);

			if (!database.ItemObjects[itemObject.Item.Id].stackable || slot == null)
			{
				return CanAddUnstackableItem();
			}
			else
			{
				return CanAddStackableItem();
			}
		}

		public void AddItem(ItemObject itemObject, int amount = 1)
		{
			InventorySlot slot = FindItemOnInventory(itemObject);

			if (!database.ItemObjects[itemObject.Item.Id].stackable  || slot == null)
			{
				FillNewSlot(itemObject, amount);	
			}
			else
			{
				slot.AddAmount(amount);
			}
		}

		public bool CanAddUnstackableItem()
		{
			if (CountEmptySlots() == 0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}

		public bool CanAddStackableItem()
		{
			if(true)
			{
				return true;
			}
			//als er ooit een maximum op het aantal stackable objecten komt moet hier een else toegevoegd worden.
		}

		public int CountEmptySlots()
		{
			int counter = 0;
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject.Item.Id <= -1)
				{
					counter++;
				}
			}
			return counter;
		}

		public InventorySlot FindItemOnInventory(ItemObject itemObject)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject.Item.Id == itemObject.Item.Id)
				{
					return Slots[i];
				}
			}
			return null;
		}

		public InventorySlot FillNewSlot(ItemObject itemObject, int amount)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject.Item.Id <= -1)
				{
					Slots[i].UpdateSlot(itemObject, amount);
				}
			}
			//negeer item als de inventory vol is.
			return null;
		}

		public void SwapSlots(InventorySlot slot1, InventorySlot slot2)
		{
			var item1 = slot1.ItemObject.Item;
			var item2 = slot2.ItemObject.Item;

			if (item1.Id == item2.Id && slot1 != slot2)
			{
				var isStackable = item1.Stackable;

				if (isStackable)
				{
					MergeStackableSlots(slot1, slot2);
				}

				else
				{
					SwapUnstackableSlots(slot1, slot2);
				}
			}
			else
			{
				SwapUnstackableSlots(slot1, slot2);
			}
		}

		private void MergeStackableSlots(InventorySlot slot1, InventorySlot slot2)
		{
			slot1.AddAmount(slot2.amount);
			slot2.ClearSlot();
		}

		private void SwapUnstackableSlots(InventorySlot slot1, InventorySlot slot2)
		{
			InventorySlot temp = new InventorySlot(slot2.ItemObject, slot2.amount);
			slot2.UpdateSlot(slot1.ItemObject, slot1.amount);
			slot1.UpdateSlot(temp.ItemObject, temp.amount);
		}

		public void ClearSlots()
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				Slots[i].ClearSlot();
			}
		}

		public void RemoveItem(ItemObject itemObject)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i].ItemObject == itemObject)
				{
					Slots[i].ClearSlot();
				}
			}
		}

		#region Save and load method 
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
		#endregion
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

			if (slot.ItemObject.Item.Id >= 0)
			{
				slotImage.sprite = slot.ItemObject.Item.Sprite;
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