/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
	public ItemSO[] ItemObjects;

	//checkt of de Id's nogsteeds overeenkomen
	[ContextMenu("Update ID's")]
	public void UpdateID()
	{
		for (int i = 0; i < ItemObjects.Length; i++)
		{
			if (ItemObjects[i].id != i)
				ItemObjects[i].id = i;
		}
	}

	public ItemSO GetItemObject(int id)
	{
		foreach (var itemObject in ItemObjects)
		{
			if(itemObject.id == id)
				return itemObject;
		}
		return null;
	}

	public void OnAfterDeserialize()
	{
		UpdateID();
	}

	public void OnBeforeSerialize()
	{

	}
}
