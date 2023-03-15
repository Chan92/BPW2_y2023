using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTile : MonoBehaviour {
	[SerializeField]
	private Material normalMat;
	[SerializeField]
	private Material markedMat;

	private MeshRenderer mr;

	private void Start() {
		mr = gameObject.GetComponent<MeshRenderer>();
	}

	public void ChangeMaterial(bool marked) {
		if(marked) {
			mr.material = markedMat;
		} else {
			mr.material = normalMat;
		}
	}
}
