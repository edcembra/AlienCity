using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
	/*[HideInInspector]*/ public bool tocaChao = false;
	
//	public bool armado = false; // *************** Variável de controle. Guarda a informação se a personagem está aramada ou não ***************
	public int arma; // *************** Variável de controle. Guarda a informação de qual arma a personagem está usando ***************
	public GameObject Tiro1_Prefab; // *************** Referência ao Prafab do projétil da arma1 ***************
	public GameObject Tiro2_Prefab; // *************** Referência ao Prafab do projétil da arma2 ***************

	private AudioSource audio; // *************** Referência para o componente AudioSource ***************
	public AudioClip somDoTiro;

	public Image iconeArma;

	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;

	public Image vida;
	private MensagemControle MC;

	private float translationY;
	private float translationX;

	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		audio = GetComponent<AudioSource>();

		tocaChao = true; // ****************************** Inicia a cena com a personagem tocando o chão ******************************
		arma = 0;
		iconeArma.color = Color.clear;

		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Implementar Pulo Aqui! 

		// ************************* Implementa o botão para pegar sacar/trocar/guardar a arma *************************
		if (Input.GetButtonDown("Fire2"))
		{
			if (arma <= 1)
			{
				arma++;
				iconeArma.color = Color.white;
				iconeArma.sprite = Resources.Load<Sprite>("Imagens/arma"+arma);
			}
			else
			{
				arma = 0;
				iconeArma.color = Color.clear;
				iconeArma.sprite = Resources.Load<Sprite>("");
			}
		}

		
		// ************************* Instancia o tiro *************************
		if (Input.GetButtonDown("Fire1"))
		{
			if (arma == 1) // *************** Instancia o tiro da arma 1 ***************
			{
				/*				Vector3 position = new Vector3(transform.position.x + (transform.localScale.x/2), transform.position.y - (transform.localScale.y - 1.1f)); // Armazena a posição do player na variável 'position'
								GameObject instance = Instantiate(Tiro1_Prefab, position, Quaternion.identity) as GameObject; // Instancia o Prefab do tiro e guarda uma referência
								if (viradoDireita == true)
									instance.gameObject.SendMessage("Virado_D"); // *************** Envia mensagem para a instância do projétil 
								else
									instance.gameObject.SendMessage("Virado_E"); */

				if (viradoDireita == true)
				{
					Vector3 position = new Vector3(transform.position.x + ((transform.localScale.x+0.5f)), transform.position.y - (transform.localScale.y - 1.1f)); // Armazena a posição do player na variável 'position'
					GameObject instance = Instantiate(Tiro1_Prefab, position, Quaternion.identity) as GameObject; // Instancia o Prefab do tiro e guarda uma referência
					instance.gameObject.SendMessage("Virado_D"); // *************** Envia mensagem para a instância do projétil 
				}
				else
				{
					Vector3 position = new Vector3(transform.position.x + ((transform.localScale.x-0.5f)), transform.position.y - (transform.localScale.y - 1.1f)); // Armazena a posição do player na variável 'position'
					GameObject instance = Instantiate(Tiro1_Prefab, position, Quaternion.identity) as GameObject; // Instancia o Prefab do tiro e guarda uma referência
					instance.gameObject.SendMessage("Virado_E");
				}
				audio.clip = somDoTiro;
				audio.Play(); ;
			}
			if (arma == 2) // *************** Instancia o tiro da arma 2 ***************
			{
				/*				Vector3 position = new Vector3(transform.position.x + (transform.localScale.x/2), transform.position.y - (transform.localScale.y - 1.1f)); // Armazena a posição do player na variável 'position'
								GameObject instance = Instantiate(Tiro1_Prefab, position, Quaternion.identity) as GameObject; // Instancia o Prefab do tiro e guarda uma referência
								if (viradoDireita == true)
									instance.gameObject.SendMessage("Virado_D"); // *************** Envia mensagem para a instância do projétil 
								else
									instance.gameObject.SendMessage("Virado_E"); */

				if (viradoDireita == true)
				{
					Vector3 position = new Vector3(transform.position.x + ((transform.localScale.x + 0.5f)), transform.position.y - (transform.localScale.y - 1.1f)); // Armazena a posição do player na variável 'position'
					GameObject instance = Instantiate(Tiro2_Prefab, position, Quaternion.identity) as GameObject; // Instancia o Prefab do tiro e guarda uma referência
					instance.gameObject.SendMessage("Virado_D"); // *************** Envia mensagem para a instância do projétil 
				}
				else
				{
					Vector3 position = new Vector3(transform.position.x + ((transform.localScale.x - 0.5f)), transform.position.y - (transform.localScale.y - 1.1f)); // Armazena a posição do player na variável 'position'
					GameObject instance = Instantiate(Tiro2_Prefab, position, Quaternion.identity) as GameObject; // Instancia o Prefab do tiro e guarda uma referência
					instance.gameObject.SendMessage("Virado_E");
				}
				audio.clip = somDoTiro;
				audio.Play(); ;
			}
		}

	}

	void FixedUpdate()
	{
//		float translationY = 0;
//		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		translationY = 0;
		translationX = Input.GetAxis("Horizontal") * Velocidade;

		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
		/*
				if (translationX != 0 && tocaChao) {
					anim.SetTrigger ("corre");
				} else {
					anim.SetTrigger("parado");
				}
		*/


		// ******************** Testa se a personagem está em movimento, tocando no chão, ou armada, para controlar a animção ********************
		if (translationX != 0 && tocaChao)
		{
			if (arma == 0)
				anim.SetTrigger("corre");
			else
				if (arma == 1)
				anim.SetTrigger("corre_arma_1");
			else
				if (arma == 2)
				anim.SetTrigger("corre_arma_2");

		}
		else
		{
			if (arma == 0)
				anim.SetTrigger("parado");
			else
				if (arma == 1)
				anim.SetTrigger("parado_arma_1");
			else
				if (arma == 2)
				anim.SetTrigger("parado_arma_2");
		}



		//Programar o pulo Aqui! 

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}

	}
	void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}
	
}
