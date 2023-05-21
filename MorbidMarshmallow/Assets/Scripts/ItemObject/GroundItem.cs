using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
	[SerializeField]
	public ItemSO itemSO;
	//public int amount = 1;
	public bool looted = false;

	public GroundItem(ItemSO itemSO)
	{ 
		this.itemSO = itemSO;
	}

	public void OnAfterDeserialize()
	{
		
	}

	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR
		GetComponentInChildren<SpriteRenderer>().sprite = itemSO.sprite;
		EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
	}

	public static GameObject Create(ItemSO itemSO)
	{
		var newGroundItem = CreateNewGroundItem(itemSO);
		var spawnPos = SetSpawnPosition();
		newGroundItem.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);

		CreateSpriteInChild(newGroundItem, itemSO);

		return newGroundItem;
	}

	private static void CreateSpriteInChild(GameObject groundItem, ItemSO itemSO) //GameObject groundItem, ItemObject itemObject
	{
		var childObject = new GameObject() { name = "Sprite" };

		childObject.AddComponent<SpriteRenderer>();
		childObject.GetComponent<SpriteRenderer>().sprite = itemSO
			.sprite;
		childObject.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

		childObject.transform.parent = groundItem.transform;
		childObject.transform.position = groundItem.transform.position;
		childObject.transform.localScale = new Vector3(4, 4, 1);
	}

	private static GameObject CreateNewGroundItem(ItemSO itemSO)
	{
		var newGroundItem = new GameObject { name = "Item" };
		newGroundItem.transform.parent = GameObject.Find("Collectables").transform;
		newGroundItem.AddComponent<BoxCollider2D>();
		newGroundItem.GetComponent<BoxCollider2D>().isTrigger = true;

		var groundItemObject = newGroundItem.AddComponent<GroundItem>();
		groundItemObject.itemSO = itemSO;
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
