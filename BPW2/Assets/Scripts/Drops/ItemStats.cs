using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStats  {
	public enum StatsType {
		Hp,
		Atk,
		Def
	}

	public StatsType type;
	public int Value {
		get {
			return fixedValue;
		}
	}

	[SerializeField]
	private int fixedValue;
	[SerializeField]
	private int minValue;
	[SerializeField]
	private int maxValue;

	public void SetRandomValue() {
		fixedValue = Random.Range(minValue, maxValue + 1);
	}
}