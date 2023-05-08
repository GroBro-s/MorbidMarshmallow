using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
	public ItemObject itemObject;
	//public int amount = 1;
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
		GetComponentInChildren<SpriteRenderer>().sprite = itemObject.UiDisplay;
		EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
	}

	public static GameObject Create(ItemObject itemObject)
	{
		var newGroundItem = CreateNewGroundItem(itemObject);
		var spawnPos = SetSpawnPosition();
		newGroundItem.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);

		CreateSpriteInChild(newGroundItem, itemObject);

		return newGroundItem;
	}

	private static void CreateSpriteInChild(GameObject groundItem, ItemObject itemObject) //GameObject groundItem, ItemObject itemObject
	{
		var childObject = new GameObject() { name = "Sprite" };

		childObject.AddComponent<SpriteRenderer>();
		childObject.GetComponent<SpriteRenderer>().sprite = itemObject.UiDisplay;
		childObject.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

		childObject.transform.parent = groundItem.transform;
		childObject.transform.position = groundItem.transform.position;
		childObject.transform.localScale = new Vector3(4, 4, 1);
	}

	private static GameObject CreateNewGroundItem(ItemObject itemObject)
	{
		var newGroundItem = new GameObject { name = "Item" };
		newGroundItem.transform.parent = GameObject.Find("Collectables").transform;
		newGroundItem.AddComponent<BoxCollider2D>();
		newGroundItem.GetComponent<BoxCollider2D>().isTrigger = true;

		var groundItemObject = newGroundItem.AddComponent<GroundItem>();
		groundItemObject.itemObject = itemObject;
		//groundItemObject.amount = slot.amount;

		return newGroundItem;
	}

	private static Vector2 SetSpawnPosition()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var offsetRange = Random.Range(-3f, 3f);

		while (offsetRange > -1 && offsetRange < 1)
		{
			offsetRange = Random.Range(-3f, 3f);
		}
		return new Vector2(player.transform.position.x + offsetRange, player.transform.position.y);
	}
}
