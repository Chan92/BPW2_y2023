using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
	//stats the character/enemy starts with
	[Header("Base Stats")]
	[SerializeField]
	private int baseMaxHp = 100;
	[SerializeField]
	private int baseAtk = 50;
	[SerializeField]
	private int baseDef = 0;

	//stats (possibly) increased by tempenary items/buffs
	[HideInInspector]
	public int maxHp;
	[HideInInspector]
	public int atk;
	[HideInInspector]
	public int def;

	private void Awake() {
		maxHp = baseMaxHp;
		atk = baseAtk;
		def = baseDef;
	}
}
