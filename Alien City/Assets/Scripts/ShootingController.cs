    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Animator anim; // *************** Referência para o componente Animator do objeto ***************
    public Rigidbody2D rigidBody; // *************** Referência para o componente RigidBody2D do objeto ***************
    public float velocidadeTiro1; // *************** Velocidade do tiro ***************
    public float timeToDestroy; // *************** Tempo de duração do projétil ***************
    public bool viradoDireita; // *************** Variável de controle para direção do projétil ***************


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // *************** Guarda uma referência para o componente Animator do objeto ***************
        rigidBody = GetComponent<Rigidbody2D>(); // *************** Guarda uma referência para o componente RigibBody2D do objeto ***************
        velocidadeTiro1 = 20f;
        timeToDestroy = 5f;
    }

    // Update is called once per frame
    void Update()
        {

        }

    void FixedUpdate()
    {
        if (viradoDireita)
 //           rigidBody.velocity = new Vector2(velocidadeTiro1, rigidBody.velocity.y);
            rigidBody.AddForce(transform.right * velocidadeTiro1);
        else
 //           rigidBody.velocity = new Vector2(-velocidadeTiro1, rigidBody.velocity.y);
            rigidBody.AddForce(transform.right * -velocidadeTiro1);

        Destroy(this.gameObject, timeToDestroy); // *************** Destrói o projétil depois de transcorrido seu tempo de duração
    }

    void Virado_D()
    {
        viradoDireita = true;
    }

    void Virado_E()
    {
        viradoDireita = false;
        Flip();
    }

    void Flip()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objectTag = collision.gameObject.tag;

        if ((objectTag != "Player") && (objectTag != "Tiro"))
        {
            //rigidBody.AddForce(transform.right * -velocidadeTiro1*10);
            anim.SetTrigger("Destruir");
            Destroy(this.gameObject);
        }
    }
}
