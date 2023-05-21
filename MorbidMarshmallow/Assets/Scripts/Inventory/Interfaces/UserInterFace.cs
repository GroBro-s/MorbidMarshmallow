/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UserInterface : MonoBehaviour
{
	public InventorySO inventory;
	public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

	//Used in/came from Description
	public Transform parent;
	public GameObject descriptionPrefab;
	private GameObject _description;

	private bool _dragging = false;

	void Start()
	{
		CreateSlots();
		SetSlotInfo();

		AddEvents();
	}

	private void Update()
	{
		if (_description)
		{
			SetDescriptionPosition();
		}
	}

	private void SetDescriptionPosition()
	{
		var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		if (pos.x > Screen.width - 200)
		{
			pos.x = Input.mousePosition.x - 100;
		}
		else
		{
			pos.x = Input.mousePosition.x + 100;
		}

		pos.y = Input.mousePosition.y - 100;

		_description.transform.position = pos;


		//if (pos.y + 200 > Screen.height)
		//else
		//	pos.y += 100;
	}

	//?naam
	private void SetSlotInfo()
	{
		for (int i = 0; i < inventory.Slots.Length; i++)
		{
			inventory.Slots[i].parent = this;
			inventory.Slots[i].OnAfterUpdate += OnSlotUpdate;
		}
	}

	private void AddEvents()
	{
		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
	}

	public abstract void CreateSlots();

	protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = obj.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}

	private void OnSlotUpdate(InventorySlot slot)
	{
		slot.UpdateSlotDisplay();
	}

	public void OnEnter(GameObject obj)
	{
		MouseData.slotHoveredOver = obj;
		if (!_dragging)
		{
			var hoveringItem = slotsOnInterface[obj];
			var hoveringItemObject = hoveringItem.ItemObject;

			if (hoveringItemObject.Item.Id >= 0)
				_description = Description.Create(this, hoveringItem);
		}
	}

	public void OnExit(GameObject obj)
	{
		MouseData.slotHoveredOver = null;

		Description.DestroyDescription(_description);
	}

	public void OnEnterInterface(GameObject obj)
	{
		MouseData.interfaceMouseIsover = obj.GetComponent<UserInterface>();
	}

	public void OnExitInterface(GameObject obj)
	{
		MouseData.interfaceMouseIsover = null;
	}

	public void OnDragStart(GameObject obj)
	{
		MouseData.tempItemBeingDragged = TempItem.Create(this, slotsOnInterface[obj]);
	}

	public void OnDragEnd(GameObject obj)
	{
		var slot = slotsOnInterface[obj];
		var itemObject = slot.ItemObject;

		_dragging = false;
		Destroy(MouseData.tempItemBeingDragged);

		if (MouseData.interfaceMouseIsover == null && itemObject.Item.Id >= 0) //Goede aanpassing? (in plaats van 2 if's)
		{
			for (int i = 0; i < slot.amount; i++)
			{
				GroundItem.Create(itemObject);
			}

			slot.ClearSlot();
			return;
		}

		if (MouseData.slotHoveredOver)
		{
			SwapItems(slot);
		}
	}

	public void OnDrag(GameObject obj)
	{
		_dragging = true;
		if (MouseData.tempItemBeingDragged != null)
		{
			MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
		}
	}

	private void SwapItems(InventorySlot slot)
	{
		InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsover.slotsOnInterface[MouseData.slotHoveredOver];
		
		inventory.SwapSlots(slot, mouseHoverSlotData);
	}
}