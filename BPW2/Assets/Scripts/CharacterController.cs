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

		//kill all (TESTING PURPOSE)
		if(Input.GetKeyDown(KeyCode.K)) {
			int amount = Manager.instance.enemyManager.AliveCounter();
			print("enemies alive: " + amount);

			int test = 0;

			for(int i = 0; i < amount; i++) {
				Transform enemy = Manager.instance.enemyManager.spawnedEnemies[0];
				Manager.instance.enemyManager.DespawnEnemy(enemy);
				test++;
			}

			print("Killed enemies: " + test);
			//DungeonManager.instance.LowerHallWalls();
			Manager.instance.dungeonManager.LowerHallWalls();
			Manager.instance.ChangeTurn();
		}
	}

	private void InBattleMovement() {
		Vector3 direction = Vector3.zero;

		if(Input.GetButtonDown("Vertical")) {
			direction = Vector3.forward * Input.GetAxisRaw("Vertical");
			Manager.instance.ChangeTurn();
		} else if(Input.GetButtonDown("Horizontal")) {
			direction = -Vector3.left * Input.GetAxisRaw("Horizontal");
			Manager.instance.ChangeTurn();
		}
		
		Movement(direction);
	}

	private void OutBattleMovement() {
		Vector3 direction = Vector3.zero;

		if(Input.GetButton("Vertical")) {
			direction = Vector3.forward * Input.GetAxisRaw("Vertical");
			Manager.instance.ChangeTurn();
		} else if(Input.GetButton("Horizontal")) {
			direction = -Vector3.left * Input.GetAxisRaw("Horizontal");
			Manager.instance.ChangeTurn();
		}

		Movement(direction);
	}

	private void Movement(Vector3 direction) {
		if(!Physics.Raycast(transform.position, direction, dungeonStats.TileSize, movementBlockade)) {
			transform.position += direction * dungeonStats.TileSize;
			
			Vector3Int position = Vector3Int.RoundToInt(transform.position);
			DungeonManager.instance.CheckCurrentRoom(position);
		}
	}



	private void Attack() {
		//Manager.instance.ChangeTurn();
	}

	private void Heal() {
		//Manager.instance.ChangeTurn();
	}
}
