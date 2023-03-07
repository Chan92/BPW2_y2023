using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void GetDamaged(int damage) {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " lost " + damage + " health");

        if(currentHealth <= 0) {
            Debug.Log(gameObject.name + " has died.");
        }
    }

    public void GetHealed(int amount) {
        if(amount > maxHealth) {
            amount = maxHealth;
        }

        currentHealth += amount;
    }
}