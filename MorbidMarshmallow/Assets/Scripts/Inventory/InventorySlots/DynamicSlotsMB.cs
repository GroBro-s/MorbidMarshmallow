/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
using UnityEngine;

public class DynamicSlotsMB : ParentSlotsMB
{
	public GameObject slotPrefab;
	private readonly int X_START = -90;
	private readonly int Y_START = 160;
	private readonly int X_SPACE_BETWEEN_ITEM = 60;
	private readonly int NUMBER_OF_COLUMN = 4;
	private readonly int Y_SPACE_BETWEEN_ITEM = 60;

	public override void CreateSlots()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			var slotGO = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
			slotGO.GetComponent<RectTransform>().localPosition = GetPosition(i);
			AddEvents(slotGO);

			//Let op hier is nog een error
			var slot = slots[i];
			slot = new InventorySlot()
			{
				slotGO = slotGO,
				parent = this
			};
			slot.OnAfterUpdate += OnSlotUpdate;
			slots[i] = slot;

			slots_dict.Add(slotGO, slot);
		}
	}

	private Vector3 GetPosition(int i)
	{
		return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
	}
}
