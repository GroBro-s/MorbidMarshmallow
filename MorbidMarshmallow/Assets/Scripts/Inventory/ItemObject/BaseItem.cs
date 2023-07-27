/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;

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
		this.Sprite = itemSO.sprite;
		this.ItemSO = itemSO;
	}
}