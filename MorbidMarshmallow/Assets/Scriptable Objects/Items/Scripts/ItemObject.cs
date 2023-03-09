using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
	Food,
	Helmet,
	Weapon,
	Shield,
	Boots,
	Chest,
	Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{
	public Sprite uiDisplay;
	public bool stackable;
	public ItemType type;
	[TextArea(15,20)]
	public string description;
	public Item data = new Item();
}

[System.Serializable]
public class Item
{
	public string Name;
	public int Id = -1;
	public Item() { }
	public Item(ItemObject item)
	{
		Name = item.name;
		Id = item.data.Id;
	}
}