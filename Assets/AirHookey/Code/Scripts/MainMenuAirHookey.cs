using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Librer�a que sirve para cambiar entre escenas

public class MainMenuAirHookey : MonoBehaviour
{
    

    public void ChangeScene()
    {
        //Carga la escena con ese nombre
        SceneManager.LoadScene("Game");
    }
}
