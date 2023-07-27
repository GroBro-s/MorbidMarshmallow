/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;

public class StackableItem : BaseItem
{
	public StackableItem(ItemSO itemSO) : base(itemSO)
	{
		Stackable = true;
	}
}
