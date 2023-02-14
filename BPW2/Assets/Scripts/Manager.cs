using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public static Manager instance;

	public enum GameState {
		Start, 
		Play, 
		Paused, 
		Win, 
		Lose
	}
	public enum GameTurn {
		Player,
		Enemy
	}

	public GameState gameState;
	public GameTurn gameTurn;

	public Vector3 StartRoomCenter {
		get;set;
	}

	private void Awake() {
		instance = this;
	}
}
