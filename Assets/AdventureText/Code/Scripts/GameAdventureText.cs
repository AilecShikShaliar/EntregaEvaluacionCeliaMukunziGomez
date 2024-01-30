using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class Game : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI textComponent;

    State stateRef; 

    [SerializeField] State startingState;

    public GameObject background;
    public Image imagen;

    void Start()
    {
        
        stateRef = startingState;
        
        textComponent.text = stateRef.GetStateStoryText();

        imagen =  background.GetComponent<Image>();

        imagen.sprite = stateRef.GetStateImage();

    }

    // Update is called once per frame
    void Update()
    {
        
        ManageStates();
    }

    
    void ManageStates()
    {
        
        State[] nextStates = stateRef.GetNextStates();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateRef = nextStates[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            stateRef = nextStates[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
           
            stateRef = nextStates[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            stateRef = nextStates[3];
        }
        
        textComponent.text = stateRef.GetStateStoryText();

        imagen.sprite = stateRef.GetStateImage();

    }
}
