/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

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
