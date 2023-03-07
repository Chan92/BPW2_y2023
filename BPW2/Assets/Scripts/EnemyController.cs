using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	[SerializeField]
	private DungeonStats dungeonStats;
	[SerializeField]
	private AttackStats[] attacks;
	
	private Transform player;

	[SerializeField]
	private LayerMask movementBlockade;
	[SerializeField]
	private LayerMask attackTargetLayer;

	[SerializeField]
	private float distanceOffset = 0.5f;


	private void Start() {
		player = Transform.FindObjectOfType<CharacterController>().transform;
	}	

	public void GetAction() {
		//choose a random attack from the avaible attacks
		int randomAttackType = Random.Range(0, attacks.Length);

		if(InAttackRange(randomAttackType)) {
			Attack(randomAttackType);
		} else {
			Movement();
		}

		Manager.instance.ChangeTurn();
	}

	//checks if the attack can hit the target
	//TODO: fix attack patern check instead of only distance check
	private bool InAttackRange(int attackType) {
		float distance = Vector3.Distance(transform.position, player.position);
		if(distance - distanceOffset <= attacks[attackType].attackRange) {
			return true;
		} else {
			return false;
		}
	}

	private void Movement() {
		Vector3 direction = player.position - transform.position;
		direction.y = 0;
		direction = direction.normalized;

		Vector3Int moveTo = Vector3Int.RoundToInt(direction);

		//avoids the chance of moving diagonally
		if(Mathf.Abs(moveTo.x) == Mathf.Abs(moveTo.z) && moveTo.z != 0) {
			moveTo.z = 0;
		}

		if(!Physics.Raycast(transform.position, moveTo, dungeonStats.TileSize, movementBlockade)) {
			transform.position += moveTo * dungeonStats.TileSize;
		}
	}

	private void Attack(int attackType) {
		attacks[attackType].Attack(transform.position, attackTargetLayer);
	}	
}
