/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInterfaceMB : MonoBehaviour
{
	//public InventorySO inventory;
	//public Dictionary<GameObject, InventorySlot> slotsOnInterface = new();

	//Used in/came from Description
	//public Transform parent;
	//public GameObject descriptionPrefab;

	//private bool _dragging = false;
	//private readonly int _offset = 100;


	void Start()
	{
		//CreateSlots();
		//SetSlotInfo();

		AddEvents();
	}

	#region commented Functions
	//public abstract void CreateSlots();

	//?naam
	//private void SetSlotInfo()
	//{
	//	for (int i = 0; i < inventory.Slots.Length; i++)
	//	{
	//		inventory.Slots[i].parent = this;
	//		inventory.Slots[i].OnAfterUpdate += OnSlotUpdate;
	//	}
	//}

	#endregion

	private void AddEvents()
	{
		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { MouseObject.OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { MouseObject.OnExitInterface(gameObject); });
	}

	protected void AddEvent(GameObject gameObject, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}
}