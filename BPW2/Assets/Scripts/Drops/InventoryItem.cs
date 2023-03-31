using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
	public bool IsEmpty { get; private set;	}
	public Drops.DropType DropType { get; private set;}
	public Equipment.EquipType EquipType { get; private set;}
	public Potion.PotionType PotionType { get; private set;}
	public List<ItemStats> Stats { get; private set;}

	private Sprite icon;
	private Image iconImg;

	private void Start() {
		iconImg = transform.GetComponent<Image>();
		RemoveInfo();
	}

	public void SetInfo(Sprite _icon, Drops.DropType _droptype, Equipment.EquipType _equipType, List<ItemStats> _stats) {
		icon = _icon;
		DropType = _droptype;
		EquipType = _equipType;
		Stats = _stats;

		PotionType = Potion.PotionType.Unuseable;
		IsEmpty = false;
		UpdateUI();
	}

	public void SetInfo(Sprite _icon, Drops.DropType _droptype, Potion.PotionType _potionType, List<ItemStats> _stats) {
		icon = _icon;
		DropType = _droptype;
		PotionType = _potionType;
		Stats = _stats;

		EquipType = Equipment.EquipType.Unequipable;
		IsEmpty = false;
		UpdateUI();
	}

	public void RemoveInfo() {
		icon = null;
		EquipType = Equipment.EquipType.Unequipable;
		PotionType = Potion.PotionType.Unuseable;
		Stats = new List<ItemStats>();
		IsEmpty = true;
		UpdateUI();
	}

	private void UpdateUI() {
		iconImg.sprite = icon;

		Color newColor = iconImg.color;

		if(iconImg.sprite != null) {
			newColor.a = 1f;
		} else {
			newColor.a = 0f;
		}
		
		iconImg.color = newColor;
	}
}
