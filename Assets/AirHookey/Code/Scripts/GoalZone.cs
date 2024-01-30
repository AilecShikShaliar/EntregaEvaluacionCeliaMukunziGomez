using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Librer�a para poder acceder a los TextMeshPro

public class GoalZone : MonoBehaviour
{
    //Referencia para acceder al marcador de puntos
    public TextMeshProUGUI scoreText;
    //Variable para guardar los puntos marcados en esa porter�a
    public int score;

    //Referencia al GameManager
    public GameManagerAirHookey gMReference;

    //Antes de que empiece el juego
    private void Awake()
    {
        //Ponemos la puntuaci�n en 0
        score = 0;
        //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
        scoreText.text = score.ToString();

        //Para transformar un int en un string hay 3 maneras
        //scoreText.text = score + ""; le sumo un string vac�o a ese int, luego ya ser� todo un string
        //scoreText.text = score.ToString(); transformo(cast) del int en un string
        //scoreText.text = "Score: " + score; a un string le ponemos despu�s un int, con lo cu�l ya todo es string
    }

    //M�todo para detectar cuando algo ha entrado en el trigger(zona de gol)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Solo aquellos GameObjects etiquetados como Disco, que hayan entrado en el trigger
        if (collision.CompareTag("Disco"))
        {
            //Sumo 1 a la puntuaci�n
            score++;
            //Si la puntuaci�n es mayor de 9
            if (score > 9)
            {
                //Ejecuto el m�todo que hace que se pase a otra ronda
                gMReference.GoalScored();
                //Ejecuto el m�todo que termina esta partida
                gMReference.WinGame();
            }
            //Si no
            else
            {
                //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
                scoreText.text = score.ToString();
                //Ejecuto el m�todo que hace que se pase a otra ronda
                gMReference.GoalScored();
            }
        }
    }
}
