/*
* Grobros
* https://github.com/GroBro-s
*/

using TMPro;
using UnityEngine;

public class DescriptionMB : MonoBehaviour
{
	#region variables
	private GameObject gameController;

	private static GameObject descriptionPrefab;
	private static Transform canvas;
    private static GameObject descriptionGO;

	private static readonly int _xOffset = 110;
	private static readonly int _yOffset = 0;

	private DescriptionMB _instance;
	#endregion

	#region unity functions
	private void Start()
	{
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();

		descriptionPrefab = gameStatsMB.descriptionPrefab;
		canvas = gameStatsMB.canvas;

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

	public static GameObject Create(Vector2 slotPos, string descriptionText)
	{
		descriptionGO = Instantiate(descriptionPrefab, Vector2.zero, Quaternion.identity, canvas);
		descriptionGO.GetComponentInChildren<TextMeshProUGUI>().text = descriptionText;

		var backGround = descriptionGO.transform.Find("BackGround");
		backGround.GetComponent<RectTransform>().position = GetDescriptionPosition(slotPos);

		return descriptionGO;
	}

	public static void DestroyDescription()
	{
		Destroy(descriptionGO);
		//descriptionGO = null;
	}

	private static Vector2 GetDescriptionPosition(Vector2 slotPos)
	{
		float outerScreenBorder = Screen.width - 200;

		var descriptionPos = slotPos;

		descriptionPos.x = descriptionPos.x > outerScreenBorder
			? descriptionPos.x -= _xOffset
			: descriptionPos.x += _xOffset;
		descriptionPos.y = descriptionPos.y -= _yOffset;

		return descriptionPos;
	}
	#endregion
}