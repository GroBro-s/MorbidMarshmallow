/*
* Grobros
* https://github.com/GroBro-s
*/
using UnityEngine;

public static class MouseObject
{
	public static UserInterfaceMB interfaceMouseIsOver;
	public static ParentSlotsMB slotMouseIsOver;
	public static GameObject tempItemBeingDragged;
	public static GameObject slotHoveredOver;

	public static Vector2 GetPosition()
	{
		return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	public static void OnEnterInterface(GameObject userInterface)
	{
		interfaceMouseIsOver = userInterface.GetComponent<UserInterfaceMB>();
	}

	public static void OnExitInterface(GameObject userInterface)
	{
		interfaceMouseIsOver = null;
	}
}

//Pas op, peter zegt dat de OnEnter/ExitInterface niet werkt. Dus als De Interface dingen niet werken, zal dit het probleem zijn.