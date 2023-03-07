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

	[SerializeField]
	private int enemyMaxTurns = 2;
	[HideInInspector]
	public int enemyTurnCounter = 0;

	private void Awake() {
		instance = this;
	}

	public void ChangeTurn() {
		switch(gameTurn) {
			case GameTurn.OutOfCombat:
				if(enemyManager.AliveCounter() > 0) {
					gameTurn = GameTurn.Player;
				}
				break;
			case GameTurn.Player:
				if(enemyManager.AliveCounter() > 0) {
					gameTurn = GameTurn.Enemy;
					enemyManager.EnemyAction();
				} else {
					gameTurn = GameTurn.OutOfCombat;
				}
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
				break;
			default:
				Debug.Log("Something went wrong?");
				break;
		}
	}
}
