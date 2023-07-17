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
public class ItemSO : ScriptableObject
{
	public Sprite sprite;
	public bool stackable;
	public string itemName;
	public int id = -1;
	public ItemType type;
	[TextArea(15, 20)]
	public string description;

	public ItemSO(ItemSO itemSO)
	{
		
	}
}

public interface IBaseItem
{
	Sprite Sprite { get; set; }
	bool Stackable { get; set; }
	string ItemName { get; set; }
	int Id { get; set; }
	ItemType Type { get; set; }
	string Description { get; set; }
	ItemSO ItemSO { get; set; }
}

public class ItemObject
{
	public IBaseItem Item { get; private set; }
	public ItemObject(ItemSO itemSO)
	{
		switch (itemSO.stackable)
		{
			case false:
				Item = new UnStackableItem(itemSO);
				break;
			case true:
				Item = new StackableItem(itemSO);
				break;
		}
	}
}
public class StackableItem : BaseItem
{
	public StackableItem(ItemSO itemSO) : base(itemSO)
	{
		Stackable = true;
	}
}

public class UnStackableItem : BaseItem
{
	public UnStackableItem(ItemSO itemSO) : base(itemSO)
	{
		Stackable = false;
	}
}

public abstract class BaseItem : IBaseItem
{
	public Sprite Sprite { get; set; }
	public bool Stackable { get; set; }
	public string ItemName { get; set; }
	public int Id { get; set; }
	public ItemType Type { get; set; }
	public string Description { get; set; }
	public ItemSO ItemSO { get; set; }

	public BaseItem(ItemSO itemSO)
	{
		this.ItemName = itemSO.itemName;
		this.Id = itemSO.id;
		this.Type = itemSO.type;
		this.Description = itemSO.description;
		this.ItemSO = itemSO;
	}
}