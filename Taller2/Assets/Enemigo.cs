using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private GameObject jugador;
    public Rigidbody2D rigidbody;
    public GameObject[] rupiasPrefabs;
    public GameObject[] powerUpPrefabs;
    public float velocidadMovimiento = 5f;
    public int vida = 5;
    public bool jefe;
    private Vector2 direccion;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (jugador != null)
        {
            int ajuste = 0;
            direccion = (jugador.transform.position - transform.position).normalized;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            if (jefe)
            {
                ajuste = 90;
            }

            rigidbody.rotation = angulo + ajuste;

            // Move the enemy towards the player
            rigidbody.velocity = direccion * velocidadMovimiento;
        }
    }

    public void BajarVida()
    {
        vida -= 1;
        if (vida <= 0)
        {
            MatarEnemigo();
        }
    }

    void MatarEnemigo()
    {
        int numeroAleatorioEfecto = Random.Range(0, 100);

        if (numeroAleatorioEfecto > 40)
        {
            int numeroAleatorioRupia = Random.Range(0, 100);
            int numeroGenerar = 0;

            if (numeroAleatorioRupia < 65)
            {
                numeroGenerar = 0;
            }
            else if (numeroAleatorioRupia < 95)
            {
                numeroGenerar = 1;
            }
            else
            {
                numeroGenerar = 2;
            }

            GameObject rupia = Instantiate(rupiasPrefabs[numeroGenerar], transform.position, Quaternion.identity);
            Destroy(rupia, 5f);
        }
        else
        {
            GameObject powerUp = Instantiate(powerUpPrefabs[0], transform.position, Quaternion.identity);
            Destroy(powerUp, 5f);
        }

        Destroy(gameObject);
    }
}
