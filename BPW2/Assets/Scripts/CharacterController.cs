using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController:MonoBehaviour {
	[SerializeField]
	private DungeonStats dungeonStats;

	private LayerMask movementBlockade;
	private int prevRoomId = -1;

	private void Start() {
		movementBlockade = LayerMask.GetMask("Wall") << LayerMask.GetMask("Obstacle") << LayerMask.GetMask("Enemy");
	}

	private void Update() {
		if(Manager.instance.gameTurn == Manager.GameTurn.Player) {
			InBattleMovement();
			Attack();
			Heal();
		} else if(Manager.instance.gameTurn == Manager.GameTurn.OutOfCombat) {
			InBattleMovement();
			//OutBattleMovement();
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
			direction = Vector3.forward * Input.GetAxisRaw("Vertical");
		} else if(Input.GetButton("Horizontal")) {
			direction = -Vector3.left * Input.GetAxisRaw("Horizontal");			
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
		}

		Manager.instance.ChangeTurn();
	}



	private void Attack() {
		//Manager.instance.ChangeTurn();
	}

	private void Heal() {
		//Manager.instance.ChangeTurn();
	}
}
