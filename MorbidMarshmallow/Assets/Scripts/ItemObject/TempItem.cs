/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class TempItem : MonoBehaviour
{
	#region Unity Methods
	public static GameObject Create(UserInterface userInterface, InventorySlot inventorySlot) //GameObject obj, 
	{
		GameObject tempItem = null;
		if (inventorySlot.ItemObject.Id >= 0)
		{
			tempItem = new GameObject();
			var rt = tempItem.AddComponent<RectTransform>();
			rt.sizeDelta = new Vector2(50, 50);
			tempItem.transform.SetParent(userInterface.transform.parent);
			var img = tempItem.AddComponent<Image>();
			img.sprite = inventorySlot.ItemObject.UiDisplay;
			img.raycastTarget = false;
		}
		return tempItem;
	}

	#endregion
}
