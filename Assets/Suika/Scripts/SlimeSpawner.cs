using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    //Array de slimes que podemos spawnear
    public GameObject[] slimesToSpawn;
    //El objeto actual instanciado
    GameObject currentSlime;
    //Posici�n inicial del Spawner
    Vector2 initialSpawnerPos;
    //Referencia al Rigibody del Spawner
    public Rigidbody2D spawnerRB;
    //Velocidad a la que se mueve el spawner
    public float moveSpeed = 4;
    //Variable para generar un n�mero aleatorio
    int random;
    //Referencia al Rigidbody del slime actual
    Rigidbody2D currentSlimeRB;
    //Variable que nos va a permitir pulsar o no
    bool canPress;

    //Referencia al UIController
    public UIControllerSuika uIReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el vector posici�n inicial
        initialSpawnerPos = transform.position;
        //Generamos un n�mero aleatorio entre 0 y 4
        random = Random.Range(0, 5);
        //Llamamos a la corrutina que genera un Slime
        StartCoroutine(NewSlimeCo());  
    }

    // Update is called once per frame
    void Update()
    {
        //Si podemos pulsar (osea movernos)
        if (canPress)
        {
            //Hacemos que se mueva este objeto a una velocidad dada
            spawnerRB.velocity = new Vector2(Input.GetAxis("Horizontal"), 0f) * moveSpeed;
            //Si este objeto Slime tiene padre
            if (currentSlime.transform.parent != null)
                //Haremos que la velocidad del Slime sea igual a la del Spawner
                currentSlimeRB.velocity = spawnerRB.velocity;

            //Si pulsamos la tecla espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Hacemos que no se pueda pulsar
                canPress = false;
                //Hacemos que el Slime tenga su gravedad normal
                currentSlimeRB.gravityScale = 1f;
                //Ponemos la velocidad del Slime a 0 para que le afecte solo la gravedad
                currentSlimeRB.velocity = Vector2.zero;//Vector2.zero = new Vector2(0,0)
                //Desemparentamos al Slime del Spawner
                currentSlime.transform.parent = null;
                //Generamos un nuevo Slime
                StartCoroutine(NewSlimeCo());
            }
        }
        //Si no podemos pulsar
        else
        {
            //Velocidad del spawner ser� cero
            spawnerRB.velocity = Vector2.zero;
            //El spawner volver� a su posici�n inicial
            transform.position = initialSpawnerPos;
        }
    }

    //Hacemos una corrutina para crear un nuevo Slime
    private IEnumerator NewSlimeCo()
    {
        //Le pedimos que espere un segundo
        yield return new WaitForSeconds(1f);
        //Hacemos aparecer el Slime aleatorio en una posici�n y con rotaci�n dadas
        //Para poder actuar sobre un objeto instanciado lo referencio
        currentSlime = Instantiate(slimesToSpawn[random], transform.position, transform.rotation);
        //De ese objeto(currentSlime) recogemos el RigidBody2D
        currentSlimeRB = currentSlime.GetComponent<Rigidbody2D>();
        //Ponemos la gravedad a 0 para que no caiga cuando el Slime es creado
        currentSlimeRB.gravityScale = 0f;
        //Hacemos hijo al Slime del Spawner
        currentSlime.transform.parent = transform;
        //Generamos un n�mero aleatorio entre 0 y 4
        random = Random.Range(0, 5);
        //Cambiamos la imagen en la UI por la del siguiente Slime que saldr�
        uIReference.nextSlime.sprite = slimesToSpawn[random].GetComponent<SpriteRenderer>().sprite;
        //Permitimos de nuevo que se pueda pulsar
        canPress = true;
    }
}
