using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public float MoveSpeed;
	public float RotationSpeed;
	CharacterController cc;
	private Animator anim;
	protected Vector3 gravidade = Vector3.zero;
	protected Vector3 move = Vector3.zero;
	private bool jump = false;

	public bool chaveAzul = false; // Armazena a chave azul
	public bool chaveVerde = false; // Armazena a chave verde

	public Image hudChaveVerde, hudChaveAzul;

	private int health;
	public Slider sliderHP;

	public Text hudGemas;

	public int totalGemas;

	public int lives;
	public Image ImagemLife1, ImagemLife2, ImagemLife3;

	void Start()
	{
		lives = 3;
		totalGemas = 0;
		health = 10;
		sliderHP.value = 1;

		hudChaveAzul.enabled = false;
		hudChaveVerde.enabled = false;

		ImagemLife1.enabled = true;
		ImagemLife2.enabled = true;
		ImagemLife3.enabled = true;

		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator>();
		anim.SetTrigger("Parado");
	}

	void Update()
	{
		Debug.Log("Health:" + health + "  Lives:" + lives);
		hudGemas.text = ("Gemas: " + totalGemas).ToString();

		if (lives == 2)
			ImagemLife3.enabled = false;
		if (lives == 1)
			ImagemLife2.enabled = false;
		if (lives == 0)
			ImagemLife1.enabled = false;

		Vector3 move = Input.GetAxis ("Vertical") * transform.TransformDirection (Vector3.forward) * MoveSpeed;
		transform.Rotate (new Vector3 (0, Input.GetAxis ("Horizontal") * RotationSpeed * Time.deltaTime, 0));
		
		if (!cc.isGrounded) {
			gravidade += Physics.gravity * Time.deltaTime;
		} 
		else 
		{
			gravidade = Vector3.zero;
			if(jump)
			{
				gravidade.y = 6f;
				jump = false;
			}
		}
		move += gravidade;
		cc.Move (move* Time.deltaTime);
		Anima ();
	}
	 
	void Anima()
	{
		if (!Input.anyKey)
		{
			anim.SetTrigger("Parado");
		} 
		else 
		{
			if(Input.GetKeyDown("space"))
			{
				anim.SetTrigger("Pula");
				jump = true;
			}
			else
			{
				anim.SetTrigger("Corre");
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "ChaveAzul")
		{
			chaveAzul = true;
			hudChaveAzul.enabled = true; 
			Destroy(other.gameObject);
		}
		if (other.gameObject.name == "ChaveVerde")
		{
			chaveVerde = true;
			hudChaveVerde.enabled = true;
			Destroy(other.gameObject);
		}

		if (other.gameObject.name == "AncoraPorta")
		{
			if ((chaveAzul == true) && (chaveVerde == true))
			{
				other.SendMessage("AbrePorta");
			}
		}

	}

	public void DoDamage (int damage)
	{
		health -= damage;
		sliderHP.value = health / 10f;
		if (health <= 0)
		{
			lives--;
			Respawn();
//			string cenaAtual = SceneManager.GetActiveScene().name;
//			SceneManager.LoadScene(cenaAtual);
		}
	}
	
	public void Respawn()
	{
		if (lives == 2)
			ImagemLife3.enabled = false;
		if (lives == 1)
			ImagemLife2.enabled = false;
		if (lives == 0)
			ImagemLife1.enabled = false;

		health = 10;
		sliderHP.value = 1;

		this.gameObject.transform.position = new Vector3(35.16f, 7.8f, -43.1f);

	}
}
