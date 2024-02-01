using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    //Necesitamos un array de posiciones llamado Waypoints(puntos de ruta, las chinchetas).
    //Cada fantasma puede tener un número de puntos de ruta distinto
    public Transform[] waypoints;
    //Inicializo la posición en la que se encuentra el fantasma. Posición 0 del array
    //Luego este valor irá variando
    int currentWaypoint = 0;
    //Velocidad del fantasma
    public float speed = 0.3f;
    //Variable para saber si el fantasma es vulnerable
    public bool canDie;
    //Referencia al Rigidbody del fantasma
    public Rigidbody2D ghostRB;
    //Referencia al Animator del fantasma
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Rigidbody
        ghostRB = GetComponent<Rigidbody2D>();
        //Inicializamos el Animator
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Si PacMan sigue siendo invencible
        if (GameManager.gmSharedInstance.invincibleTime > 0)
        {
            //El fantasma cambia a azul
            anim.SetBool("PacManInvencible", true);
            //El fantasma puede morir
            canDie = true;
        }
        else
        {
            //El fantasma queda como al principio
            anim.SetBool("PacManInvencible", false);
            //El fantasma no puede morir
            canDie = false;
        }
            
    }

    // Usamos Fixed porque es un movimiento físico y continuo y automático
    void FixedUpdate()
    {
        //Distancia al punto de ruta, entre la posición actual del fantasma
        // y el punto de ruta hacia el que se está dirigiendo
        float distanceToWaypoint = Vector2.Distance(transform.position, waypoints[currentWaypoint].position);
        //Debug.Log(distanceToWaypoint);

        //Si la distancia hasta el punto de ruta es menor que 0.1 es que he llegado a la posición
        if(distanceToWaypoint < 0.1f)
        {
            //Ir al siguiente Waypoint. Lo de abajo es equivalente a la siguiente línea
            //Esto es un operador ternario. => condicion ? consecuencia : alternativa
            //currentWaypoint = (currentWaypoint < waypoints.Length - 1) ? currentWaypoint += 1 : currentWaypoint = 0;
            //Si el número del punto en el que está el fantasma es menor
            //que la cantidad de los que hay guardados
            if (currentWaypoint < waypoints.Length - 1)
                //Avanzamos al siguiente punto
                currentWaypoint++;
            //Si por el contrario el número del punto en el que está el fantasma
            //es igual o mayor que los que hay guardados
            else
                //Reseteamos al primer punto de los guardados
                currentWaypoint = 0;

            //Nueva dirección para calcular la animación si cambiamos de dirección:
            //donde va - donde está ahora
            Vector2 newDirection = waypoints[currentWaypoint].position - transform.position;
            //Cambiamos las animaciones
            anim.SetFloat("DirX", newDirection.x);
            anim.SetFloat("DirY", newDirection.y);

        }
        //Si el fantasma aún no ha llegado a su destino
        else
        {
            //Creo un Vector2 para moverme desde donde esté el fantasma ahora mismo,
            //hasta el siguiente Waypoint a una velocidad
            Vector2 newPos = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
            //Hacemos que se mueva a la posición que le toca
            ghostRB.MovePosition(newPos);
        }
    }

    //Método para conocer la reacción de un fantasma al impactar contra PacMan
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el objeto que se ha metido en el trigger del fantasma es el jugador y el enemigo no puede morir
        if (collision.CompareTag("Player") && !canDie)
        {
            //Destruye a PacMan
            Debug.Log("Jugador muerto");
            //Destruye a PacMan(obteniendo de este GameObject, su código para poder coger de él el método de PacManDead())
            collision.gameObject.GetComponent<PacManMovement>().PacManDead();
        }
    }
}
