using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public static Manager instance;

	public enum GameState {
		//turns, win, lose, pause
	}

	private void Awake() {
		instance = this;
	}
}
