/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using Inventory;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentSlotsMB : UserInterfaceMB
{
	#region Variables
	private bool _dragging = false;

	public Dictionary<GameObject, InventorySlot> slots = new();
	public InventorySO inventorySO;
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

	private void SwapItems(InventorySlot slot1)
	{
		InventorySlot slot2 = MouseObject.parentSlotContainer.slots[MouseObject.slotHoveredOver];

		inventorySO.SwapSlots(slot1, slot2);
	}

	public void OnEnter(GameObject slotGO)
	{
		MouseObject.slotHoveredOver = slotGO;
		MouseObject.parentSlotContainer = this;
		if (!_dragging)
		{
			var slot = slots[slotGO];
			var itemObject = slot?.ItemObject;

			if ((itemObject?.Item.Id ?? -1) >= 0)
			{
				DescriptionMB.Create(slotGO, itemObject);
			}
		}
	}

	public void OnExit(GameObject slotGO)
	{
		MouseObject.slotHoveredOver = null;
		MouseObject.parentSlotContainer = null;	

		DescriptionMB.DestroyDescription();
	}

	protected void OnDragStart(GameObject draggedSlot)
	{
		if (slots[draggedSlot].ItemObject  != null)
			MouseObject.tempItemBeingDragged = TempItemMB.Create(slots[draggedSlot].ItemObject.Item); //this, de this is voor de interfaceMB, maar die heb ik er nu uitgegooid.
	}

	protected void OnDragEnd(GameObject draggedSlot)
	{
		var slot = slots[draggedSlot];
		var itemObject = slot.ItemObject;
		_dragging = false;
		Destroy(MouseObject.tempItemBeingDragged);

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

	protected void OnDrag(GameObject draggedSlot)
	{
		_dragging = true;
		var tempItemBeingDragged = MouseObject.tempItemBeingDragged;

		if (tempItemBeingDragged != null)
		{
			var mousePosition = MouseObject.GetPosition(); 

			TempItemMB.Move(tempItemBeingDragged, mousePosition);
		}
	}

	#endregion
}
