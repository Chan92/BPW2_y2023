using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Drops {
	public enum EquipType {
		Unequipable,
		Weapon,
		Body,
		Shoes
	}

	[Space(10)]
	public EquipType equipType;
	public List<ItemStats> stats;

	protected override void OnPickup() {
		for(int i = 0; i < stats.Count; i++) {
			stats[i].SetRandomValue();
		}		

		base.OnPickup();
	}

	protected override void SetValues() {
		int freeId = Inventory.instance.GetEmptySlotId();

		Inventory.instance.bagSlotList[freeId].SetInfo(icon, dropType, equipType, stats);
	}
}
