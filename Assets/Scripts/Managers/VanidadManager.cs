using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanidadManager : MonoBehaviour
{
    public static VanidadManager instance;
    [Range(0, 1f)] public float diferenciaAltura;

    [Header("Barras elegidas")]
    public BarraMovimiento[] barrasElegidas = new BarraMovimiento[5];

    //privadas
    List<BarraMovimiento> barrasScript = new List<BarraMovimiento>();
    int barraColisionadaIndex;




    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.instance.puntero.punteroColisiono.AddListener(CollisionEnter);
        //GameManager.instance.puntero.punteroMantieneColision.AddListener(CollisionStay);


        foreach (GameObject script in GameManager.instance.barras)
        {
            barrasScript.Add(script.GetComponent<BarraMovimiento>());
        }
    }


    void CollisionEnter(Collider2D colliderBarra)
    {
        ElegirBarras(colliderBarra);
        foreach (BarraMovimiento barra in barrasScript)
        {
            //Si una de las barras es una de las elegidas
            foreach (BarraMovimiento barraElegida in barrasElegidas)
            {
                if (barra == barraElegida)
                {
                    return;
                }
            }


        }

        /*
        barraColisionadaIndex = GameManager.instance.barras.FindIndex(a => a == colliderBarra.gameObject);

        //Barra Principal
        barrasScript[barraColisionadaIndex].estado = BarraMovimiento.Estado.manual;


        for (int i = 0; i < barrasScript.Count - 1; i++)
        {
            if (i < 0) return;
            if (i >= barrasScript.Count) return;

            //Barras laterales
            if (barraColisionadaIndex >= i-2 && barraColisionadaIndex <= i + 2)
            {
                barrasScript[i].estado = BarraMovimiento.Estado.manual;
                barrasScript[i].heightTarget = GameManager.instance.punteroPosition.y / 2;
                return;
            }

            //Todas las barras que no sean las laterales
            if (barrasScript[i].estado == BarraMovimiento.Estado.manual)
            {
                Tareas.Nueva(2, () =>
                {
                    if (barraColisionadaIndex != i - 1 || barraColisionadaIndex != i - 2 || barraColisionadaIndex != i + 1 || barraColisionadaIndex != i + 2)
                    {
                        barrasScript[i].estado = BarraMovimiento.Estado.baja;
                        barrasScript[i].gameObject.GetComponent<BarraInteraccion>().GetValoresEstadoA();
                    }
                });
            }

        }
        */
    }

    private void CollisionStay(Collider2D colliderBarra)
    {
        //Barra Principal
        barrasScript[barraColisionadaIndex].heightTarget = GameManager.instance.punteroPosition.y * 1.5f;

        for (int i = 0; i < barrasScript.Count - 1; i++)
        {
            if (i < 0) return;
            if (i >= barrasScript.Count) return;

            //Barras laterales
            if (barraColisionadaIndex == i - 1 || barraColisionadaIndex == i - 2 || barraColisionadaIndex == i + 1 || barraColisionadaIndex == i + 2)
            {
                barrasScript[i].heightTarget = GameManager.instance.punteroPosition.y / 2;
                return;
            }
        }
    }

    void ElegirBarras(Collider2D colliderBarra)
    {
        barraColisionadaIndex = GameManager.instance.barras.FindIndex(a => a == colliderBarra.gameObject);
        if (barraColisionadaIndex >= 2 && barraColisionadaIndex <= barrasScript.Count - 3)
        {
            barrasElegidas[0] = barrasScript[barraColisionadaIndex - 2];
            barrasElegidas[1] = barrasScript[barraColisionadaIndex - 1];
            barrasElegidas[2] = barrasScript[barraColisionadaIndex];
            barrasElegidas[3] = barrasScript[barraColisionadaIndex + 1];
            barrasElegidas[4] = barrasScript[barraColisionadaIndex + 2];
            return;
        }
        else if (barraColisionadaIndex == 0)
        {
            barrasElegidas[0] = barrasScript[barraColisionadaIndex];
            barrasElegidas[1] = barrasScript[barraColisionadaIndex];
            barrasElegidas[2] = barrasScript[barraColisionadaIndex];
            barrasElegidas[3] = barrasScript[barraColisionadaIndex + 1];
            barrasElegidas[4] = barrasScript[barraColisionadaIndex + 2];
            return;
        }
        else if (barraColisionadaIndex == 1)
        {
            barrasElegidas[0] = barrasScript[barraColisionadaIndex - 1];
            barrasElegidas[1] = barrasScript[barraColisionadaIndex - 1];
            barrasElegidas[2] = barrasScript[barraColisionadaIndex];
            barrasElegidas[3] = barrasScript[barraColisionadaIndex + 1];
            barrasElegidas[4] = barrasScript[barraColisionadaIndex + 2];
            return;
        }
        else if (barraColisionadaIndex == barrasScript.Count - 1)
        {
            barrasElegidas[0] = barrasScript[barraColisionadaIndex - 2];
            barrasElegidas[1] = barrasScript[barraColisionadaIndex - 1];
            barrasElegidas[2] = barrasScript[barraColisionadaIndex];
            barrasElegidas[3] = barrasScript[barraColisionadaIndex];
            barrasElegidas[4] = barrasScript[barraColisionadaIndex];
            return;
        }
        else if (barraColisionadaIndex == barrasScript.Count - 2)
        {
            barrasElegidas[0] = barrasScript[barraColisionadaIndex - 2];
            barrasElegidas[1] = barrasScript[barraColisionadaIndex - 1];
            barrasElegidas[2] = barrasScript[barraColisionadaIndex];
            barrasElegidas[3] = barrasScript[barraColisionadaIndex + 1];
            barrasElegidas[4] = barrasScript[barraColisionadaIndex];
            return;
        }

    }

    void RegresarEstadoA(Collider2D colliderBarra)
    {
        int index = GameManager.instance.barras.FindIndex(a => a == colliderBarra.gameObject);
        GameObject barraColisionExit = GameManager.instance.barras[index];

        for (int i = index - 2; i < index + 2; i++)
        {
            if (i < 0) return;
            if (i >= GameManager.instance.barras.Count) return;


            Tareas.Nueva(2, () =>
            {
                barrasScript[i].estado = BarraMovimiento.Estado.baja;
                barrasScript[i].gameObject.GetComponent<BarraInteraccion>().GetValoresEstadoA();
            });
        }


    }
}
