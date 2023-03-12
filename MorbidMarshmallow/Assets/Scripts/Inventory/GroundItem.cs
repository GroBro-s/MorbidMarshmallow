using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
	public ItemObject itemObject;
	public Item item;
	public int amount = 1;
	public bool looted = false;

	public GroundItem(ItemObject item)
	{ 
		this.itemObject = item;
	}

	public void OnAfterDeserialize()
	{
		
	}

	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR
		GetComponentInChildren<SpriteRenderer>().sprite = itemObject.uiDisplay;
		EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
	}

	public static GameObject Create(InventorySlot slot)
	{
		var newGroundItem = CreateNewGroundItem(slot);
		var spawnPos = SetSpawnPosition();
		newGroundItem.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);

		CreateSpriteInChild(newGroundItem, slot);

		return newGroundItem;
	}

	private static void CreateSpriteInChild(GameObject groundItem, InventorySlot slot) //GameObject groundItem, ItemObject itemObject
	{
		var childObject = new GameObject() { name = "Sprite" };

		childObject.AddComponent<SpriteRenderer>();
		childObject.GetComponent<SpriteRenderer>().sprite = slot.ItemObject.uiDisplay;

		childObject.transform.parent = groundItem.transform;
		childObject.transform.position = groundItem.transform.position;
		childObject.transform.localScale = new Vector3(4, 4, 1);
	}

	private static GameObject CreateNewGroundItem(InventorySlot slot)
	{
		var newGroundItem = new GameObject { name = "Item" };
		newGroundItem.AddComponent<BoxCollider2D>();
		newGroundItem.GetComponent<BoxCollider2D>().isTrigger = true;

		var groundItemObject = newGroundItem.AddComponent<GroundItem>();
		var itemObject = slot.ItemObject;
		groundItemObject.item = itemObject.item;
		groundItemObject.itemObject = itemObject;
		groundItemObject.amount = slot.amount;

		return newGroundItem;
	}

	private static Vector2 SetSpawnPosition()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var offsetRange = UnityEngine.Random.Range(-3f, 3f);

		while (offsetRange > -1 && offsetRange < 1)
		{
			offsetRange = UnityEngine.Random.Range(-3f, 3f);
		}
		return new Vector2(player.transform.position.x + offsetRange, player.transform.position.y);
	}
}
