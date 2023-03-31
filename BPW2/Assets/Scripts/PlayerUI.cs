using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour {
	[SerializeField]
	private Transform player;
	private Stats playerStats;
	private CharacterController charController;

	[SerializeField]
	private CanvasGroup quickbarCG;
	[SerializeField]
	private CanvasGroup skillInfoCG;
	[SerializeField]
	private CanvasGroup inventoryCG;

	[SerializeField]
	private TMP_Text skillTitle;
	[SerializeField]
	private TMP_Text skillInfo;
	[SerializeField]
	private TMP_Text statsValue;

	[SerializeField]
	private Image healthFill;

	

	private void Awake() {
		playerStats = player.GetComponent<Stats>();
		charController = player.GetComponent<CharacterController>();
		DisableSkillInfo(0);
		inventoryCG.alpha = 0;
	}

	private void OnEnable() {
		if(player) player.GetComponent<Health>().onHealthChanged += HealthChanged;
		Manager.isPlayerTurn += EnableQuickbar;
		SkillHover.onPointerEnter += EnableSkillInfo;
		SkillHover.onPointerExit += DisableSkillInfo;
		EquipmentSlots.onEquipChange += UpdateStats;
	}

	private void OnDisable() {
		if(player) player.GetComponent<Health>().onHealthChanged -= HealthChanged;
		Manager.isPlayerTurn -= EnableQuickbar;
		SkillHover.onPointerEnter -= EnableSkillInfo;
		SkillHover.onPointerExit -= DisableSkillInfo;
		EquipmentSlots.onEquipChange -= UpdateStats;
	}

	//update hp bar when the player takes damage or gets healed
	private void HealthChanged(float newHealth) {
		float healthPrecent = newHealth / playerStats.maxHp;
		healthFill.fillAmount = healthPrecent;
	}

	private void EnableQuickbar(bool enable) {
		if(enable) {
			quickbarCG.alpha = 1;
			quickbarCG.blocksRaycasts = true;
		} else {
			quickbarCG.alpha = 0.2f;
			quickbarCG.blocksRaycasts = false;
		}
	}

	private void EnableSkillInfo(GameObject obj, int skillId) {
		AttackStats atkStats = charController.attacks[skillId];

		skillTitle.text = $"{atkStats.name}";

		skillInfo.text	= $"Damage : {atkStats.minDmg}% - {atkStats.maxDmg}%";
		skillInfo.text += $"\nRange   : {atkStats.attackRange}";

		skillInfoCG.alpha = 1;
	}

	private void DisableSkillInfo(int skillId) {
		skillInfoCG.alpha = 0;
	}

	private void UpdateStats(int value) {
		statsValue.text  = $"{playerStats.maxHp}";
		statsValue.text += $"\n{playerStats.atk}";
		statsValue.text += $"\n{playerStats.def}";
	}

	public void ToggleInventory() {
		if(inventoryCG.alpha < 1) {
			UpdateStats(0);
			inventoryCG.alpha = 1;
		} else {
			inventoryCG.alpha = 0;
		}
	}
}
