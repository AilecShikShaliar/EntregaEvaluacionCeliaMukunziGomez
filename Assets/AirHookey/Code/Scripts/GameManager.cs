using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Librer�a para cambiar entre escenas
using TMPro; //Librer�a para poder usar los TextMeshPro

public class GameManagerAirHookey: MonoBehaviour
{
    //Referencia para guardar la direcci�n del disco
    Vector2 direction;
    //Referencia al disco
    public GameObject disk;
    //Referencia para las palas
    public GameObject playerLeft, playerRight, playerUp, playerDown;
    //Referencia para el texto del ganador
    public GameObject panelWin;

    //Referencias a las porterias
    public GameObject goalLeft, goalRight, goalUp, goalDown;
    //Referencia para acceder al cartel de ganar
    public TextMeshProUGUI winText;

    //M�todo para hacer lo que ocurre al marcar un punto
    public void GoalScored()
    {
        //Ponemos el disco al marcar un gol en la posici�n de origen
        disk.transform.position = Vector2.zero; //Vector2.zero <-> new Vector2(0,0)
        //Ponemos a los jugadores en sus posiciones de origen
        playerLeft.transform.position = new Vector2(-6.75f, 0f);
        playerRight.transform.position = new Vector2(6.75f, 0f);
        playerUp.transform.position = new Vector2 (0f, 3.99f);
        playerDown.transform.position = new Vector2 (0f, -3.99f);

        //Aqu� guardamos la velocidad en X que llevaba el disco e invertimos su signo
        direction = new Vector2(-disk.GetComponent<Rigidbody2D>().velocity.x, 0f);

        //Paramos el disco
        disk.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Usando Invoke esperamos X segundos antes de llamara un m�todo
        Invoke("LaunchDisk", 2.0f); //Le decimos el m�todo que quiero invocar y el tiempo que tiene que pasar en segundos para que eso suceda
    }

    //M�todo para hacer que el disco se lance
    void LaunchDisk()
    {
        //Aplicamos esa nueva direcci�n en el disco
        disk.GetComponent<Rigidbody2D>().velocity = direction;
    }

    //M�todo para resetear el juego cuando uno gana
    public void WinGame()
    {
        //SetActive sirve para activar o desactivar objetos
        panelWin.SetActive(true);
      
        if (goalLeft.GetComponent<GoalZone>().score > 9)
            winText.text = "El Jugador Derecha ha ganado";
        else if(goalRight.GetComponent<GoalZone>().score > 9)

            winText.text = "El Jugador Izquierda ha ganado";
        else if (goalUp.GetComponent<GoalZone>().score > 9)
             winText.text = "El jugador Arriba ha ganado";

        else if (goalDown.GetComponent<GoalZone>().score > 9)
             winText.text = "El jugador Arriba ha ganado";
     
        Invoke("GoMenu", 2f);
    }

    //M�todo para ir a la pantalla de t�tulo
    public void GoMenu()
    {
        //Vamos a la escena de t�tulo
        SceneManager.LoadScene("MainMenu");
    }
}
