using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Stats))]
public class Health : MonoBehaviour {
	public event System.Action<GameObject> onDeath;
	public event System.Action<float> onHealthChanged;
	private int currentHealth;
	private Stats stats;

	private void Awake() {
		stats = gameObject.GetComponent<Stats>();
	}

	private void Start() {
		GetHealed(stats.maxHp);
    }

    public void GetDamaged(int damage) {
		damage -= stats.def;

		if(damage < 0) {
			damage = 0;
		} else {
			currentHealth -= damage;

			if(currentHealth <= 0) {
				currentHealth = 0;
				onDeath?.Invoke(gameObject);
			}

			onHealthChanged?.Invoke(currentHealth);
		}
    }

    public void GetHealed(int amount) {
		if(currentHealth + amount >= stats.maxHp) {
			currentHealth = stats.maxHp;
		} else {
			currentHealth += amount;
		}

		onHealthChanged?.Invoke(currentHealth);
	}
}