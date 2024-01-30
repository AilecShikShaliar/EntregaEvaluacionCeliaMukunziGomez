using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    //Velocidad a la que bajan los fondos
    public float yVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, yVelocity, 0f);
        if (transform.position.y < -3.84f)
            transform.position = new Vector2(0f, 15.36f);
    }
}
