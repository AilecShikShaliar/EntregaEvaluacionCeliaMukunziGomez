using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner1942 : MonoBehaviour
{
    //La jarra de agua. Rellena el contador
    public float spawnTime;
    //El contador de tiempo entre spawns
    private float spawnCounter;

    //Referencia a la nave enemiga
    public GameObject smallEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Iniciamos el contador
        spawnCounter = spawnTime;//Rellenamos el contador
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de spawn no está vacío
        if(spawnCounter > 0)
        {
            //Le restamos al contador 1 cada segundo
            spawnCounter -= Time.deltaTime;
        }
        //Si el contador está vacío
        else
        {
            //Debug.Log(spawnCounter);
            SpawnEnemy();
            //Reiniciamos el contador para que empiece de nuevo
            spawnCounter = spawnTime;
        }

    }

    public void SpawnEnemy()
    {
        //Cogemos un número aleatorio entre esos dos valores
        float xPosition = Random.Range(-1.0f, 1.0f);
        Debug.Log(xPosition);
        //Hacemos aparecer una nave, en la posición del spawner más una posición aleatoria en X, y con la rotación de la nave
        Instantiate(smallEnemy, transform.position + new Vector3(xPosition, 0f, 0f), smallEnemy.transform.rotation);
    }
}
