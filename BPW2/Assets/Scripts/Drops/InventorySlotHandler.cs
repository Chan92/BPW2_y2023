using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotHandler:MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
	/* Made with tutorial from
	 * youtube ch: One Wheel Studio
	 */

	public Transform StartingSlot { get; private set; }
	private Vector2 offset;

	public void OnBeginDrag(PointerEventData eventData) {
		StartingSlot = transform.parent;

		transform.SetParent(transform.root);
		transform.SetAsLastSibling();

		offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
		transform.GetComponent<Image>().raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = eventData.position - offset;
	}

	public void OnEndDrag(PointerEventData eventData) {
		transform.GetComponent<Image>().raycastTarget = true;
		
		//return to original pos if the new pos is neither a bag or equipment slot
		if(transform.parent.GetComponent<InventorySlot>() == null 
			&& transform.parent.GetComponent<EquipmentSlots>() == null) {
			transform.SetParent(StartingSlot);
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		if(eventData.button == PointerEventData.InputButton.Right) {
			InventoryItem item = transform.GetComponent<InventoryItem>();

			if(!item.IsEmpty) {
				EquipmentSlots.instance.OnEquipping(null, item);

				item.RemoveInfo();
			}
		}
	}
}
