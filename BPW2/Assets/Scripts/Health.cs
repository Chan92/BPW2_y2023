using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Health : MonoBehaviour {
    private int currentHealth;
	private Stats stats;

    private void Start() {
		stats = gameObject.GetComponent<Stats>();
        currentHealth = stats.maxHp;
    }

    public void GetDamaged(int damage) {
		damage -= stats.def;

		if(damage < 0) {
			damage = 0;
			Debug.Log(gameObject.name + " got hit but lost no damage.");
		} else {
			currentHealth -= damage;
			Debug.Log(gameObject.name + " lost " + damage + " health");

			if(currentHealth <= 0) {
				Debug.Log(gameObject.name + " has died.");
			}
		}
    }

    public void GetHealed(int amount) {
        if(amount > stats.maxHp) {
            amount = stats.maxHp;
        }

        currentHealth += amount;
    }
}