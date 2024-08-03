using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cantidadDinero;
    public int dinero = 0;
    public int vida = 15;
    public float velocidadMovimiento = 5f;
    private float tiempoEfectoPowerUp = 0;
    private float tiempoTranscurrido = 0;
    private bool powerUpActivo = false;
    public Camera camara;

    public Rigidbody2D rigidbody;
    private Animator animator;

    Vector2 movimiento;
    Vector2 posicionMouse;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString();
    }

    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movimiento.x);
        animator.SetFloat("Vertical", movimiento .y);
        animator.SetFloat("Velocidad", movimiento.sqrMagnitude);

        posicionMouse = camara.ScreenToWorldPoint(Input.mousePosition);

        if (powerUpActivo == true)
        {
            tiempoTranscurrido += Time.deltaTime;
            if (tiempoTranscurrido > tiempoEfectoPowerUp)
            {
                powerUpActivo = false;
                tiempoTranscurrido = 0;
                tiempoEfectoPowerUp = 0;
                gameObject.GetComponent<Disparar>().ratioDeDisparo = gameObject.GetComponent<Disparar>().ratioDeDisparo * 2;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movimiento.normalized * velocidadMovimiento * Time.fixedDeltaTime);

        Vector2 direccion = posicionMouse - rigidbody.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) *Mathf.Rad2Deg;
        rigidbody.rotation = angulo;
    }

    public void BajarVida()
    {
        vida = vida - 1;
        if (vida == 0)
        {
            Destroy(gameObject);
        }
    }

    public void AumentarDinero(int ganancia)
    {
        dinero += ganancia;
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString();
    }

    public void PowerUp(int numPowerUp)
    {
        if (numPowerUp == 1)
        {
            if (powerUpActivo == false)
            {
                powerUpActivo = true;
                tiempoEfectoPowerUp = 10;
                gameObject.GetComponent<Disparar>().ratioDeDisparo = gameObject.GetComponent<Disparar>().ratioDeDisparo / 2;
            }
            else 
            {
                tiempoTranscurrido = 0;
            }
        }
    }
}
