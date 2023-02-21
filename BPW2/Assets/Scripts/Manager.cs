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

	private int enemiesAlive = 1;
	private int enemyTurnCounter = 0;
	private int enemyMaxTurns = 2;

	private void Awake() {
		instance = this;
	}

	public void ChangeTurn() {
		switch(gameTurn) {
			case GameTurn.OutOfCombat:
				if(enemiesAlive > 0) {
					gameTurn = GameTurn.Player;
				}
				break;
			case GameTurn.Player:
				if(enemiesAlive > 0) {
					gameTurn = GameTurn.Enemy;
				} else {
					gameTurn = GameTurn.OutOfCombat;
				}
				break;
			case GameTurn.Enemy:
				if(enemiesAlive > 0) {
					if(enemyTurnCounter < enemyMaxTurns) {
						enemyTurnCounter++;
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
