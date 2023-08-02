/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

public class UnStackableItem : BaseItem
{
	public UnStackableItem(ItemSO itemSO) : base(itemSO)
	{
		Stackable = false;
	}
}
