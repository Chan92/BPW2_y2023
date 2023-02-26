using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	[SerializeField]
	private DungeonStats dungeonStats;
	private Transform player;
	private LayerMask movementBlockade;

	private void Start() {
		movementBlockade = LayerMask.GetMask("Wall") << LayerMask.GetMask("Obstacle") << LayerMask.GetMask("Enemy") << LayerMask.GetMask("Player");
		player = Transform.FindObjectOfType<CharacterController>().transform;
	}	

	public void GetAction() {
		if(Manager.instance.gameTurn == Manager.GameTurn.Enemy) {
			//if in attack range
			//Attack();
			//else
			Movement();

			Manager.instance.enemyTurnCounter++;
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

	private void Attack() {
	}
}
