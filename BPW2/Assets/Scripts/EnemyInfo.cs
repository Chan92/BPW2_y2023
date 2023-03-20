using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour {
	[SerializeField]
	private Vector3 infoOffset;
	
	[Header("Canvas Groups")]
	[SerializeField]
	private CanvasGroup infoWindowCG;
	[SerializeField]
	private CanvasGroup turnMarkCG;

	[Space(15)]
	[SerializeField]
	private Image healthFill;

	private Health enemyHealth;
	private Stats enemyStats;

	public void AssignInfo(Transform enemy) {
		enemyHealth = enemy.GetComponent<Health>();
		enemyStats = enemy.GetComponent<Stats>();
	}

	public void SetInfo(Vector3 position) {
		transform.position = position + infoOffset;
		infoWindowCG.alpha = 1;
	}

	public void EnableInfo(Vector3 position) {
		SetInfo(position);
		enemyHealth.onHealthChanged += HealthChanged;
		enemyHealth.GetHealed(enemyStats.maxHp);
	}

	public void DisableInfo() {
		infoWindowCG.alpha = 0;
		enemyHealth.onHealthChanged -= HealthChanged;
	}

	private void HealthChanged(float newHealth) {
		float healthPrecent = newHealth / enemyStats.maxHp;
		healthFill.fillAmount = healthPrecent;
	}

	public void SetActiveTurn(bool active) {
		if(active) {
			turnMarkCG.alpha = 1;
		} else {
			turnMarkCG.alpha = 0;
		}
	}
}
