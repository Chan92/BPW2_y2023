using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillHover:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public static event System.Action<GameObject, int> onPointerEnter;
	public static event System.Action<GameObject> onPointerExit;

	[SerializeField]
	private int attackId;

	//hover on a skill in the quickbar
	public void OnPointerEnter(PointerEventData eventData) {
		onPointerEnter?.Invoke(gameObject, attackId);
	}

	public void OnPointerExit(PointerEventData eventData) {
		onPointerExit?.Invoke(gameObject);
	}
}
