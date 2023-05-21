using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticSlots : UserInterface
{
	public GameObject[] slots;

	public override void CreateSlots()
	{
		slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
		for (int i = 0; i < inventory.Slots.Length; i++)
		{
			var obj = slots[i];

			AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
			AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
			AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
			AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
			AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
			inventory.Slots[i].slotDisplay = obj;
			slotsOnInterface.Add(obj, inventory.Slots[i]); 
		}
	}
}
