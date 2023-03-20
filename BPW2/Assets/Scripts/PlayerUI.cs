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
	private Image healthFill;

	private void Awake() {
		playerStats = player.GetComponent<Stats>();
	}

	private void OnEnable() {
		if(player) player.GetComponent<Health>().onHealthChanged += HealthChanged;
	}

	private void OnDisable() {
		if(player) player.GetComponent<Health>().onHealthChanged -= HealthChanged;
	}

	//update hp bar when the player takes damage or gets healed
	private void HealthChanged(float newHealth) {
		float healthPrecent = newHealth / playerStats.maxHp;
		healthFill.fillAmount = healthPrecent;
	}	
}
