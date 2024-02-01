using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSuika : MonoBehaviour
{
    //Referencia al UIController
    public UIControllerSuika uIReference;

    //Array con todos los tipos de Slimes
    public GameObject[] slimes;

    //M�todo para cuando llegamos al GameOver
    public void GameOver()
    {
        //Activamos el texto de GameOver
        uIReference.gameOverText.gameObject.SetActive(true);
    }
}
