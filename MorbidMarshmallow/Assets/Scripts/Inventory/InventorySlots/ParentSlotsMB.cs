/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ParentSlotsMB : UserInterfaceMB
{
	#region Variables
	private bool _dragging = false;
	//private readonly int _slotAmount = 20;

	[SerializeField] protected ItemDatabaseSO itemDatabaseSO;

	protected Dictionary<GameObject, InventorySlot> slots_dict = new();
	protected InventorySlot[] slots = new InventorySlot[20];	
	//public InventorySO inventorySO;
	//public ParentInventoryMB parentInventoryMB;
	//public string savePath;
	#endregion

	#region Unity Methods
	private void Start()
	{
		//slots = new InventorySlot[_slotAmount];

		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		itemDatabaseSO = gameStatsMB.ItemDatabaseSO;

		CreateSlots();
		//SetSlotInfo();
	}

	//private void SetSlotInfo()
	//{
	//	for (int i = 0; i < slots.Length; i++)
	//	{
	//		slots[i] = new InventorySlot
	//		{
	//			parent = this
	//		};
	//		slots[i].OnAfterUpdate += OnSlotUpdate;
	//	}
	//}

	public void OnEnter(GameObject slotGO)
	{
		MouseObject.OnEnterSlot(slotGO, this);

		if (!_dragging)
		{
			var slot = slots_dict[slotGO];
			var itemObject = slot?.ItemObject;

			if ((itemObject?.Item.Id ?? -1) >= 0)
			{
				var slotPos = slotGO.transform.position;
				var descriptionText = itemObject.Item.ItemSO.description;

				DescriptionMB.Create(slotPos, descriptionText);
			}
		}
	}

	public void OnExit(GameObject slotGO)
	{
		MouseObject.OnExitSlot();

		DescriptionMB.DestroyDescription();
	}

	public void OnDragStart(GameObject draggedSlot)
	{
		if (slots_dict[draggedSlot].ItemObject  != null)
		{
			MouseObject.tempItemBeingDragged = TempItemMB.Create(slots_dict[draggedSlot].ItemObject.Item);
		}
	}

	public void OnDragEnd(GameObject draggedSlot)
	{
		var slot = slots_dict[draggedSlot];
		var itemObject = slot.ItemObject;
		_dragging = false;

		TempItemMB.Delete(MouseObject.tempItemBeingDragged);

		if(itemObject != null)
		{
			if (MouseObject.interfaceMouseIsOver == null && itemObject.Item.Id >= 0)
			{
				for (int i = 0; i < slot.amount; i++)
				{
					GroundItemMB.Create(itemObject.Item.ItemSO);
				}

				slot.ClearSlot();
				return;
			}

			if (MouseObject.slotHoveredOver)
			{
				SwapItems(slot);
			}
		}
	}

	public void OnDrag(GameObject draggedSlot)
	{
		_dragging = true;
		var tempItemBeingDragged = MouseObject.tempItemBeingDragged;

		if (tempItemBeingDragged != null)
		{
			var mousePosition = MouseObject.GetPosition(); 

			TempItemMB.Move(tempItemBeingDragged, mousePosition);
		}
	}

	protected GameObject AddEvents(GameObject slot)
	{
		AddEvent(slot, EventTriggerType.PointerEnter, delegate { OnEnter(slot); });
		AddEvent(slot, EventTriggerType.PointerExit, delegate { OnExit(slot); });
		AddEvent(slot, EventTriggerType.BeginDrag, delegate { OnDragStart(slot); });
		AddEvent(slot, EventTriggerType.EndDrag, delegate { OnDragEnd(slot); });
		AddEvent(slot, EventTriggerType.Drag, delegate { OnDrag(slot); });

		return slot;
	}

	public InventorySlot FindItemOnInventory(int itemId_object)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			var itemId_slot = slots[i].ItemObject?.Item.Id;
			if (itemId_slot == itemId_object) // slot moet null zijn maar de editor laat 0 zien. alsnog wordt hij hier als ongelijk weergegeven, wat klopt. Wij hebben geen verklaring kan later errors opleveren.
			{
				return slots[i];
			}
		}
		return null;
	}

	public InventorySlot FillNewSlot(ItemObject itemObject, int amount)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].ItemObject == null)
			{
				slots[i].UpdateSlot(itemObject, amount);
				return slots[i];
			}
		}
		//negeer item als de inventory vol is.
		return null;
	}

	protected void OnSlotUpdate(InventorySlot slot)
	{
		slot.UpdateSlotDisplay();
	}

	public void SwapSlots(InventorySlot slot1, InventorySlot slot2)
	{
		var item1 = slot1.ItemObject.Item;
		var item2 = slot2.ItemObject?.Item;

		if (item1.Id == item2?.Id && slot1 != slot2)
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

	public abstract void CreateSlots();

	public void ClearSlots()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].ClearSlot();
		}
	}

	public bool AddItem(ItemObject itemObject, int amount = 1)
	{
		var itemId_object = itemObject.Item.Id;
		InventorySlot slot = FindItemOnInventory(itemId_object);
		var stackableItem = itemDatabaseSO.ItemSOlist[itemId_object].stackable;

		if (!stackableItem || (stackableItem && slot == null))
		{
			FillNewSlot(itemObject, amount);
			return true;
		}
		else  //als er ooit een maximum op het aantal stackable objecten komt moet hier de voorwaarde aangepast worden.
		{
			slot.AddAmount(amount);
			return true;
		};
	}

	private void SwapItems(InventorySlot slot1)
	{
		InventorySlot slot2 = MouseObject.parentSlotsMB.slots_dict[MouseObject.slotHoveredOver];

		SwapSlots(slot1, slot2);
	}

	public void RemoveItem(ItemObject itemObject)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].ItemObject == itemObject)
			{
				slots[i].ClearSlot();
			}
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
		var slotImage = slot.slotGO.transform.GetChild(0).GetComponentInChildren<Image>();
		var slotText = slot.slotGO.GetComponentInChildren<TextMeshProUGUI>();

		if ((slot.ItemObject?.Item.Id ?? -1) >= 0)
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
