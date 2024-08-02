using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarEnemigo : MonoBehaviour
{
    public Transform[] puntos;
    public GameObject enemigo;
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
        Instantiate(enemigo, puntoAleatorio.position, Quaternion.identity);
    }
}
