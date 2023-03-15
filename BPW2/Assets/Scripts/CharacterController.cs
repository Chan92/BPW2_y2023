using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController:MonoBehaviour {
	[SerializeField]
	private DungeonStats dungeonStats;

	[SerializeField]
	private LayerMask movementBlockade;
	
	[SerializeField]
	private LayerMask attackTargetLayer;

	[SerializeField]
	float movementCooldown = 0.2f;
	float movementTimer;

	public AttackStats[] attacks;
	private Stats stats;

	private void Start() {
		stats = gameObject.GetComponent<Stats>();
	}

	private void Update() {
		if(Manager.instance.gameTurn == Manager.GameTurn.Player) {
			InBattleMovement();
			Attack();
			Heal();
		} else if(Manager.instance.gameTurn == Manager.GameTurn.OutOfCombat) {
			OutBattleMovement();
			Heal();
		}

		DebuggingKillAll();
	}

	//kill all enemies (TESTING PURPOSE)
	private void DebuggingKillAll() {
		if(Input.GetKeyDown(KeyCode.K)) {
			int amount = Manager.instance.enemyManager.AliveCounter();
			int killCounter = 0;

			for(int i = 0; i < amount; i++) {
				Transform enemy = Manager.instance.enemyManager.spawnedEnemies[0];
				Manager.instance.enemyManager.DespawnEnemy(enemy);
				killCounter++;
			}

			print("::DEBUG KILLER::   Killed enemies: " + killCounter);
			Manager.instance.dungeonManager.ToggleHallWalls(true);
			Manager.instance.ChangeTurn();
		}
	}

	private void InBattleMovement() {
		Vector3 direction = Vector3.zero;

		if(Input.GetButtonDown("Vertical")) {
			direction = Vector3.forward * Input.GetAxisRaw("Vertical");
		} else if(Input.GetButtonDown("Horizontal")) {
			direction = -Vector3.left * Input.GetAxisRaw("Horizontal");
		}
		
		Movement(direction);
	}

	private void OutBattleMovement() {
		Vector3 direction = Vector3.zero;

		if(Input.GetButton("Vertical")) {
			if(movementTimer > 0) {
				movementTimer -= Time.deltaTime;
			} else {
				movementTimer = movementCooldown;
				direction = Vector3.forward * Input.GetAxisRaw("Vertical");			
			}
		} else if(Input.GetButton("Horizontal")) {
			if(movementTimer > 0) {
				movementTimer -= Time.deltaTime;
			} else {
				movementTimer = movementCooldown;
				direction = -Vector3.left * Input.GetAxisRaw("Horizontal");
			}
		}

		Movement(direction);
	}

	private void Movement(Vector3 direction) {
		if(direction == Vector3.zero) {
			return;
		}

		if(!Physics.Raycast(transform.position, direction, dungeonStats.TileSize, movementBlockade)) {
			transform.position += direction * dungeonStats.TileSize;
			
			Vector3Int position = Vector3Int.RoundToInt(transform.position);
			DungeonManager.instance.CheckCurrentRoom(position);
			Manager.instance.ChangeTurn();
		}
	}

	private void Attack() {
		if(Input.GetKeyDown(KeyCode.X)) {
			attacks[0].Attack(Vector3Int.RoundToInt(transform.position), attackTargetLayer, stats);
			Manager.instance.ChangeTurn();
		}

		if(Input.GetKeyDown(KeyCode.Z)) {
			attacks[1].Attack(Vector3Int.RoundToInt(transform.position), attackTargetLayer, stats);
			Manager.instance.ChangeTurn();
		}

		if(Input.GetKeyDown(KeyCode.C)) {
			attacks[2].Attack(Vector3Int.RoundToInt(transform.position), attackTargetLayer, stats);
			Manager.instance.ChangeTurn();
		}
	}

	private void Heal() {
		//Manager.instance.ChangeTurn();
	}
}
