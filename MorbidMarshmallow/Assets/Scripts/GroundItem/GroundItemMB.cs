/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEditor;
using UnityEngine;

public class GroundItemMB : MonoBehaviour, ISerializationCallbackReceiver
{
	[SerializeField]
	public ItemSO itemSO;
	//public int amount = 1;
	public bool looted = false;

	public GroundItemMB(ItemSO itemSO)
	{
		this.itemSO = itemSO;
	}


	public static GameObject Create(ItemSO itemSO)
	{
		var newGroundItem = CreateNewGroundItem(itemSO);
		// spawnPos = GetSpawnPosition();

		newGroundItem.transform.SetPositionAndRotation(GetSpawnPosition(), Quaternion.identity);  //spawnPos <- heb ik uit de SetPositionAndRotation gehaald en in 1 keer gedaan, is dit handig?

		//CreateSpriteInChild(newGroundItem.transform, itemSO);

		return newGroundItem;
	}

	private static GameObject CreateNewGroundItem(ItemSO itemSO)
	{
		var newGroundItem = new GameObject { name = "Item" }; //ItemSO kan in 1 keer worden toegevoegd met functie zie regel 12

		SetCollectablesAsParent(newGroundItem);

		AddBoxCollider2DWithTrigger(newGroundItem);
		AddGroundItemWithItemSO(newGroundItem, itemSO); //kan dit in 1 keer?
														//groundItemObject.amount = slot.amount;

		CreateSpriteInChild(newGroundItem.transform, itemSO);

		return newGroundItem;
	}

	private static Transform SetCollectablesAsParent(GameObject newGroundItem)
	{
		return newGroundItem.transform.parent = GameObject.Find("Collectables").transform;
	}

	private static GameObject AddBoxCollider2DWithTrigger(GameObject newGroundItem)
	{
		newGroundItem.AddComponent<BoxCollider2D>().isTrigger = true;
		return newGroundItem;
	}

	private static GameObject AddGroundItemWithItemSO(GameObject newGroundItem, ItemSO itemSO)
	{
		newGroundItem.AddComponent<GroundItemMB>().itemSO = itemSO;
		return newGroundItem;
	}

	private static void CreateSpriteInChild(Transform groundItemTransform, ItemSO itemSO) //GameObject groundItem, ItemObject itemObject //GameObject groundItem of Transform groundItemTransform??
	{
		var childObject = new GameObject() { name = "Sprite" };

		AddSpriteRenderer(childObject, itemSO);
		AddTransformInfo(childObject.transform, groundItemTransform);
	}

	private static GameObject AddSpriteRenderer(GameObject childObject, ItemSO itemSO)
	{
		childObject.AddComponent<SpriteRenderer>();
		childObject.GetComponent<SpriteRenderer>().sprite = itemSO.sprite;
		childObject.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

		return childObject;
	}

	private static Transform AddTransformInfo(Transform childTransform, Transform ParentTransform)
	{
		childTransform.parent = ParentTransform;
		childTransform.position = ParentTransform.position;
		childTransform.localScale = new Vector3(4, 4, 1);

		return childTransform;
	}

	private static Vector2 GetSpawnPosition()
	{
		var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		var offset = GetOffset();

		return new Vector2(playerTransform.transform.position.x + offset, playerTransform.transform.position.y);
	}

	private static float GetOffset()
	{
		var offset = Random.Range(-3f, 3f);

		//while offset is less than 1 unit from the origin		
		while (offset > -1 && offset < 1)
		{
			offset = Random.Range(-3f, 3f);
		}

		return offset;
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
}