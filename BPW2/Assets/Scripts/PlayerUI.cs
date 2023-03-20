using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	[SerializeField]
	private Transform player;
	private Stats playerStats;

	[SerializeField]
	private CanvasGroup quickbarCG;
	[SerializeField]
	private Image healthFill;

	private void Awake() {
		playerStats = player.GetComponent<Stats>();
	}

	private void OnEnable() {
		if(player) player.GetComponent<Health>().onHealthChanged += HealthChanged;
		Manager.isPlayerTurn += EnableQuickbar;
	}

	private void OnDisable() {
		if(player) player.GetComponent<Health>().onHealthChanged -= HealthChanged;
		Manager.isPlayerTurn -= EnableQuickbar;
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
}
