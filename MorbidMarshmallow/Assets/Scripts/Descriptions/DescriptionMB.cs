/*
* Grobros
* https://github.com/GroBro-s
*/

using TMPro;
using UnityEngine;

public class DescriptionMB : MonoBehaviour
{
	#region variables
	public GameObject gameController;
	public static GameObject descriptionPrefab;
	public static Transform canvas;

	private static readonly int _xOffset = 110;
	private static readonly int _yOffset = 0;

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

	public static GameObject Create(GameObject slotGO, ItemObject itemObject)
	{
		var slotPosition = GetDescriptionPosition(slotGO);
		descriptionGO = Instantiate(descriptionPrefab, Vector2.zero, Quaternion.identity, canvas);

		var backGround = descriptionGO.transform.Find("BackGround");
		backGround.GetComponent<RectTransform>().position = slotPosition;

		descriptionGO.GetComponentInChildren<TextMeshProUGUI>().text = itemObject.Item.ItemSO.description;
		return descriptionGO;
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
	#endregion
}