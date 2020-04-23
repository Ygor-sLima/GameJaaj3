using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour {
//	 J – K – L - j - k - l

	//Frutas
	public static int frutas;
	public Text frutaTxt;
	//Andar
	private float MoveInput;
	public float speed;

	//Game
	private Rigidbody2D rb;
	private Animator anim;
	public GameObject mainCam;
	public GameObject followingCam;	
	
	//pulo
	private float timeJumpingNow;
	private bool isJumping = false;
	public float checkRadius;
	private bool isGrounded;
	public Transform feetPos;
	public float jumpSpeed;
	public float timeJumping;
	public LayerMask whatIsGround;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		frutas = 0;
	}
	
	// Update is called once per frame
	void Update () {
		frutaTxt.text = ""+frutas;
		
		MoveInput = Input.GetAxisRaw("Horizontal");
		if(MoveInput != 0) {
			anim.SetBool("Running", true);
		} else { anim.SetBool("Running", false); }

		isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
		anim.SetBool("pu", !isGrounded);

		if(Input.GetKeyDown(KeyCode.W) && isGrounded) {
			rb.velocity = Vector2.up * jumpSpeed;
			isJumping = true;
			timeJumpingNow = timeJumping;
		}
		if(Input.GetKey(KeyCode.W) && isJumping) {
			if(timeJumpingNow < 0) {
				isJumping = false;
			}else
			{	
				rb.velocity = Vector2.up * jumpSpeed;
				timeJumpingNow -= Time.deltaTime;	
			}
		}
		if(Input.GetKeyUp(KeyCode.W)) {
			isJumping = false;
		}

		if(MoveInput == 1) {
			transform.eulerAngles = new Vector3(0,0,0);
		} else if ( MoveInput == -1) {
			transform.eulerAngles = new Vector3(0,180,0);
		}

	}

	void LateUpdate() {
		followingCam.transform.position = transform.position + new Vector3(0,0,-10);
	}

	void FixedUpdate() {
		rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
	}

	public void getFruta(GameObject fruta) {
		frutas++;
		Destroy(fruta);
	}

	public void cameraFollow(bool check) {
		if(check) {
			mainCam.SetActive(false);
			followingCam.SetActive(true);
			rb.gravityScale = 1f;
		}
	}

	public void changeCamera(string s) {
		SceneManager.LoadScene(s);
	}
}
