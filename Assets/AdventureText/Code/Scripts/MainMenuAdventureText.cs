using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuAdventureText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            
           transform.position = new Vector2(-1.51f, -2.54f);
        }
        
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
           
            transform.position = new Vector2(-2.72f, -3.98f);
        }

        if(transform.position.y == -2.54f && Input.GetKeyDown(KeyCode.Space))
        {
          
            SceneManager.LoadScene("GameAdventureText");
        }

        
    }

    
    

}
