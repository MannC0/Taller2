using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerarEnemigo : MonoBehaviour
{
    public GameObject textoTiempo;
    public Transform[] puntos;
    public GameObject[] enemigos;
    public float tiempoEntreEnemigos;
    public float tiempoDeHorda;
    public float tiempoEntreHordas;
    public int numeroDeHordas;
    public int hordaActual = 0;
    private float tiempoTranscurridoTemporal;
    private float tiempoTranscurridoTotal;
    private bool terminaHorda;
    private bool bossSpawned = false; // Flag to check if boss has been spawned
    private float probabilidadEnemigoFacil = 90;

    private void Update()
    {
        int minutos = (int)(tiempoTranscurridoTotal / 60);
        int segundos = (int)(tiempoTranscurridoTotal % 60);
        textoTiempo.GetComponent<TMP_Text>().text = minutos.ToString("D2") + ": " + segundos.ToString("D2");

        if (numeroDeHordas == hordaActual)
        {
            if (!bossSpawned) // Check if boss is not spawned yet
            {
                CrearJefe();
                bossSpawned = true; // Set the flag to true when the boss is spawned
            }
        }
        else
        {
            tiempoTranscurridoTemporal += Time.deltaTime;
            tiempoTranscurridoTotal += Time.deltaTime;

            if (tiempoTranscurridoTotal < tiempoDeHorda)
            {
                if (tiempoTranscurridoTemporal > tiempoEntreEnemigos)
                {
                    tiempoTranscurridoTemporal = 0;
                    CrearEnemigo();
                }
            }
            else
            {
                if (terminaHorda)
                {
                    terminaHorda = false;
                    tiempoTranscurridoTemporal = 0;
                }

                if (tiempoTranscurridoTemporal > tiempoEntreHordas)
                {
                    tiempoTranscurridoTotal = 0;
                    tiempoTranscurridoTemporal = 0;
                    tiempoDeHorda *= 2;
                    tiempoEntreEnemigos /= 2;
                    terminaHorda = true;
                    hordaActual++;
                    probabilidadEnemigoFacil /= 2;
                }
            }
        }

        // Debug info to see what's happening in the Update loop
        Debug.Log($"Horde: {hordaActual}, Tiempo Total: {tiempoTranscurridoTotal}, Tiempo Temporal: {tiempoTranscurridoTemporal}, Termina Horde: {terminaHorda}, Boss Spawned: {bossSpawned}");
    }

    void CrearJefe()
    {
        Transform puntoAleatorio = puntos[Random.Range(0, puntos.Length)];
        Instantiate(enemigos[2], puntoAleatorio.position, Quaternion.identity);
    }

    void CrearEnemigo()
    {
        Transform puntoAleatorio = puntos[Random.Range(0, puntos.Length)];
        int random = Random.Range(0, 100);
        if (random < probabilidadEnemigoFacil)
        {
            Instantiate(enemigos[0], puntoAleatorio.position, Quaternion.identity);
        }
        else if (random < 95)
        {
            Instantiate(enemigos[1], puntoAleatorio.position, Quaternion.identity);
        }
    }
}
