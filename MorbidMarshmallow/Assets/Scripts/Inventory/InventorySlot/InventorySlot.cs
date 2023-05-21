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

		#region GetSetItemObject
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
		#endregion

		//kan deze weg?
		public InventorySlot(ItemObject itemObject, int amount)
		{
			UpdateSlot(itemObject, amount);
		}

		public void UpdateSlot(ItemObject itemObject, int amount)
		{
			OnBeforeUpdate?.Invoke(this);

			ItemObject = itemObject;
			this.amount = amount;

			OnAfterUpdate?.Invoke(this);
			//kan beter?
		}

		public void ClearSlot()
		{
			UpdateSlot(null, 0);
		}

		public void AddAmount(int value)
		{
			UpdateSlot(ItemObject, amount += value);
		}

		public bool IsAllowedInSlot(ItemObject itemObject)
		{
			var hasItem = AllowedItems.Length > 0 || itemObject != null || itemObject.Item.Id >= 0;
			
			return hasItem ? CheckAllowedItems(itemObject) : true;
		}

		private bool CheckAllowedItems(ItemObject itemObject)
		{
			for (int i = 0; i < AllowedItems.Length; i++)
			{
				if (itemObject.Item.Type == AllowedItems[i])
				{
					return true;
				}
			}
			return false;
		}
	}
}