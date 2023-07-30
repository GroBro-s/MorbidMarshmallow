/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
using TMPro;
using UnityEngine;

public class DescriptionMB : MonoBehaviour
{
	#region variables
	public GameObject gameController;
	public static GameObject descriptionPrefab;
	public static Transform canvas;

	private static int _xOffset = 110;
	private static int _yOffset = 0;

	private DescriptionMB _instance;
    public static GameObject descriptionGO;

	public TextMeshProUGUI text;
	#endregion

	#region unity functions
	private void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController");

		var gameStats = gameController.GetComponent<GameStatsMB>();
		descriptionPrefab = gameStats.descriptionPrefab;
		canvas = gameStats.canvas;

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

	public static GameObject Create(GameObject slotGO, ItemObject itemObject) //UserInterfaceMB userInterface, 
	{
		//var parent = userInterface.parent;
		//var itemDescription = slotGO.
		var slotPosition = GetDescriptionPosition(slotGO);
		descriptionGO = Instantiate(descriptionPrefab, Vector2.zero, Quaternion.identity, canvas); //userInterface.descriptionPrefab, parent

		var backGround = descriptionGO.transform.Find("BackGround");
		backGround.GetComponent<RectTransform>().position = slotPosition;

		descriptionGO.GetComponentInChildren<TextMeshProUGUI>().text = itemObject.Item.ItemSO.description;
		//descriptionGO.GetComponent<DescriptionMB>().AssignValues(itemDescription);
		return descriptionGO;
		//var _uiDisplay = hoveringItem.ItemObject.uiDisplay;
	}

	public static void DestroyDescription()
	{
		Destroy(descriptionGO);
		//descriptionGO = null;
	}

	private static Vector2 GetDescriptionPosition(GameObject slotGO)
	{
		float outerScreenBorder = Screen.width - 200;

		var slotPosition = slotGO.transform.position;

		slotPosition.x = slotPosition.x > outerScreenBorder
			? slotPosition.x -= _xOffset
			: slotPosition.x += _xOffset;
		slotPosition.y = slotPosition.y -= _yOffset;

		return slotPosition;
	}
	
	public void AssignValues(string _description)
	{
		text.text = _description;

		//string _name, , Sprite _itemSprite
		//name.text = _name;
		//item.sprite = _itemSprite;
	}
	#endregion
}

//public class DescriptionObject
//{
//	#region variables
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

