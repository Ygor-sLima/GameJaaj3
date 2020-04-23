using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerTD : MonoBehaviour {

	//UI
	public Image vidaUI;
	public Image staminaUI;

	public Image inimigoUI;
	public Text FrutaUI;

	//Stats
	[HideInInspector] public float vida;
	private float stamina;
	public float speedRecharge;
	//Itens
	private int frutas;

	private Vector2 moveInput;
	private Rigidbody2D rb;
	public float speed;
	private float limitador = 1f;
	private Animator anim;
	public GameObject arma;

	public static int inimigosMortos;
	private int maxInim = 10;
	//jogar vara
	public Transform posGo;
	public GameObject objThrow;

	[HideInInspector] public bool jogado = false;

	private GameObject cam;
	// Use this for initialization
	void Start () {
		frutas = PlayerCtrl.frutas;
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		cam = GameObject.Find("Main Camera");
		vida = 5;
		stamina = 10;
	}
	
	private void jogarVara(Vector3 go, GameObject vara) {
		GameObject v = Instantiate(vara, posGo.position, Quaternion.identity);
		v.GetComponent<Throw>().go = go;
		v.GetComponent<Throw>().moving = true;
		jogado = true;
	}

	// Update is called once per frame
	void Update () {
		FrutaUI.text = frutas+"x";
		if(Input.GetKeyDown(KeyCode.LeftControl) && frutas>0) {
			vida += 20;
			frutas -= 1;
		}
		Vector3 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseInput.x = mouseInput.x - arma.transform.position.x;
		mouseInput.y = mouseInput.y - arma.transform.position.y;
		float angle = Mathf.Atan2(mouseInput.y, mouseInput.x) * Mathf.Rad2Deg;
		arma.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle-90));

		if(Input.GetMouseButtonDown(0) && !jogado && stamina >= 40) {
			stamina -= 40;
			arma.SetActive(jogado);
			jogarVara(Camera.main.ScreenToWorldPoint(Input.mousePosition), objThrow);
		}

		stamina += Time.deltaTime * speedRecharge;
		if(stamina >= 100) {
			stamina = 100;
		}
		
		if(vida <= 0) {
			Destroy(gameObject);
		}
		if(vida >= 100) {
			vida = 100;
		}

		vidaUI.fillAmount = vida / 100;
		staminaUI.fillAmount = stamina / 100;

		if(inimigosMortos >= maxInim) {
			vida+=55;
			inimigosMortos = 0;
		}
		inimigoUI.fillAmount = inimigosMortos / 10f;
		
		

		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		if(moveInput.x != 0 && moveInput.y != 0) {
			limitador = 1.5f;
		} else {
			limitador = 1f;
		}

		if(moveInput.x != 0) {
			anim.SetBool("andando", true);
		} else { anim.SetBool("andando", false); }

		if(moveInput.x == 1) {
			transform.eulerAngles = new Vector3(0f,0f,0f);
		} else if(moveInput.x == -1) {
			transform.eulerAngles = new Vector3(0f,180f,0f);
		}

		if(moveInput.y == 1) {
			anim.SetBool("subindo", true);
			anim.SetBool("descendo", false);
		} else if(moveInput.y == -1) {
			anim.SetBool("descendo", true);
			anim.SetBool("subindo", false);
		} else {
			anim.SetBool("descendo", false);
			anim.SetBool("subindo", false);
		}
		
	}
	void LateUpdate() {
		Vector3 x;
		Vector3 y;
		if(transform.position.x <= -1.92f) {
			x = new Vector3(-1.92f, 0, -10);
		} else if (transform.position.x >= 2.15f) {
			x = new Vector3(2.15f, 0, -10);
		} else {
			x =new Vector3(transform.position.x,0,-10);	
		}
		
		if(transform.position.y <= -4.51f) {
			y = new Vector3(0, -4.51f, 0);
		} else if (transform.position.y >= 6.32f) {
			y = new Vector3(0, 6.32f, 0);
		} else {
			y =new Vector3(0,transform.position.y,0);	
		}

		cam.transform.position = x + y;
	}

	void FixedUpdate() {
		rb.velocity = moveInput * (speed/limitador);
	}
	void OnCollisionEnter2D(Collision2D o) {
		if(o.gameObject.CompareTag("enemy")) {
			vida -= 10;
		}
	}

	void OnDestroy() {
		SceneManager.LoadScene("tanqs");
	}
}
