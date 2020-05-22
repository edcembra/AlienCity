using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public float MoveSpeed;
	public float RotationSpeed;
	CharacterController cc;
	private Animator anim;
	protected Vector3 gravidade = Vector3.zero;
	protected Vector3 move = Vector3.zero;
	private bool jump = false;

	private int health;
	public Slider sliderHP;

	void Start()
	{
		health = 10;
		sliderHP.value = 1;

		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator>();
		anim.SetTrigger("Parado");
	}

	void Update()
	{
		Vector3 move = CrossPlatformInputManager.GetAxis ("Vertical") * transform.TransformDirection (Vector3.forward) * MoveSpeed;
		transform.Rotate (new Vector3 (0, CrossPlatformInputManager.GetAxis ("Horizontal") * RotationSpeed * Time.deltaTime, 0));
		
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
			if(CrossPlatformInputManager.GetButtonDown("Jump"))
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
	public void DoDamage(int damage)
	{
		Handheld.Vibrate();
		health -= damage;
		sliderHP.value = health / 10f;
		if (health <= 0)
		{
			Handheld.Vibrate();
			Respawn();
		}
	}

	public void Respawn()
	{
		health = 10;
		sliderHP.value = 1;

		this.gameObject.transform.position = new Vector3(0f, 0f, -43.1f);

	}

}
