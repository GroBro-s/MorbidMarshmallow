using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Description : MonoBehaviour
{
	//new is zelf toegevoegd, kan errors geven
	//public new TextMeshProUGUI name;
	public TextMeshProUGUI description;
	//public Image item;

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

	public void AssignValues( string _description) //string _name, , Sprite _itemSprite
	{
		//name.text = _name;
		description.text = _description;
		//item.sprite = _itemSprite;
	}
}
