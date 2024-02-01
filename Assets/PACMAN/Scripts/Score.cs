using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    public int points;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScorePoints()
    {
        points += 1;
        score.text = "" + points;
    
    }
}
