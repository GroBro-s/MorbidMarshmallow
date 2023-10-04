/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEditor;
using UnityEngine;

public class GroundItemMB : MonoBehaviour, ISerializationCallbackReceiver
{	
	private static Transform _collectables;
	private static GameObject _itemPrefab;

	public ItemSO itemSO;

	private void Start()
	{
		//var gameController = GameObject.Find("GameController");
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		_itemPrefab = gameStatsMB.groundItemPrefab;
		_collectables = gameStatsMB.collectables;
	}

	public GroundItemMB(ItemSO itemSO)
	{
		this.itemSO = itemSO;
	}

	public static GameObject Create(ItemSO itemSO)
	{
		var newGroundItem = Instantiate(_itemPrefab, GetSpawnPosition(), Quaternion.identity, _collectables);

		newGroundItem.GetComponent<GroundItemMB>().itemSO = itemSO;
		newGroundItem.GetComponent<SpriteRenderer>().sprite = itemSO.sprite;

		return newGroundItem;
	}

	private static Vector2 GetSpawnPosition()
	{
		var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		var offset = GetOffset();

		return new Vector2(playerPos.x + offset, playerPos.y);
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







//newGroundItem.transform.SetPositionAndRotation(GetSpawnPosition(), Quaternion.identity);

//var newGroundItem = CreateNewGroundItem(itemSO);
//CreateSpriteInChild(newGroundItem.transform, itemSO);



//private static GameObject CreateNewGroundItem(ItemSO itemSO)
//{
//	var newGroundItem = new GameObject { name = "Item" }; //ItemSO kan in 1 keer worden toegevoegd met functie zie regel 12

//	SetCollectablesAsParent(newGroundItem);

//	AddBoxCollider2DWithTrigger(newGroundItem);
//	AddGroundItemWithItemSO(newGroundItem, itemSO); //kan dit in 1 keer?
//													//groundItemObject.amount = slot.amount;

//	CreateSpriteInChild(newGroundItem.transform, itemSO);

//	return newGroundItem;
//}

//private static Transform SetCollectablesAsParent(GameObject newGroundItem)
//{
//	return newGroundItem.transform.parent = GameObject.Find("Collectables").transform;
//}

//private static GameObject AddBoxCollider2DWithTrigger(GameObject newGroundItem)
//{
//	newGroundItem.AddComponent<BoxCollider2D>().isTrigger = true;
//	return newGroundItem;
//}

//private static GameObject AddGroundItemWithItemSO(GameObject newGroundItem, ItemSO itemSO)
//{
//	newGroundItem.AddComponent<GroundItemMB>().itemSO = itemSO;
//	return newGroundItem;
//}

//private static void CreateSpriteInChild(Transform groundItemTransform, ItemSO itemSO) //GameObject groundItem, ItemObject itemObject //GameObject groundItem of Transform groundItemTransform??
//{
//	var childObject = new GameObject() { name = "Sprite" };

//	AddSpriteRenderer(childObject, itemSO);
//	AddTransformInfo(childObject.transform, groundItemTransform);
//}

//private static GameObject AddSpriteRenderer(GameObject childObject, ItemSO itemSO)
//{
//	childObject.AddComponent<SpriteRenderer>();
//	childObject.GetComponent<SpriteRenderer>().sprite = itemSO.sprite;
//	childObject.GetComponent<SpriteRenderer>().sortingLayerName = "Items";

//	return childObject;
//}

//private static Transform AddTransformInfo(Transform childTransform, Transform ParentTransform)
//{
//	childTransform.parent = ParentTransform;
//	childTransform.position = ParentTransform.position;
//	childTransform.localScale = new Vector3(4, 4, 1);

//	return childTransform;
//}
