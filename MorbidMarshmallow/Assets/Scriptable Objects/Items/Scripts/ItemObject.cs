/*
* Grobros
* https://github.com/GroBro-s
*/

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
[System.Serializable]
public class ItemObject : ScriptableObject
{
	public Sprite UiDisplay;
	public bool Stackable;
	public string Name;
	public int Id = -1;
	public ItemType Type;
	[TextArea(15, 20)]
	public string description;

	public ItemObject() { }

	public ItemObject(ItemObject item)
	{
		Name = item.Name;
		Id = item.Id;
		Type = item.Type;
	}
}