using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
	public InventoryObject inventory;
	public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

	public Transform parent;
	GameObject description;
	public GameObject descriptionPrefab;
	private Transform canvas;
	private bool dragging = false;

	TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

	void Start()
	{
		CreateSlots();
		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
			inventory.GetSlots[i].parent = this;
			inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate; ;
		}

		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
	}

	private void Update()
	{
		if (description)
		{
			Vector2 _pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			if (_pos.x > Screen.width - 10)
				_pos.x = Input.mousePosition.x - 100;
			else
				_pos.x = Input.mousePosition.x + 60;

			//if (_pos.y + 100 > Screen.height)
			//	_pos.y = Screen.height - 100;
			//else
			//	_pos.y += 100;

			description.transform.position = _pos;
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

	private void OnSlotUpdate(InventorySlot _slot)
	{
		if (_slot.item.Id >= 0)
		{
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
			_slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
		}
		else
		{
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
			_slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
		}
	}
	public void OnEnter(GameObject obj)
	{
		MouseData.slotHoveredOver = obj;
		if (!dragging)
		{
			InventorySlot hoveringItem = slotsOnInterface[obj];
			if (hoveringItem.item.Id >= 0)
			{
				{
					CreateDescription(hoveringItem);
				}
			}
		}
	}

	public void OnExit(GameObject obj)
	{
		MouseData.slotHoveredOver = null;
		if (description != null)
		{
			Destroy(description);
		}
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
		MouseData.tempItemBeingDragged = CreateTempItem(obj);
	}

	public GameObject CreateTempItem(GameObject obj)
	{
		GameObject tempItem = null;
		if (slotsOnInterface[obj].item.Id >= 0)
		{
			tempItem = new GameObject();
			var rt = tempItem.AddComponent<RectTransform>();
			rt.sizeDelta = new Vector2(50, 50);
			tempItem.transform.SetParent(transform.parent);
			var img = tempItem.AddComponent<Image>();
			img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
			img.raycastTarget = false;
		}
		return tempItem;
	}

	public void OnDragEnd(GameObject obj)
	{
		dragging = false;
		Destroy(MouseData.tempItemBeingDragged);

		if (MouseData.interfaceMouseIsover == null)
		{
			if (slotsOnInterface[obj].item.Id >= 0)
			{
				Item _item = slotsOnInterface[obj].item;
				ItemObject itemObject = slotsOnInterface[obj].ItemObject;

				GameObject _player = GameObject.FindGameObjectWithTag("Player");

				for (int i = 0; i < slotsOnInterface[obj].amount; i++)
				{
					float offsetRange = UnityEngine.Random.Range(-3f, 3f);

					while (offsetRange > -1 && offsetRange < 1)
					{
						offsetRange = UnityEngine.Random.Range(-3f, 3f);
					}
					Vector2 spawnPos = new Vector2(_player.transform.position.x + offsetRange, _player.transform.position.y);

					CreateNewGroundItem(_item, obj, itemObject, spawnPos);
				}

				slotsOnInterface[obj].RemoveItem();
				return;
			}
		}

		if (MouseData.slotHoveredOver)
		{
			InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsover.slotsOnInterface[MouseData.slotHoveredOver];
			inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
		}
	}

	public GameObject CreateNewGroundItem(Item _item, GameObject obj, ItemObject itemObject, Vector2 spawnPos)
	{
		var newGroundItem = new GameObject() { name = "Item" };

		var groundItemObject = newGroundItem.AddComponent<GroundItem>();
		newGroundItem.AddComponent<BoxCollider2D>();
		newGroundItem.GetComponent<BoxCollider2D>().isTrigger = true;

		groundItemObject.ItemData = _item;
		groundItemObject.item = itemObject;
		groundItemObject.amount = slotsOnInterface[obj].amount;
		groundItemObject.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);

		CreateSpriteInChild(newGroundItem, itemObject);
		return newGroundItem;
	}

	public void CreateSpriteInChild(GameObject parentObject, ItemObject itemObject)
	{
		var childObject = new GameObject() { name = "Sprite" };

		childObject.AddComponent<SpriteRenderer>();
		childObject.GetComponent<SpriteRenderer>().sprite = itemObject.uiDisplay;

		childObject.transform.parent = parentObject.transform;
		childObject.transform.position = parentObject.transform.position;
		childObject.transform.localScale = new Vector3(4, 4, 1);
	}

	public void OnDrag(GameObject obj)
	{
		dragging = true;
		if (MouseData.tempItemBeingDragged != null)
		{
			MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
		}
	}

	void CreateDescription(InventorySlot hoveringItem)
	{
		Transform _trans;
		if (parent == null)
			_trans = canvas;
		else
			_trans = parent;

		description = Instantiate(descriptionPrefab, Vector2.zero, Quaternion.identity, _trans);
		var _item = hoveringItem.item;
		var _description = hoveringItem.ItemObject.description;
		var _uiDisplay = hoveringItem.ItemObject.uiDisplay;

		description.GetComponent<Description>().AssignValues(_item.Name, _description , _uiDisplay);
	}
}

public static class MouseData
{
	public static UserInterface interfaceMouseIsover;
	public static GameObject tempItemBeingDragged;
	public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
	public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
	{
		foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
		{
			if (_slot.Value.item.Id >= 0)
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
			}
			else
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
			}
		}
	}
}
