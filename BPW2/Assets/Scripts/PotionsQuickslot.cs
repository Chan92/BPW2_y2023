using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class PotionsQuickslot : MonoBehaviour, IPointerClickHandler {
	public static event System.Action<int> onPointerClick;

	public static PotionsQuickslot instance;

	[SerializeField]
	private Health playerHealth;
	[SerializeField]
	private TMP_Text potionAmount;

	private void Awake() {
		instance = this;
	}

	public void OnPointerClick(PointerEventData eventData) {
		UsePotion();
		onPointerClick?.Invoke(0);
	}

	public int PotionCount() {
		int counter = 0;
		
		for(int i = 0; i < Inventory.instance.bagSlotList.Length; i++) {
			if(Inventory.instance.bagSlotList[i].PotionType == Potion.PotionType.Replenish) {
				counter++;
			}
		}

		potionAmount.text = $"{counter}";
		return counter;
	}

	private void UsePotion() {
		if(PotionCount() > 0) {
			for(int i = 0; i < Inventory.instance.bagSlotList.Length; i++) {
				if(Inventory.instance.bagSlotList[i].PotionType == Potion.PotionType.Replenish) {
					int value = 0;

					for(int j = 0; j < Inventory.instance.bagSlotList[i].Stats.Count; j++) {
						value += Inventory.instance.bagSlotList[i].Stats[j].Value;
					}

					Inventory.instance.bagSlotList[i].RemoveInfo();
					playerHealth.GetHealed(value);
					PotionCount();
					break;
				}
			}
		}
	}

}
