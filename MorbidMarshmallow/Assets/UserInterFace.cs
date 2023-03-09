using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//using System;
using JetBrains.Annotations;
using System.Data.Common;

public abstract class UserInterface : MonoBehaviour
{
	public InventoryObject inventory;
	public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
	private GameObject _player;

	void Start()
	{
		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
			inventory.GetSlots[i].parent = this;
			inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;;
		}
		CreateSlots();
		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
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

	public abstract void CreateSlots();

	protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = obj.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}

	public void OnEnter(GameObject obj)
	{
		MouseData.slotHoveredOver = obj;
	}

	public void OnExit(GameObject obj)
	{
		MouseData.slotHoveredOver = null;
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
		Destroy(MouseData.tempItemBeingDragged);

		if(MouseData.interfaceMouseIsover == null)
		{
			if (slotsOnInterface[obj].item.Id >= 0)
			{
				Item _item = slotsOnInterface[obj].item;
				ItemObject itemObject = slotsOnInterface[obj].ItemObject;

				GameObject _player = GameObject.FindGameObjectWithTag("Player");

				for (int i = 0; i < slotsOnInterface[obj].amount; i++)
				{
					float offsetRange = Random.Range(-3f, 3f);

					while(offsetRange > -1 && offsetRange < 1)
					{
						offsetRange = Random.Range(-3f, 3f);
					}

					//float randomOffset = 
					//		offsetRange < 1 && offsetRange >= 0 ? offsetRange + 0.7f
					//		: offsetRange > -1 && offsetRange < 0 ? offsetRange - 0.7f
					//		: offsetRange;
					Vector2 spawnPos = new Vector2(_player.transform.position.x + offsetRange, _player.transform.position.y);	

					CreateNewGroundItem(_item, obj, itemObject, spawnPos);
				}

				slotsOnInterface[obj].RemoveItem();
				return;
			}
		}

		if(MouseData.slotHoveredOver)
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
		if(MouseData.tempItemBeingDragged != null)
		{
			MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
		}
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
