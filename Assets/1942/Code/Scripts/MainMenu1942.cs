using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Librería que nos permite cambiar de escenas

public class MainMenu1942 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Si pulso esta tecla hace el método que me lleva al juego
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();
    }

    //Método que me lleva al juego
    public void StartGame()
    {
        //Llamamos a la corrutina
        StartCoroutine(StartGameCo());
    }

    //Corrutina para iniciar el juego
    private IEnumerator StartGameCo()
    {
        //Ponemos el sonido de empezar el juego
        AudioManager1942.amInstance.PlaySFX(9);
        //Esperamos un segundo
        yield return new WaitForSeconds(1f);
        //Cargamos la escena de juego
        SceneManager.LoadScene("Game1942");
    }
}
