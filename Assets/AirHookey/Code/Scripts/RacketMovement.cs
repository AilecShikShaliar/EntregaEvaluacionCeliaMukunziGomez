using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour
{
    //Velocidad de la raqueta
    public float racketSpeed = 25f;
    //Creo una referencia al eje que quiero utilizar
    public string axe;
    //Esto es una referencia al RigidBody del jugador que nos permite cambiar su velocidad
    public Rigidbody2D rb;

    public bool isHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        //As� inicializar�amos el RigidBody desde c�digo
        //rb = GetComponent<Rigidbody2D>();
    }

    // Ponemos FixedUpdate para que la longitud de cada frame en segundos mida lo mismo, y as� el movimiento sea suavizado
    void FixedUpdate()
    {
        if (isHorizontal)
        {
            float horizontalMovement = Input.GetAxis(axe);
            rb.velocity = new Vector2(horizontalMovement, 0f ) * racketSpeed;


        }
        else
        {
          float verticalMovement = Input.GetAxis(axe);
          rb.velocity = new Vector2(0f, verticalMovement) * racketSpeed;

        }

       
       
    }
}
