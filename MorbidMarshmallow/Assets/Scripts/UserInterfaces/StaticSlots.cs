using Inventory;
using UnityEngine.EventSystems;

public class StaticSlots : ParentSlots
{
	public override void CreateSlots()
	{
		foreach (var slot in inventorySO.slots)	
		{
			AddEvent(slot.slotGO, EventTriggerType.PointerEnter, delegate { OnEnter(slot.slotGO); });
			AddEvent(slot.slotGO, EventTriggerType.PointerExit, delegate { OnExit(slot.slotGO); });
			AddEvent(slot.slotGO, EventTriggerType.BeginDrag, delegate { OnDragStart(slot.slotGO); });
			AddEvent(slot.slotGO, EventTriggerType.EndDrag, delegate { OnDragEnd(slot.slotGO); });
			AddEvent(slot.slotGO, EventTriggerType.Drag, delegate { OnDrag(slot.slotGO); });
			base.slots.Add(slot.slotGO, slot); 
		}
	}
}