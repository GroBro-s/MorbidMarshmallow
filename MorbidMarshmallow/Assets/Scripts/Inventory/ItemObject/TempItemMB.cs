/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;
using UnityEngine.UI;

public class TempItemMB : MonoBehaviour
{
	#region variables
	private static GameObject _tempItemPrefab;
	private static Transform _canvas;
	#endregion

	#region Unity Methods
	private void Start()
	{
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		_tempItemPrefab = gameStatsMB.tempItemPrefab;
		_canvas = gameStatsMB.canvas;
	}

	public static GameObject Create(IBaseItem itemBeingDragged) //de return null voelt niet zo goed aan mijn tenen. Moet hier nog iets anders komen te staan? Geeft dit geen errors?
	{
		if (itemBeingDragged.Id >= 0)
		{
			GameObject tempItem = Instantiate(_tempItemPrefab, Vector2.zero, Quaternion.identity, _canvas);
			tempItem.GetComponent<Image>().sprite = itemBeingDragged.ItemSO.sprite;
			return tempItem;
		}
		return null;
	}

	public static void Move(GameObject tempItemBeingDragged, Vector3 newPosition)
	{
		tempItemBeingDragged.GetComponent<RectTransform>().position = newPosition;
	}

	public static void Delete(GameObject tempItemBeingDragged)
	{
		Destroy(tempItemBeingDragged);
	}
	#endregion
}

//In dit script heb ik nu de methods niet meer static gemaakt, ik hoop dat ik zo geen asset reference meer nodig heb.
//Daarom is dat dus zo, maar als dit problemen oplevert moet je het weer terug veranderen. Vergeet niet dit script bij de gamecontroller te referencen.






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
