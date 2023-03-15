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

	[HideInInspector]
	public int dmg;

	[Header("Attack method")]
	public bool straightlineAttack = true;
	public bool diagnoalLineAttack = false;
	public bool oddSpotsAttack = false;

	[Header("Attack Effects")]
	public Transform effectPrefab;
	public float effectDuration = 2f;

	[HideInInspector]
	public LayerMask target;
	private List<Vector3Int> attackPositions;
	private Vector3Int attackerPosition;
	private Vector3Int previousPosition;

	public void Attack(Vector3Int position, LayerMask targetLayer, Stats stats) {
		attackerPosition = Vector3Int.RoundToInt(position);
		target = targetLayer;

		GetAttackDamage(stats);
		GetAttackPositions(attackerPosition);
		SpawnEffect();
	}

	//calculate the damage based on the strength of the attacker and the skill itself
	public void GetAttackDamage(Stats stats) {
		int min = Mathf.RoundToInt(stats.atk * (minDmg * 0.01f));
		int max = Mathf.RoundToInt(stats.atk * (maxDmg * 0.01f));

		dmg = Random.Range(min, max);
	}

	//get the hit patern positions
	public List<Vector3Int> GetAttackPositions(Vector3Int attackerPos) {
		//if the hit positions have already been calculated, return the previous calculated list
		if(attackerPos == previousPosition && attackPositions.Count > 0) {
			return attackPositions;
		} else {
			previousPosition = attackerPos;
		}

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
							attackPositions.Add(attackerPos + gridPos);
						}
					}

				//diagnoalLineAttack
				} else if(Mathf.Abs(gridPos.x) == Mathf.Abs(gridPos.z)) {
					if(diagnoalLineAttack) {
						attackPositions.Add(attackerPos + gridPos);
					}

				//oddSpotsAttack
				} else {
					if(oddSpotsAttack) {
						attackPositions.Add(attackerPos + gridPos);
					}
				}
			}
		}

		return attackPositions;
	}

	//checks if the attacker is able to hit the target before setting the attack
	public bool CanHitTarget(Vector3 attackerPos, Vector3 targetPos) {
		attackerPosition = Vector3Int.RoundToInt(attackerPos);
		Vector3Int targetPosition = Vector3Int.RoundToInt(targetPos);

		GetAttackPositions(attackerPosition);
		if(attackPositions.Contains(targetPosition)) {
			return true;
		} else {
			return false;
		}
	}
	
	//spawn an effect on the locations of the attack pattern
	public void SpawnEffect() {
		if(attackPositions.Count < 1) {
			return;
		}

		for(int i = 0; i < attackPositions.Count; i++) {
			Transform newEffect = Instantiate(effectPrefab, attackPositions[i], Quaternion.identity);
			newEffect.GetComponent<Attack>().SetAttackInfo(this);
		}
	}
}