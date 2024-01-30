using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Variable para controlar la velocidad de la bala
    public float bulletSpeed;
    //Referencia al RB de la bala
    public Rigidbody2D bRB;

    // Start is called before the first frame update
    void Start()
    {
        //Si la bala es la del jugador
        if(gameObject.CompareTag("Bullet"))
            //Hacemos que la bala avance hacia arriba
            bRB.velocity = new Vector2(0f, bulletSpeed);
        //Si la bala es la del enemigo
        if(gameObject.CompareTag("EnemyBullet"))
            //Hacemos que la bala avance hacia abajo
            bRB.velocity = new Vector2(0f, -bulletSpeed);
        //Destruiría el objeto al que está asociado este código pasados 2 segundos
        //Destroy(gameObject, 2f);
    }

    //Método de Unity que hace un acción cuando se deja de ver el objeto por la cámara
    void OnBecameInvisible()
    {
        //Destruye el objeto donde está asociado este código
        Destroy(gameObject);
    }

    //Método para saber cuando la bala se choca contra algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el objeto contra el que colisionamos es una bala o una bala enemiga
        if(collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            //Sonido de impacto de balas entre sí
            AudioManager1942.amInstance.PlaySFX(4);
            //Se destruye el otro objeto
            Destroy(collision.gameObject);
            //Se destruye este objeto
            Destroy(gameObject);
        }
    }
}
