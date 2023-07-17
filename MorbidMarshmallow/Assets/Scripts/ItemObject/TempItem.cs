/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;
using UnityEngine.UI;

public class TempItem : MonoBehaviour
{
	#region Unity Methods
	public static GameObject Create(UserInterfaceMB userInterface, IBaseItem itemBeingDragged) //GameObject obj, 
	{
		GameObject tempItem = null;
		Vector2 rectTransformSize = new(50, 50);

		if (itemBeingDragged.Id >= 0)
		{
			tempItem = new GameObject();
			AddRectTransform(tempItem, rectTransformSize);
			SetParent(tempItem, userInterface.transform);
			AddImage(tempItem, itemBeingDragged);
			//var rt = tempItem.AddComponent<RectTransform>();
			//rt.sizeDelta = new Vector2(50, 50);
		}
		return tempItem;
	}

	public static void Move(GameObject tempItemBeingDragged, Vector3 newPosition)
	{
		tempItemBeingDragged.GetComponent<RectTransform>().position = newPosition;
	}

	private static GameObject AddImage(GameObject tempItem, IBaseItem itemBeingDragged)
	{
		var img = tempItem.AddComponent<Image>();
		img.sprite = itemBeingDragged.Sprite;
		img.raycastTarget = false;

		return tempItem;
	}

	private static GameObject AddRectTransform(GameObject tempItem, Vector2 rectTransformSize)
	{
		tempItem.AddComponent<RectTransform>().sizeDelta = rectTransformSize;
		return tempItem;
	}

	//Kan deze functie nog anders? (de return in 1 regel geven)
	private static GameObject SetParent(GameObject tempItem, Transform parentTransform)
	{
		tempItem.transform.SetParent(parentTransform.parent);
		return tempItem;
	}
	#endregion
}
