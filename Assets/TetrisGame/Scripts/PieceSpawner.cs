using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public GameObject pista;

    public GameObject piecePrefab;

    //Array donde guardamos los objetos que serán spawneados
    public GameObject[] levelPieces;

    //Variables para saber la pieza que tenemos y la siguiente que nos dará el juego
    public GameObject currentPiece, nextPiece;

    // Start is called before the first frame update
    void Start()
    {
        //Sacamos la siguiente pieza para tenerla visible
        nextPiece = Instantiate(levelPieces[0], this.transform.position, Quaternion.identity);
        //Sacará una pieza al azar al empezar el juego
        SpawnNextPiece();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Método para spawnear piezas
    public void SpawnNextPiece()
    {
       
        currentPiece = nextPiece;
        //Activo esa pieza (su script para que esta funcione)
        currentPiece.GetComponent<Piece>().enabled = true;
        Destroy(piecePrefab);
        //Para cada bloque dentro de esa pieza
        foreach (SpriteRenderer child in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            //Cogemos el color actual de ese bloque
            Color currentColor = child.color;
            //Hacemos algo transparente a ese bloque
            currentColor.a = 1.0f; //La transparencia va entre 0 y 1
            child.color = currentColor;
        }
        

        StartCoroutine("PrepareNextPiece");
        nextPiece.SetActive(true);
    }

    //Corrutina para preparar la siguiente pieza
    IEnumerator PrepareNextPiece()
    {
        //Esperamos antes de nada 0.1 segundos
        yield return new WaitForSecondsRealtime(0.1f);

        //Tomamos un valor aleatorio comprendido en la longitud de todo el array
        int i = Random.Range(0, levelPieces.Length); //Random.Range(valor más bajo, valor más alto)
        Spoiler(i);
        //Instanciamos la pieza seleccionada en la posición del objeto spawneador
        nextPiece = Instantiate(levelPieces[i], this.transform.position, Quaternion.identity); //Quaternion.identity es la rotación que tuviera en ese momento el objeto spawneador
        //Instantiate(objeto a instanciar, posición en la que se instancia, rotación con la que se instancia)
        //Desactivamos para que la siguiente pieza no se mueva su script
        nextPiece.GetComponent<Piece>().enabled = false;
        //Para cada bloque dentro de esa pieza
        foreach (SpriteRenderer child in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            //Cogemos el color actual de ese bloque
            Color currentColor = child.color;
            //Hacemos algo transparente a ese bloque
            currentColor.a = 0.3f; //La transparencia va entre 0 y 1
            child.color = currentColor;
        }
        nextPiece.SetActive(false);

    }
    private void Spoiler(int spawnerSpoiler)
    {

        //Instanciamos la pieza seleccionada en la posición del objeto spawneador
        piecePrefab = Instantiate(levelPieces[spawnerSpoiler], pista.transform.position, Quaternion.identity);
        piecePrefab.GetComponent<Piece>().enabled = false;
        //Para cada bloque dentro de esa pieza
        foreach (SpriteRenderer child in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            //Cogemos el color actual de ese bloque
            Color currentColor = child.color;
            //Hacemos algo transparente a ese bloque
            currentColor.a = 0.3f; //La transparencia va entre 0 y 1
            child.color = currentColor;
        }  
    }
}