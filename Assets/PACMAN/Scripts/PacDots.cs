using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDots : MonoBehaviour
{
    //M�todo para conocer cuando un objeto se ha metido en la zona de trigger de los PacDots
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el objeto que ha entrado en el trigger est� etiquetado como Player
        if (collision.CompareTag("Player"))
        {
            //Podr�a sumar puntos

            GameManager.gmSharedInstance.gameObject.GetComponent<Score>().ScorePoints();

            //Recogemos el objeto PacDot concreto, en nuestro caso lo eliminamos
            Destroy(gameObject);
        }
    }
}
