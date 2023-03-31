using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlots:MonoBehaviour, IDropHandler {
	public static event System.Action<int> onEquipChange;
	public static EquipmentSlots instance;

	private void Awake() {
		instance = this;
	}

	[SerializeField]
	private Equipment.EquipType slotType;

	private int tempHpValue = 0;
	private int tempAtkValue = 0;
	private int tempDefValue = 0;

	public void OnDrop(PointerEventData eventData) {
		InventoryItem newItem = eventData.pointerDrag.transform.GetComponent<InventoryItem>();
		InventoryItem oldItem = null;

		if(newItem.EquipType == slotType) {

			if(transform.childCount > 0) {
				oldItem = transform.GetChild(0).GetComponent<InventoryItem>();
				transform.GetChild(0).SetParent(eventData.pointerDrag.transform.GetComponent<InventorySlotHandler>().StartingSlot);
			}
		
			eventData.pointerDrag.transform.SetParent(transform);
		}
		
		OnEquipping(newItem, oldItem);
	}

	public void OnEquipping(InventoryItem newItem, InventoryItem oldItem) {
		Stats playerStats = Manager.instance.playerObj.GetComponent<Stats>();

		if(oldItem != null) {
			GetItemStats(oldItem);
			playerStats.maxHp -= tempHpValue;
			playerStats.atk -= tempAtkValue;
			playerStats.def -= tempDefValue;
			onEquipChange?.Invoke(0);
		}

		if(newItem != null) {
			GetItemStats(newItem);
			playerStats.maxHp += tempHpValue;
			playerStats.atk += tempAtkValue;
			playerStats.def += tempDefValue;
			onEquipChange?.Invoke(0);
		}

		Manager.instance.playerObj.GetComponent<Health>().CheckStatChanges();
	}

	private void GetItemStats(InventoryItem item) {
		tempHpValue = 0;
		tempAtkValue = 0;
		tempDefValue = 0;

		for(int i = 0; i < item.Stats.Count; i++) {
			ItemStats.StatsType type = item.Stats[i].type;

			switch(type) {
				case ItemStats.StatsType.Hp:
					tempHpValue += item.Stats[i].Value;
					break;
				case ItemStats.StatsType.Atk:
					tempAtkValue += item.Stats[i].Value;
					break;
				case ItemStats.StatsType.Def:
					tempDefValue += item.Stats[i].Value;
					break;
			}
		}
	}
}
