using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public void AdventureText()
    {
        SceneManager.LoadScene("MainMenuAdventureText");
    
    }
    public void AirHookey()
    {
        SceneManager.LoadScene("MainMenuAirHookey");

    }

    public void Game1942()
    {
        SceneManager.LoadScene("MainMenu1942");

    }
   public void TetrisGame()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void PacmanGame()
    {
        SceneManager.LoadScene("PacmanGame");

    }

    public void SuikaGame()
    {
        SceneManager.LoadScene("SuikaGame");

    }
}
