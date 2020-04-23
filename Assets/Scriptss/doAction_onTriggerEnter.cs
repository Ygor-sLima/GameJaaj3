using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class doAction_onTriggerEnter : MonoBehaviour {

	public UnityEvent acao;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			acao.Invoke();
		}
	}

}
