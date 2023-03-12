using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Licker : MonoBehaviour
{
	public GameObject inventory;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		inventory.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		inventory.SetActive(false);
	}
}
