/*
* Grobros
* https://github.com/GroBro-s
*/
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicSlotsMB : ParentSlotsMB
{
	public GameObject slotPrefab;
	public int X_START;
	public int Y_START;
	public int X_SPACE_BETWEEN_ITEM;
	public int NUMBER_OF_COLUMN;
	public int Y_SPACE_BETWEEN_ITEM;

	public override void CreateSlots()
	{
		for (int i = 0; i < inventorySO.slots.Length; i++)
		{
			var slot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
			slot.GetComponent<RectTransform>().localPosition = GetPosition(i);

			AddEvents(slot);

			inventorySO.slots[i].slotGO = slot;

			slots.Add(slot, inventorySO.slots[i]);
		}
	}

	private Vector3 GetPosition(int i)
	{
		return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
	}

	private GameObject AddEvents(GameObject slot)
	{
		AddEvent(slot, EventTriggerType.PointerEnter, delegate { OnEnter(slot); });
		AddEvent(slot, EventTriggerType.PointerExit, delegate { OnExit(slot); });
		AddEvent(slot, EventTriggerType.BeginDrag, delegate { OnDragStart(slot); });
		AddEvent(slot, EventTriggerType.EndDrag, delegate { OnDragEnd(slot); });
		AddEvent(slot, EventTriggerType.Drag, delegate { OnDrag(slot); });

		return slot;
	}
}
