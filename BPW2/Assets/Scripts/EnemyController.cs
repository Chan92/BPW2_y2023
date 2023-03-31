using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
	private GameObject[] droplist;
	[SerializeField]
	private int dropChance = 30;

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

		Manager.instance.Invoke("ChangeTurn", 0f);
	}

	private void Attack(int attackType) {		
		attacks[attackType].Attack(Vector3Int.RoundToInt(transform.position), attackTargetLayer, stats);
		Manager.instance.Invoke("ChangeTurn", attacks[attackType].effectDuration);
	}

	private void Death(GameObject obj) {
		Drops();
		EnemyManager.instance.DespawnEnemy(transform);
	}

	private void Drops() {
		if(droplist.Length < 1) {
			return;
		}

		int randomChance = Random.Range(0, 100);

		if(randomChance <= dropChance) {
			int randomDrop = Random.Range(0, droplist.Length);

			Instantiate(droplist[randomDrop], transform.position, Quaternion.identity);
		}
	}
}
