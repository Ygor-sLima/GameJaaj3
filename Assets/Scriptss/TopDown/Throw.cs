using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Throw : MonoBehaviour {

	[HideInInspector] public Vector3 go;
	[HideInInspector] public bool moving = false;
	

	public GameObject esseSemGo;
	
	// Update is called once per frame
	void Update () {
		if(moving) {
			go.z = 0;
			transform.position = Vector2.MoveTowards(transform.position, go, 10f * Time.deltaTime);

			if(transform.position == go) {
				Destroy(gameObject);
				stopMov(transform.rotation);				
			}
		}
	}
	private void stopMov(Quaternion a) {
		GameObject instantied = Instantiate(esseSemGo, transform.position, a);
		instantied.GetComponent<Throw>().DestroyAnim();
		instantied.GetComponent<BoxCollider2D>().isTrigger = true;
		instantied.GetComponent<BoxCollider2D>().enabled = true;
		instantied.GetComponent<Throw>().moving = false;
	}
	public void DestroyAnim() {
		Destroy(GetComponent<Animator>());
	}

	void OnCollisionEnter2D(Collision2D o) {
		
		if(o.gameObject.layer == 8) {
			Destroy(gameObject);
			stopMov(Quaternion.Euler(0,0,90));
			
		}
	}

	void OnTriggerEnter2D(Collider2D o) {
		if(o.gameObject.CompareTag("Player")) {
			o.gameObject.GetComponent<PlayerTD>().jogado = false;
			o.gameObject.GetComponent<PlayerTD>().arma.SetActive(true);
			Destroy(gameObject);
		} else if(o.gameObject.CompareTag("enemy") && moving) {
			Destroy(gameObject);
			stopMov(Quaternion.Euler(0,0,transform.rotation.z));
			Destroy(o.gameObject);
		}
	}
	
}
