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
		Enemy,
		OutOfCombat
	}

	public GameState gameState;
	public GameTurn gameTurn;

	public DungeonManager dungeonManager;
	public EnemyManager enemyManager;

	public Transform playerObj;

	public static event System.Action<bool> isPlayerTurn;

	public GameObject winScreen;
	public GameObject loseScreen;

	[SerializeField]
	private int enemyMaxTurns = 2;
	[HideInInspector]
	public int enemyTurnCounter = 0;

	private void Awake() {
		instance = this;
		winScreen.SetActive(false);
		loseScreen.SetActive(false);
	}

	public void ChangeTurn() {
		switch(gameTurn) {
			case GameTurn.OutOfCombat:
				if(enemyManager.AliveCounter() > 0) {
					gameTurn = GameTurn.Player;
				}

				CheckNewTurns();
				break;
			case GameTurn.Player:
				if(enemyManager.AliveCounter() > 0) {
					gameTurn = GameTurn.Enemy;
					enemyManager.EnemyAction();
				} else {
					gameTurn = GameTurn.OutOfCombat;
				}

				CheckNewTurns();
				break;
			case GameTurn.Enemy:
				if(enemyManager.AliveCounter() > 0) {
					if(enemyTurnCounter < enemyMaxTurns-1) {
						enemyTurnCounter++;
						enemyManager.EnemyAction();
					} else {
						enemyTurnCounter = 0;
						gameTurn = GameTurn.Player;
					}
				} else {
					gameTurn = GameTurn.OutOfCombat;
				}

				CheckNewTurns();
				break;
			default:
				Debug.Log("Something went wrong?");
				break;
		}
	}

	private void CheckNewTurns() {
		switch(gameTurn) {
			case GameTurn.Player:
				isPlayerTurn?.Invoke(true);
				StartCoroutine(enemyManager.SetActiveMark(-1));
				break;
			case GameTurn.Enemy:
				isPlayerTurn?.Invoke(false);
				break;
			case GameTurn.OutOfCombat:
				isPlayerTurn?.Invoke(true);

				if(dungeonManager.unvisitedRooms <= 0) {
					winScreen.SetActive(true);
				}

				break;
		}
	}
}
