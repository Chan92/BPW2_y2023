using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Drops {
	public enum PotionType {
		Unuseable,
		Replenish		
	}

	[Space(10)]
	public PotionType potionType;
	public List<ItemStats> stats;

	protected override void SetValues() {
		int freeId = Inventory.instance.GetEmptySlotId();

		Inventory.instance.bagSlotList[freeId].SetInfo(icon, dropType, potionType, stats);
		
		PotionsQuickslot.instance.PotionCount();
	}
}
