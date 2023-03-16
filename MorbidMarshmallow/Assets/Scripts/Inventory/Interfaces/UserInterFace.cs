using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UserInterface : MonoBehaviour
{
	public InventoryObject inventory;
	public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

	//Used in/came from Description
	public Transform parent;
	public GameObject descriptionPrefab;
	private GameObject _description;

	private bool _dragging = false;

	void Start()
	{
		CreateSlots();
		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
			inventory.GetSlots[i].parent = this;
			inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
		}

		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
	}

	private void Update()
	{
		if (_description)
		{
			var _pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			if (_pos.x > Screen.width - 200)
				_pos.x = Input.mousePosition.x - 100;
			else
				_pos.x = Input.mousePosition.x + 100;

			_pos.y = Input.mousePosition.y - 100;
			
			_description.transform.position = _pos;


			//if (_pos.y + 200 > Screen.height)
			//else
			//	_pos.y += 100;
		}
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
			InventorySlot hoveringItem = slotsOnInterface[obj];
			if (hoveringItem.item.Id >= 0)
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
		_dragging = false;
		Destroy(MouseData.tempItemBeingDragged);

		if (MouseData.interfaceMouseIsover == null && slotsOnInterface[obj].item.Id >= 0) //Goede aanpassing? (in plaats van 2 if's)
		{
			for (int i = 0; i < slotsOnInterface[obj].amount; i++)
				GroundItem.Create(slotsOnInterface[obj]);

			slotsOnInterface[obj].RemoveItem();
			return;
		}

		if (MouseData.slotHoveredOver)
		{
			InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsover.slotsOnInterface[MouseData.slotHoveredOver];
			inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
		}
	}

	public void OnDrag(GameObject obj)
	{
		_dragging = true;
		if (MouseData.tempItemBeingDragged != null)
			MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
	}
}