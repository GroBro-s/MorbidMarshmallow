using UnityEngine;

public enum ItemType
{
	SugarCane,
	SharpSugarcane,
	Weight,
	HeavyWeight,
	Mushroom,
	PoisenousMushroom,
	Food,
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
	public Item item = new Item();
}

[System.Serializable]
public class Item
{
	public string Name;
	public int Id = -1;
	public ItemType Type;
	public Item() { }
	public Item(Item item)
	{
		Name = item.Name;
		Id = item.Id;
		Type = item.Type;
	}
}