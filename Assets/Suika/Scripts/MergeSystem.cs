using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    //Generamos una enumeraci�n con todos los tipos de Slime disponibles
    public enum SlimeType { slime0, slime1, slime2, slime3, slime4, slime5, slime6, slime7, slime8, slime9}

    //Referencia para conocer de que tipo es este Slime
    public SlimeType slimeType;

    //Obtenemos una referencia al GameManager
    GameManagerSuika gMReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al GameManager
        //GameObject.Find("Objeto") => busca el objeto por nombre en la escena
        gMReference = GameObject.Find("GameManager").GetComponent<GameManagerSuika>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si este Slime est� por encima de la l�nea roja y no tiene padre
        if (transform.position.y > 7.5 && transform.parent == null)
            //Llamamos al m�todo de GameOver
            gMReference.GameOver();
    }

    //M�todo para trabajar con las colisiones entre los Slimes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el objeto que ha colisionado es un Slime y es el mismo tipo de Slime que este
        if(collision.gameObject.CompareTag("Slime") && collision.gameObject.GetComponent<MergeSystem>().slimeType == slimeType)
        {
            //Debug.Log("Merge");
            //Desactivamos el Slime que se ha chocado con este
            collision.gameObject.SetActive(false);
            //Desactivamos este Slime
            gameObject.SetActive(false);
            //Hacemos aparecer el siguiente tipo de Slime de la lista
            //Cuando ponemos ++ delante significa que se suma 1 antes de usar
            Instantiate(gMReference.slimes[(int)++slimeType], transform.position, transform.rotation);
        }
    }
}
