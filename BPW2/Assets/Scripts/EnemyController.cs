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

	private Stats stats;

	[HideInInspector]
	public EnemyInfo enemyInfo;

	private void Start() {
		player = Transform.FindObjectOfType<CharacterController>().transform;
		stats = gameObject.GetComponent<Stats>();
	}

	private void OnEnable() {
		if(enemyInfo) enemyInfo.EnableInfo(transform.position);
		gameObject.GetComponent<Health>().onDeath += Death;
	}

	private void OnDisable() {
		if(enemyInfo) enemyInfo.DisableInfo();
		gameObject.GetComponent<Health>().onDeath -= Death;
	}

	public void GetAction() {
		//choose a random attack from the avaible attacks
		int randomAttackType = Random.Range(0, attacks.Length);

		if(attacks[randomAttackType].CanHitTarget(transform.position, player.position)) {
			Attack(randomAttackType);
		} else {
			Movement();
		}

		Manager.instance.ChangeTurn();
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
			enemyInfo.SetInfo(transform.position);
		}
	}

	private void Attack(int attackType) {		
		attacks[attackType].Attack(Vector3Int.RoundToInt(transform.position), attackTargetLayer, stats);
	}

	private void Death(GameObject obj) {
		print("~~~~~~ENEMY died~~~**");
		//DROP ITEMS
		EnemyManager.instance.DespawnEnemy(transform);
	}
}
