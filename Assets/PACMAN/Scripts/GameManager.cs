using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variable para controlar el tiempo en el que PacMan es invencible
    public float invincibleTime = 0.0f;

    //Creamos el Singleton del GameManager
    public static GameManager gmSharedInstance;

    private void Awake()
    {
        //Si la referencia está vacía
        if (gmSharedInstance == null)
            //La rrellenamos con todo el contenido de este script
            gmSharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Hacemos que el contador de tiempo vaya decreciendo hasta que se vacíe
        //Si el contador no está vacío
        if (invincibleTime > 0)
            //Usando el Time.deltaTime le restamos 1 cada segundo al contador
            //Porque le restamos las partes proporcionales de ese segundo divididas en frames
            invincibleTime -= Time.deltaTime;
    }

    //Es un método para inicializar el contador de tiempo de invencibilidad.
    //Al llamarlo le pasamos ese tiempo por parámetro
    public void MakeInvincibleFor(float numberOfSeconds)
    {
        //Inicializamos el contador de tiempo de invencibilidad
        invincibleTime = numberOfSeconds;
    }
}
