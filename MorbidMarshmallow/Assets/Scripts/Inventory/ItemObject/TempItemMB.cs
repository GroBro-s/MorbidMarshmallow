/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;
using UnityEngine.UI;

public class TempItemMB : MonoBehaviour
{
	#region variables
	private static GameObject tempItemPrefab;
	private static Transform canvas;
	#endregion

	#region Unity Methods
	private void Start()
	{
		var gameController = GameObject.Find("GameController");
		tempItemPrefab = gameController.GetComponent<GameStatsMB>().tempItemPrefab;
		canvas = gameController.GetComponent<GameStatsMB>().canvas;
	}

	public static GameObject Create(IBaseItem itemBeingDragged) //de return null voelt niet zo goed aan mijn tenen. Moet hier nog iets anders komen te staan? Geeft dit geen errors?
	{
		if (itemBeingDragged.Id >= 0)
		{
			GameObject tempItem = Instantiate(tempItemPrefab, Vector2.zero, Quaternion.identity);
			tempItem.transform.SetParent(canvas, false);
			tempItem.GetComponent<Image>().sprite = itemBeingDragged.ItemSO.sprite;
			return tempItem;
		}
		return null;
	}

	public static void Move(GameObject tempItemBeingDragged, Vector3 newPosition)
	{
		tempItemBeingDragged.GetComponent<RectTransform>().position = newPosition;
	}
	#endregion
}







//tempItem = new GameObject();
//AddRectTransform(tempItem, rectTransformSize);
//SetParent(tempItem, userInterface.transform);
//AddImage(tempItem, itemBeingDragged);
//var rt = tempItem.AddComponent<RectTransform>();
//rt.sizeDelta = new Vector2(50, 50);

//private static GameObject AddImage(GameObject tempItem, IBaseItem itemBeingDragged)
//{
//	var img = tempItem.AddComponent<Image>();
//	img.sprite = itemBeingDragged.Sprite;
//	img.raycastTarget = false;

//	return tempItem;
//}

//private static GameObject AddRectTransform(GameObject tempItem, Vector2 rectTransformSize)
//{
//	tempItem.AddComponent<RectTransform>().sizeDelta = rectTransformSize;
//	return tempItem;
//}

//private static GameObject SetParent(GameObject tempItem, Transform parentTransform)
//{
//	tempItem.transform.SetParent(parentTransform.parent);
//	return tempItem;
//}
