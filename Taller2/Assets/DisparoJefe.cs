using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJefe : MonoBehaviour
{
    public Transform posicionGenerar;
    public GameObject bala;
    private GameObject jugador;

    public float velocidadBala = 10f;
    public float tiempoEntreDisparos = 1f;
    public int cantidadRafaga = 5;
    public float tiempoRafaga = 0.2f;
    private bool atacando = false;

    [Range(0, 359)] public float anguloCono;
    public int cantidadBalasCono;
    public float distanciaGenerar = 0.1f;


    private void Start()
    {
        jugador = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (atacando == false)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        StartCoroutine(Disparo());
    }

    private IEnumerator Disparo()
    {
        atacando = true;
        float anguloInicial, anguloActual, anguloIncremental;
        ConoInfluencia(out anguloInicial, out anguloActual, out anguloIncremental);

        for (int i = 0; i < cantidadRafaga; i++)
        {
            for (int j = 0; j < cantidadBalasCono; j++)
            {
                Vector2 posicion = EncontrarPosicionGenerarBala(anguloActual);

                GameObject bullet = Instantiate(bala, posicion, posicionGenerar.rotation);
                Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
                bullet.transform.right = bullet.transform.position - transform.position;
                rigidbody.AddForce(bullet.transform.right * velocidadBala, ForceMode2D.Impulse);
                Destroy(bullet, 5f);

                anguloActual += anguloIncremental;
            }

            anguloActual = anguloInicial;
            yield return new WaitForSeconds(tiempoRafaga);
            ConoInfluencia(out anguloInicial, out anguloActual, out anguloIncremental);
        }

        yield return new WaitForSeconds(tiempoEntreDisparos);
        atacando = false;
    }

    private void ConoInfluencia(out float anguloInicial, out float anguloActual, out float anguloIncremental)
    {
        Vector2 direccionDisparo = jugador.transform.position - transform.position;

        float anguloObjetivo = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;
        anguloInicial = anguloObjetivo;
        anguloActual = anguloObjetivo;
        float mitadAnguloCono = 0f;
        anguloIncremental = 0f;
        if (anguloCono != 0)
        {
            anguloIncremental = anguloCono / (cantidadBalasCono - 1);
            mitadAnguloCono = anguloCono / 2;
            anguloInicial = anguloObjetivo - mitadAnguloCono;
            anguloActual = anguloInicial;
        }
    }

    private Vector2 EncontrarPosicionGenerarBala(float anguloActual)
    {
        float x = transform.position.x + distanciaGenerar * Mathf.Cos(anguloActual * Mathf.Deg2Rad);
        float y = transform.position.y + distanciaGenerar * Mathf.Sin(anguloActual * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
