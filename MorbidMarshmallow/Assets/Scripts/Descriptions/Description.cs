using UnityEngine;
using TMPro;

public class Description : MonoBehaviour
{
	public TextMeshProUGUI description;
	public static Description Instance;

	private void Start()
	{
		if (Instance == null)
			Instance = this;
		else
		{
			print("Error: There are multiple descriptions present, deleting old one");
			Destroy(Instance);
			Instance = this;
		}
	}

	private void OnDisable()
	{
		Instance = null;
	}

	public static GameObject Create(UserInterface userInterface, InventorySlot hoveringItem)
	{
		var _trans = userInterface.parent;
		var _description = hoveringItem.ItemObject.description;
		//var _item = hoveringItem.item;

		var description = Instantiate(userInterface.descriptionPrefab, Vector2.zero, Quaternion.identity, _trans);
		//var _uiDisplay = hoveringItem.ItemObject.uiDisplay;

		description.GetComponent<Description>().AssignValues(_description);  //_item.Name, , _uiDisplay

		return description;
	}


	public void AssignValues( string _description) 
	{
		description.text = _description;
		
		//string _name, , Sprite _itemSprite
		//name.text = _name;
		//item.sprite = _itemSprite;
	}

	public static void DestroyDescription(GameObject description)
	{
		if (description != null) 
			Destroy(description);
	}
}
