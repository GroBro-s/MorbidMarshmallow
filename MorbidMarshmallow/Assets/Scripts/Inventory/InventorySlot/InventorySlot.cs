/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
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
		public ItemObject ItemObject { get; set; }
		public int amount;

		//public ItemObject ItemObject
		//{
		//	get
		//	{
		//		if (itemObject.Id >= 0)
		//		{
		//			return parent.inventory.database.ItemObjects[item.Id];
		//		}
		//		return null;
		//	}
		//}
		public InventorySlot()
		{
			UpdateSlot(new ItemObject(), 0);
		}

		public InventorySlot(ItemObject itemObject, int _amount)
		{
			UpdateSlot(itemObject, _amount);
		}

		public void UpdateSlot(ItemObject itemObject, int _amount)
		{
			OnBeforeUpdate?.Invoke(this);

			ItemObject = itemObject;
			amount = _amount;

			OnAfterUpdate?.Invoke(this);
			//kan beter?
		}

		public void RemoveItem()
		{
			UpdateSlot(new ItemObject(), 0);
		}

		public void AddAmount(int value)
		{
			UpdateSlot(ItemObject, amount += value);
		}

		public bool CanPlaceInSlot(ItemObject _itemObject)
		{
			if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.Id < 0)
			{
				return true;
			}
			else
			{
				for (int i = 0; i < AllowedItems.Length; i++)
				{
					if (_itemObject.Type == AllowedItems[i])
						return true;
				}
				return false;
			}
		}
	}
}


//REGEL 61!! en 40!!
