using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    //Velocidad de PacMan
    public float speed = 2f;
    //Referencia al RigidBody
    public Rigidbody2D theRB;
    //Referencia al Animator
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            theRB.velocity = new Vector2(-speed, 0f);
            //Cambiamos el parámetro del Animator (DirX) para que haga animación izquierda
            anim.SetFloat("DirX", -1);
            //Anulamos la DirY
            anim.SetFloat("DirY", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            theRB.velocity = new Vector2(speed, 0f);
            //Cambiamos el parámetro del Animator (DirX) para que haga animación derecha
            anim.SetFloat("DirX", 1);
            //Anulamos la DirY
            anim.SetFloat("DirY", 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            theRB.velocity = new Vector2(0f, speed);
            //Cambiamos el parámetro del Animator (DirY) para que haga animación arriba
            anim.SetFloat("DirY", 1);
            //Anulamos la DirX
            anim.SetFloat("DirX", 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            theRB.velocity = new Vector2(0f, -speed);
            //Cambiamos el parámetro del Animator (DirY) para que haga animación abajo
            anim.SetFloat("DirY", -1);
            //Anulamos la DirX
            anim.SetFloat("DirX", 0);
        }
    }

    //Método para hacer que PacMan muera
    public void PacManDead()
    {
        //Destruimos a PacMan
        Destroy(gameObject);
    }


    //Método para conocer la reacción de PacMan al impactar contra un fantasma
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el objeto que se ha metido en el trigger de PacMan es un enemigo y puede morir
        if (collision.CompareTag("Enemy") && collision.GetComponent<GhostMovement>().canDie)
        {
            //Destruye al fantasma
            Destroy(collision.gameObject);
        }
    }

}
