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
			//InBattleMovement();
			OutBattleMovement();
			Attack();
			Heal();
		}
	}

	private void InBattleMovement() {
		Vector3 direction = Vector3.zero;

		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
			direction = Vector3.forward * Input.GetAxisRaw("Vertical");
		} else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
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
		if(!Physics.Raycast(transform.position, direction, dungeonStats.TileSize, movementBlockade)) {
			transform.position += direction * dungeonStats.TileSize;
		}
	}


	private void Attack() {
	}

	private void Heal() {
	}
}
