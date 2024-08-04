using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarEnemigo : MonoBehaviour
{
    public Transform[] puntos;
    public GameObject[] enemigos;
    public float tiempoEntreEnemigos;
    private float tiempoTranscurrido;

    private void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        if (tiempoTranscurrido > tiempoEntreEnemigos)
        {
            tiempoTranscurrido = 0;
            CrearEnemigo();
        }

    }

    void CrearEnemigo()
    {
        Transform puntoAleatorio = puntos[Random.Range(0, puntos.Length)];
        int random = Random.Range(0, 100);

        if (random < 70)
        {
            Instantiate(enemigos[0], puntoAleatorio.position, Quaternion.identity);
        }
        else if (random < 95)
        {
            Instantiate(enemigos[1], puntoAleatorio.position, Quaternion.identity);
        }
        else
        {
            Instantiate(enemigos[2], puntoAleatorio.position, Quaternion.identity);
        }
    }
}
