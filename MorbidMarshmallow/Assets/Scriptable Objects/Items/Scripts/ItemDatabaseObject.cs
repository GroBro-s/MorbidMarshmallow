using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
	public ItemObject[] ItemObjects;

	//checkt of de Id's nogsteeds overeenkomen
	[ContextMenu("Update ID's")]
	public void UpdateID()
	{
		for (int i = 0; i < ItemObjects.Length; i++)
		{
			if (ItemObjects[i].Id != i)
				ItemObjects[i].Id = i;
		}
	}

	public ItemObject GetItemObject(int id)
	{
		foreach (var itemObject in ItemObjects)
		{
			if(itemObject.Id == id)
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
