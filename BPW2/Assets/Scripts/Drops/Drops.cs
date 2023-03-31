using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Drops : MonoBehaviour {
	public enum DropType {
		Useable,
		Equipable
	}

	public DropType dropType;
	public Sprite icon;

	protected virtual void OnPickup() {
		if(Inventory.instance.FreeInvSpace > 0) {
			SetValues();
			Destroy(gameObject);
		}
	}

	protected void OnTriggerEnter(Collider other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
			OnPickup();
		}
	}

	protected abstract void SetValues();
	
}
