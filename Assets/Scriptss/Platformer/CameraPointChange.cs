using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointChange : MonoBehaviour {

	public bool entrou = false;
	private GameObject cam;
	public string animName;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player") && !entrou) {
			entrou = true;
			transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
			GetComponent<BoxCollider2D>().isTrigger = false;
			cam.GetComponent<Animator>().Play(animName);
		}
	}
}
