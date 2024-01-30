using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    //Velocidad del disco
    public float diskSpeed = 25f;

    //Esto es una referencia al RigidBody del disco que nos permite cambiar su velocidad
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //El disco empieza con una velocidad que lo impulsa a la derecha
        rb.velocity = Vector2.right * diskSpeed; //Equivale a new Vector2(1, 0)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
     * El objeto collision del paréntesis contiene la información del choque
     * En particular, nos interesa saber cuando choca contra una pala.
     * -collision.gameObject: tiene información del objeto contra el cuál ha colisionado (la pala)
     * -collision.transform.position: tiene información de la posición (pala)
     * -collision.collider: tiene información del collider (pala)
     */

    /* Es un método de Unity que detecta colisión entre dos GO.
     * Al chocar el disco contra el objeto que choca le pasa su Colisión por parámetro */
    private void OnCollisionEnter2D(Collision2D collision) //El parámetro collision es el objeto que ha chocado contra el disco
    {
        //Si el disco colisiona con la pala izquierda
        if(collision.gameObject.name == "PlayerLeft")
        {
            //Obtenemos el factor de golpeo, pasándole la posición del disco, la posición de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float yF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            /*Le damos una nueva dirección al disco
             * En este caso con una X a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(1, yF).normalized; //Hacemos que no acelere en su movimiento en diagonal
            //Le decimos al disco que salga con esa velocidad previamente calculada
            rb.velocity = direction * diskSpeed;
        }
        //Si el disco colisiona con la pala derecha
        if (collision.gameObject.name == "PlayerRight")
        {
            //Obtenemos el factor de golpeo, pasándole la posición del disco, la posición de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float yF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            /*Le damos una nueva dirección al disco
             * En este caso con una X a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(-1, yF).normalized; //Hacemos que no acelere en su movimiento en diagonal
            //Le decimos al disco que salga con esa velocidad previamente calculada
            rb.velocity = direction * diskSpeed;
        }
    }

    /*
     * 1 - El disco choca contra la parte superior de la pala
     * 0 - El disco choca contre el centro de la pala
     * -1 - El disco choca contra la parte inferior de la pala
     */
    /*
     * Es un método de tipo 3. En este caso le pasamos 3 parámetros:
     * -posición actual del disco
     * -posición actual de la pala
     * -altura de la pala
     * Y el método tal y como le indicamos nos devuelve una variable de tipo float
     */
    private float HitFactor(Vector2 diskPosition, Vector2 racketPosition, float racketHeight)
    {
        return (diskPosition.y - racketPosition.y) / racketHeight;
    }
}
