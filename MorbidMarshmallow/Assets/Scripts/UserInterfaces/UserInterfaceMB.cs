/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInterfaceMB : MonoBehaviour
{
	#region Unity Functions
	void Start()
	{
		AddEvents();
	}

	private void AddEvents()
	{
		AddEvent(this.gameObject, EventTriggerType.PointerEnter, delegate { MouseObject.OnEnterInterface(this.gameObject); });
		AddEvent(this.gameObject, EventTriggerType.PointerExit, delegate { MouseObject.OnExitInterface(); });
	}

	protected void AddEvent(GameObject gameObject, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}
	#endregion
}