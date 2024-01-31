using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour
{
    /* Matriz filas y columnas
      |  0  |  1  |  2  |  3  |
   -----------------------------
    0 | null   x     x    null
    1 |  x     x    null  null
    2 | null  null  null  null
    3 | null  null  null  null
     
     */

    //Ancho y alto de la rejilla
    //Son est�ticas para poder instanciarlas sin tener que consultar el objeto
    public static int w = 10, h = 18 + 4;
    //Creamos el array doble rejilla, de altura y anchura dada
    public static Transform[,] grid = new Transform[w, h]; //La [,] indica dos dimensiones

    //M�todo que dado un Vector2 coger� ese Vector, y redondear� sus coordenadas de X e Y. Tras esto el m�todo nos devuelve el vector redondeado
    public static Vector2 RoundVector(Vector2 v)
    {
        //Devuelve un nuevo Vector2 ya redondeado en X e Y
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y)); //Mathf.Round -> redondea al n�mero entero m�s pr�ximo
    }

    //M�todo que dada una posici�n comprobamos si esta pieza est� dentro de los bordes del juego. Nos devolver� si es cierto o no
    public static bool IsInsideBorders(Vector2 pos)
    {
        //Si ambas coordenadas son positivas y no se pasan por la derecha
        if (pos.x >= 0 && pos.y >= 0 && pos.x < w)
        {
            //La pieza est� dentro de la zona de juego
            return true;
        }
        //Si lo de arriba no se cumple
        else
        {
            //La pieza est� fuera de la zona de juego
            return false;
        }
        
    }

    //M�todo al que le pasamos una fila y si hemos comprobado que est� completa, la elimina
    public static void DeleteRow(int y)
    {
        //Para poder borrar la fila, vemos cada una de las columnas de la fila actual
        for (int x = 0; x < w; x++)
        {
            //Destruyo el cuadrado que hay en esa posici�n, el objeto que vemos en la pantalla
            Destroy(grid[x, y].gameObject);
            //Despu�s de destruirlo, el espacio que hab�a reservado en la rejilla virtual, lo vac�o.
            //Cambiar�amos las X del dibujo de arriba por una posici�n vac�a (null)
            grid[x, y] = null;
            AudioManagerTetris.amInstance.PlaySFX(0);
        }
    }

    //M�todo que baja una fila a partir de una fila concreta
    public static void DecreaseRow(int y)
    {
        //Para poder bajar la fila, vemos cada una de las columnas de la fila actual
        for (int x = 0; x < w; x++)
        {
            //Si la posici�n que quiero bajar no est� vac�a
            if (grid[x, y] != null)
            {
                //Muevo la ficha -1 en la Y, a la posici�n en la que me encontraba
                grid[x, y - 1] = grid[x, y];
                //Como hemos bajado el bloque en la posici�n anterior, hacemos null la posici�n que ahora ha quedado vac�a
                grid[x, y] = null;

                //Ahora repintamos en pantalla
                //Repintamos en pantalla el bloque una posici�n m�s abajo en la pantalla por cada bloque
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
            AudioManagerTetris.amInstance.PlaySFX(0);
        } 
    }

    //M�todo que baja las filas de arriba a partir de una dada
    public static void DecreaseAbove(int y)
    {
        //Para todas las filas desde la dada
        for (int i = y; i < h; i++)
        {
            //Llamamos al m�todo que baja una fila, pero en este caso ir�n bajando de una en una, hasta que no queden m�s
            DecreaseRow(i);
            AudioManagerTetris.amInstance.PlaySFX(0);
        }
    }

    //M�todo para saber si una fila est� completa, pas�ndole una fila
    public static bool IsRowFull(int y)
    {
        //Pasamos primero por todas las columnas de esa fila
        for (int x = 0; x < w; x++)
        {
            //Si encuentro alg�n hueco en esa fila, es que no est� llena
            if (grid[x, y] == null)
            {
                //Hay un hueco en la fila, no est� completa
                return false;
            }
            AudioManagerTetris.amInstance.PlaySFX(0);
        }
        //La fila si no se cumple lo de arriba, ser� que est� llena
        return true;
    }

    //M�todo para borrar varias o todas las filas de golpe
    public static void DeleteAllFullRows()
    {
        //Comprobamos para todas las filas, desde la de m�s abajo, hasta la de m�s arriba
        for (int y = 0; y < h; y++)
        {
            //Si la fila que estamos comprobando est� llena
            if (IsRowFull(y))
            {
                //Borramos la fila actual
                DeleteRow(y);
                //Al borrar la fila actual, bajamos las que est�n por encima
                DecreaseAbove(y + 1);
                //Volver�amos a la fila anterior, es decir, si ya hemos borrado una fila todas bajar�n
                //Pero no pasaremos a la siguiente, primero volvemos a comprobar la fila en la que estamos
                y--;
            }
            AudioManagerTetris.amInstance.PlaySFX(0);
        }

        //Hacemos un borrado de piezas que se hayan quedado vac�as
        CleanPieces();
    }

    //M�todo para limpiar piezas cuando ya no tienen bloques
    private static void CleanPieces()
    {
        //Hacemos una pasada por todos los objetos de tipo pieza que encontramos
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            //Si esa pieza ya no tiene bloques
            if (piece.transform.childCount == 0)
            {
                //Destruimos el objeto pieza
                Destroy(piece);
            }
            AudioManagerTetris.amInstance.PlaySFX(0);
        }
    }
}
