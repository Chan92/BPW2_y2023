using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {
	public static Inventory instance;
	public InventoryItem[] bagSlotList;

	public int FreeInvSpace {
		get {
			int counter = 0;
			for(int i = 0; i < bagSlotList.Length; i++) {
				if(bagSlotList[i].IsEmpty) {
					counter++;
				}
			}

			return counter;
		}
	}

	private void Awake() {
		instance = this;
	}

	public int GetEmptySlotId() {
		for(int i = 0; i < bagSlotList.Length; i++) {
			if(bagSlotList[i].IsEmpty) {
				return i;
			}
		}

		Debug.Log("No free slots.");
		return 0;
	}

}
