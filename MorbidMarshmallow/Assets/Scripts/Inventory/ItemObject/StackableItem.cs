/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

public class StackableItem : BaseItem
{
	public StackableItem(ItemSO itemSO) : base(itemSO)
	{
		Stackable = true;
	}
}
