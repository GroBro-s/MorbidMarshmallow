/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
using UnityEngine;

public class DescriptionMB : MonoBehaviour
{
	#region variables
	public static GameObject descriptionPrefab;
	private static int _offset = 100;

	private DescriptionMB _instance;
    public static GameObject descriptionGO;
	#endregion

	#region unity functions
	private void Start()
	{
		CheckInstance();
	}

	private void CheckInstance()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			print("Error: There are multiple descriptions present, deleting old one");
			Destroy(_instance);
			_instance = this;
		}
	}

	private void OnDisable()
	{
		_instance = null;
	}

	public static GameObject Create(GameObject slotGO) //UserInterfaceMB userInterface, 
	{
		//var parent = userInterface.parent;
		var slotPosition = SetDescriptionPosition(slotGO);
		descriptionGO = Instantiate(descriptionPrefab, slotPosition, Quaternion.identity); //userInterface.descriptionPrefab, parent
		return descriptionGO;
		//var _uiDisplay = hoveringItem.ItemObject.uiDisplay;
	}

	public static void DestroyDescription()
	{
		descriptionGO = null;
	}

	private static Vector2 SetDescriptionPosition(GameObject slotGO)
	{
		float outerScreenBorder = Screen.width - 200;

		var slotPosition = slotGO.transform.position;

		slotPosition.x = slotPosition.x > outerScreenBorder
			? slotPosition.x -= _offset
			: slotPosition.x += _offset;
		slotPosition.y = slotPosition.y -= _offset;

		return slotPosition;
	}
	#endregion
}

//private static void SetDescriptionPosition(GameObject slotGO)
//{
//	var mousePos = MouseObject.GetPosition();
//	float outerScreenBorder = Screen.width - 200;

//	descriptionGO.transform.position = new Vector2
//	{
//		x = mousePos.x > outerScreenBorder 
//			? mousePos.x -= _offset 
//			: mousePos.x += _offset,
//		y = mousePos.y -= _offset
//	};
//}
//private void Update()
//{
//	if (descriptionGO != null)
//	{
//		SetDescriptionPosition(slot);
//	}
//}

//public class DescriptionObject
//{
//	#region variables
//	public TextMeshProUGUI text;
//	public DescriptionMB descriptionMB;
//	public GameObject descriptionGO;
//	#endregion

//	#region unity functions
//	public DescriptionObject(ItemObject itemObject)
//	{
//		var itemDescription = itemObject.Item.Description;

//		descriptionGO = DescriptionMB.Create(itemObject);
//		descriptionGO.GetComponent<DescriptionObject>().text.text = itemDescription;

//		//descriptionGO.GetComponent<DescriptionObject>().AssignValues(itemDescription);  //_item.Name, , _uiDisplay		
//	}
//	#endregion
//}

//public void AssignValues( string _description) 
//{
//	text.text = _description;

//	//string _name, , Sprite _itemSprite
//	//name.text = _name;
//	//item.sprite = _itemSprite;
//}

//private void SetPosition()
//{
//	var mousePos = MouseObject.GetPosition();

//	descriptionGO.transform.position = SetDescriptionOffset(mousePos);
//}

//private Vector2 SetDescriptionOffset(Vector2 pos)
//{
//	float xOffset = SetDescriptionXPosition(pos.x, _offset);
//	pos.x = xOffset;

//	float yOffset = SetDescriptionYPosition(pos.y, _offset);
//	pos.y = yOffset;

//	return pos;
//}
//private float SetDescriptionXPosition(float xPosition, int offset)
//{
//	float outerScreenBorder = Screen.width - 200;
//	if (xPosition > outerScreenBorder)
//	{
//		return xPosition -= offset;
//	}
//	else
//	{
//		return xPosition += offset;
//	}
//}

//private float SetDescriptionYPosition(float yPosition, int offset)
//{
//	return yPosition -= offset;
//}

