using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private GameObject jugador;
    public Rigidbody2D rigidbody;
    public float velocidadMovimiento;
    public int vida = 5;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.transform.position, velocidadMovimiento * Time.deltaTime);
    }

    public void BajarVida()
    {
        vida = vida - 1;
        if (vida == 0)
        {
            Destroy(gameObject);
        }

    }
}
