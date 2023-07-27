/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;

public class UnStackableItem : BaseItem
{
	public UnStackableItem(ItemSO itemSO) : base(itemSO)
	{
		Stackable = false;
	}
}
