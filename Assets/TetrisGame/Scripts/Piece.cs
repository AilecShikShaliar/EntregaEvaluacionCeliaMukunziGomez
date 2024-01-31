using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //�ltima ca�da(bajada) de la pieza hace 0 segundos
    float fillCounter = 1f, counterToDown;

    // Start is called before the first frame update
    void Start()
    {
        //Rellenamos el contador de tiempo para que baje autom�tico
        counterToDown = fillCounter;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de tiempo est� lleno
        if (counterToDown > 0)
            //Le quitamos uno cada segundo
            counterToDown -= 1 * Time.deltaTime;
       
        //Movimiento horizontal de las piezas
        //Movimiento de la ficha a la izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Llamamos al m�todo de movimiento horizontal y le pasamos la direcci�n izquierda
            MovePieceHorizontally(-1);
        }
        //Movimiento de la ficha a la derecha
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Llamamos al m�todo de movimiento horizontal y le pasamos la direcci�n derecha
            MovePieceHorizontally(1);
        }
        //Rotaci�n de la pieza
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Roto la pieza hacia la derecha
            this.transform.Rotate(0, 0, -90);

            //Si la posici�n es v�lida
            if (IsValidPiecePosition())
            {
                //Actualizamos la rejilla, guardando la nueva posici�n en el GridHelper
                UpdateGrid();
            }
            //Si la posici�n no fuese v�lida
            else
            {
                //Revierto la rotaci�n hacia el lado contrario(izquierdo)
                this.transform.Rotate(0, 0, 90);
            }
        }
        //Mover la pieza hacia abajo al pulsar la tecla o cuando haya pasado m�s de un segundo desde la �ltima vez que se movi�
        else if (Input.GetKeyDown(KeyCode.DownArrow) || counterToDown <= 0)
        {
            //Muevo la pieza hacia abajo una posici�n
            this.transform.position += new Vector3(0, -1, 0);

            // Si la posici�n es v�lida
            if (IsValidPiecePosition())
            {
                //Actualizamos la rejilla, guardando la nueva posici�n en el GridHelper
                UpdateGrid();
            }
            //Si la posici�n no fuese v�lida
            else
            {
                //Revierto el movimiento hacia abajo sumando uno hacia arriba
                this.transform.position += new Vector3(0, 1, 0);
                //Si ya no pudiese bajar m�s, habr�a que detectar si es momento de borrar una fila
                GridHelper.DeleteAllFullRows();
                //Hacemos que aparezca una pieza nueva, llamando al PieceSpawner a su m�todo
                FindObjectOfType<PieceSpawner>().SpawnNextPiece();//Busca un objeto de ese tipo para poder usar sus m�todos y variables
                //Deshabilitamos este script para que esta pieza no vuelva a moverse
                this.enabled = false;
            }
            //Reiniciamos el contador de tiempo
            counterToDown = fillCounter;
        }
    }

    //M�todo para el movimiento horizontal
    void MovePieceHorizontally(int direction) //con direction, le pasamos un n�mero para saber si el movimiento es a izquierda o a derecha
    {
        //Muevo la pieza en la direcci�n dada
        this.transform.position += new Vector3(direction, 0, 0);
        //Comprobamos si la nueva posici�n es v�lida
        if (IsValidPiecePosition())
        {
            //Actualizamos la rejilla, guardando la nueva posici�n en el GridHelper
            UpdateGrid();
        }
        //Si la posici�n no es v�lida
        else
        {
            //Revertimos el movimiento a la posici�n en la que estaba antes
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }

    //M�todo que comprueba si la posici�n en la que se encuentra ahora mismo la pieza, es o no v�lida
    private bool IsValidPiecePosition()
    {
        //Hacemos una pasada por todas las posiciones de los hijos de la pieza(los bloques)
        foreach (Transform block in this.transform)
        {
            //Recuperamos su posici�n (la de los bloques, hijos de la pieza) y la redondeamos para que no tenga decimales
            Vector2 pos = GridHelper.RoundVector(block.position);

            //Si no est� dentro de los bordes, la posici�n no es v�lida. Es decir, alguno de los bloques de la pieza se sale de los bordes o est� encima de ellos
            if (!GridHelper.IsInsideBorders(pos))
            {
                //Si alg�n bloque de la pieza no est� en una posici�n v�lida
                return false;
            }

            //Si ya hay otro bloque en esa misma posici�n, la posici�n tampoco es v�lida. 
            //Como la posici�n podr�a ser un float(tener decimales), la transformamos en n�mero entero
            Transform possibleObject = GridHelper.grid[(int)pos.x, (int)pos.y];
            //Si ya hay otro objeto y este no es hijo del mismo objeto (osea el bloque que hay es de otra pieza)
            if (possibleObject != null && possibleObject.parent != this.transform)
            {
                //La posici�n no ser� valida
                return false;
            }
        }
        //Si ninguna de las cosas anteriormente mencionadas se cumple, ser� que este bloque o pieza est� en una posici�n v�lida
        return true;
    }

    //M�todo que actualiza la rejilla virtual tras mover las piezas o bloques a su nueva posici�n
    //Lo haremos primero haciendo un borrado de bloques, poniendo primero todo a null, y luego poniendo las posiciones nuevas de esos bloques
    private void UpdateGrid()
    {
        //Comparamos si el padre del objeto coincide con el del bloque estamos mirando
        for (int y = 0; y < GridHelper.h; y++)
        {
            //Despu�s por columnas de cada fila
            for (int x = 0; x < GridHelper.w; x++)
            {
                //Comprobamos si en esa posici�n no hay un bloque
                if (GridHelper.grid[x, y] != null)
                {
                    //Comprobamos si el padre del bloque es la pieza donde est� este script metido
                    if (GridHelper.grid[x, y].parent == this.transform)
                    {
                        //Se carga los bloques que quedan de esa pieza y pone esas posiciones a null
                        GridHelper.grid[x, y] = null;
                    }
                }
            }
            //Insertamos los bloques en las posiciones que deben estar
            //Hacemos una pasada por cada uno de los bloques de la pieza actual
            foreach (Transform block in this.transform)
            {
                //Cojo la posici�n donde est� cada uno de los hijos y la redondeo
                Vector2 pos = GridHelper.RoundVector(block.position);
                //Metemos esa posici�n en la posici�n de la rejilla virtual que le toque
                GridHelper.grid[(int)pos.x, (int)pos.y] = block;
            }
        }
        AudioManagerTetris.amInstance.PlaySFX(1);
    }
}
