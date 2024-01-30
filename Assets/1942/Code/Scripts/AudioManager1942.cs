using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class AudioManager1942 : MonoBehaviour
{
    //Array para los sonidos y para las músicas
    public AudioSource[] sfx, music;

    //Hacemos esta clase(este script) Singleton para poder sacar su información desde muchos objetos
    //Static es que solo hay uno de este tipo en todo el juego
    public static AudioManager1942 amInstance;//Es el mismo tipo que el script
    //El singleton es más apropiado usarlo, cuando vamos a hacer muchísimas referencias desde distintos objetos
    private void Awake()
    {
        //Si la referencia no está llena, la lleno
        if (amInstance == null)
            amInstance = this;//La relleno con todo el contenido de este código
    }

    // Start is called before the first frame update
    void Start()
    {
        //Ponemos la música en marcha
        PlayMusic();
    }

    //Método para reproducir sonidos
    public void PlaySFX(int soundToPlay)
    {
        //Si ya se estaba reproduciendo ese sonido, lo paramos
        sfx[soundToPlay].Stop();
        //Reproducimos el sonido pasado por parámetro
        sfx[soundToPlay].Play();
    }

    //Método para reproducir la música del juego
    public void PlayMusic()
    {
        //Si estamos en el menú principal
        if (SceneManager.GetActiveScene().name == "MainMenu")
            //Reproducimos la primera música del array
            music[0].Play();
        //Si estamos en el juego
        if (SceneManager.GetActiveScene().name == "Game")
            //Reproducimos la segunda música del array
            music[1].Play();
    }
}
