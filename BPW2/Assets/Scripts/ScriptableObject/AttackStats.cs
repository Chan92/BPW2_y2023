using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/AttackStats")]
public class AttackStats:ScriptableObject {
	[Header("Attack stats")]
	public int minDmg = 1;
	public int maxDmg = 10;
	public int attackRange = 1;
	public int aoeHitAmount = 1;
	public float effectDuration = 2f;

	[HideInInspector]
	public int dmg;

	[Header("Attack method")]
	public bool straightlineAttack = true;
	public bool diagnoalLineAttack = false;
	public bool oddSpotsAttack = false;

	[Header("Attack Effects")]
	public Transform effectPrefab;

	[HideInInspector]
	public LayerMask target;
	private List<Vector3Int> attackPositions;
	private Vector3Int attackerPosition;

	public void Attack(Vector3 position, LayerMask targetLayer) {
		Debug.Log("Attacked with: " + name);

		attackerPosition = Vector3Int.RoundToInt(position);
		target = targetLayer;

		GetAttackDamage();
		GetAttackPositions();
		SpawnEffect();
	}

	public void GetAttackDamage() {
		dmg = Random.Range(minDmg, maxDmg);
	}

	//get the hit patern positions
	private void GetAttackPositions() {
		attackPositions = new List<Vector3Int>();
		Vector3Int gridPos = Vector3Int.zero;

		for(int row = -attackRange; row <= attackRange; row++) {
			for(int colum = -attackRange; colum <= attackRange; colum++) {
				gridPos.x = row;
				gridPos.z = colum;

				//straightlineAttack
				if(gridPos.x == 0 || gridPos.z == 0) {
					if(gridPos.x == 0 && gridPos.z == 0) {
						continue;					
					} else {
						if(straightlineAttack) {
							attackPositions.Add(attackerPosition + gridPos);
						}
					}

				//diagnoalLineAttack
				} else if(Mathf.Abs(gridPos.x) == Mathf.Abs(gridPos.z)) {
					if(diagnoalLineAttack) {
						attackPositions.Add(attackerPosition + gridPos);
					}

				//oddSpotsAttack
				} else {
					if(oddSpotsAttack) {
						attackPositions.Add(attackerPosition + gridPos);
					}
				}
			}
		}
	}

	private void SpawnEffect() {
		if(attackPositions.Count < 1) {
			return;
		}

		for(int i = 0; i < attackPositions.Count; i++) {
			Transform newEffect = Instantiate(effectPrefab, attackPositions[i], Quaternion.identity);
			newEffect.GetComponent<Attack>().SetAttackInfo(this);
		}
	}
}