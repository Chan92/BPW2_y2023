using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot:MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData) {
		InventoryItem newItem = eventData.pointerDrag.transform.GetComponent<InventoryItem>();
		InventoryItem oldItem = null;

		if(transform.childCount > 0) {
			oldItem = transform.GetChild(0).GetComponent<InventoryItem>();
			transform.GetChild(0).SetParent(eventData.pointerDrag.transform.GetComponent<InventorySlotHandler>().StartingSlot);
		}

		eventData.pointerDrag.transform.SetParent(transform);	
		EquipmentSlots.instance.OnEquipping(newItem, oldItem);
	}
}
