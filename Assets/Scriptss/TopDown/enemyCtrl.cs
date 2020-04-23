using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCtrl : MonoBehaviour {
	
	private GameObject p;
	private Animator anim;
	// Use this for initialization
	void Start () {
		p = GameObject.Find("Player");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, p.transform.position, 4*Time.deltaTime);
		
		if(p.transform.position.x >= transform.position.x) {
			transform.eulerAngles = new Vector3(0,0,0);
		} else
		{
			transform.eulerAngles = new Vector3(0,180,0);
		}
	}
	
	void OnDestroy() {
		PlayerTD.inimigosMortos++;
	}
}
