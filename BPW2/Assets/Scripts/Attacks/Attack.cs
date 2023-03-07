using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	private AttackStats attackStats;

	public void SetAttackInfo(AttackStats info) {
		attackStats = info;
		StartCoroutine(Timer());
	}
	
	private void OnTriggerEnter(Collider other) {
		//checks if the collided layer is the same as the target layer
		bool checkLayer = (attackStats.target & 1 << other.gameObject.layer) == 1 << other.gameObject.layer;

		if(checkLayer) {
			Health target = other.gameObject.GetComponent<Health>();

			if(target) {
				target.GetDamaged(attackStats.dmg);
			}
		}
	}

	private IEnumerator Timer() {
		yield return new WaitForSeconds(attackStats.effectDuration);
		Transform.Destroy(transform.gameObject);
	}
}
