/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using Inventory;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ParentSlots : UserInterfaceMB
{
	#region Variables
	private bool _dragging = false;

	public Dictionary<GameObject, InventorySlot> slots = new();
	public InventorySO inventorySO;

	private DescriptionMB _descriptionMB;
	#endregion

	#region Unity Methods

	private void Start()
	{
		CreateSlots();
		SetSlotInfo();
	}

	public abstract void CreateSlots();

	private void SetSlotInfo()
	{
		for (int i = 0; i < inventorySO.slots.Length; i++)
		{
			inventorySO.slots[i].parent = this;
			inventorySO.slots[i].OnAfterUpdate += OnSlotUpdate;
		}
	}

	private void OnSlotUpdate(InventorySlot slot)
	{
		slot.UpdateSlotDisplay();
	}

	private void SwapItems(InventorySlot slot)
	{
		InventorySlot mouseHoverSlotData = MouseObject.slotMouseIsOver.slots[MouseObject.slotHoveredOver];

		inventorySO.SwapSlots(slot, mouseHoverSlotData);
	}

	public void OnEnter(GameObject slotGO)
	{
		MouseObject.slotHoveredOver = slotGO;
		if (!_dragging)
		{
			var itemObject = slots[slotGO].ItemObject;

			if (itemObject.Item.Id >= 0)
			{
				_descriptionMB = new DescriptionMB();
			}
		}
	}

	public void OnExit(GameObject slotGO)
	{
		MouseObject.slotHoveredOver = null;

		DescriptionMB.DestroyDescription();
	}

	protected void OnDragStart(GameObject slot)
	{
		MouseObject.tempItemBeingDragged = TempItem.Create(this, slots[slot].ItemObject.Item);
	}

	protected void OnDragEnd(GameObject slotGO)
	{
		var slot = slots[slotGO];
		var itemObject = slot.ItemObject;

		_dragging = false;
		Destroy(MouseObject.tempItemBeingDragged);

		if (MouseObject.interfaceMouseIsOver == null && itemObject.Item.Id >= 0) //Goede aanpassing? (in plaats van 2 if's)
		{
			for (int i = 0; i < slot.amount; i++)
			{
				GroundItem.Create(itemObject.Item.ItemSO);
			}

			slot.ClearSlot();
			return;
		}

		if (MouseObject.slotHoveredOver)
		{
			SwapItems(slot);
		}
	}

	protected void OnDrag(GameObject slot)
	{
		_dragging = true;
		var tempItemBeingDragged = MouseObject.tempItemBeingDragged;
		var mousePosition = MouseObject.GetPosition(); 

		if (tempItemBeingDragged != null)
		{
			TempItem.Move(tempItemBeingDragged, mousePosition);
			//tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
		}
	}

	#endregion
}
