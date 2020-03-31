using UnityEngine;
using System.Collections;

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


	void Start()
	{
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator>();
		anim.SetTrigger("Parado");
	}

	void Update()
	{
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
			Destroy(other.gameObject);
		}
		if (other.gameObject.name == "ChaveVerde")
		{
			chaveVerde = true;
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

	/*
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Colidiu");
		if (collision.gameObject.name == "door")
		{
			if ((chaveAzul == true) && (chaveVerde == true))
			{
				collision.gameObject.SendMessage("AbrePorta");
			}
		}
	}
	*/

}
