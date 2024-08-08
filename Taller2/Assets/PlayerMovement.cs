using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public int da�o = 100;
    public float defensa = 1;
    public int regeneracionVida = 0;
    public float velocidadMovimiento = 1f;
    public int vida = 1000;
    private int vidaMax;
    public int area = 1;
    public int nivel = 1;
    public int siguienteNivel = 100;
    public int incrementoNivel = 2;
    private int experienciaActual = 0;

    public GameObject carta;
    public GameObject cantidadDinero;
    public int dinero = 0;
    private float tiempoEfectoPowerUp = 0;
    private float tiempoTranscurrido = 0;
    private float tiempoTranscurridoRegeneracion = 0;
    private bool powerUpActivo = false;
    public Camera camara;
    public Rigidbody2D rigidbody;
    private Animator animator;
    private DeathScreenController deathScreenController;


    Vector2 movimiento;
    Vector2 posicionMouse;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString();
        deathScreenController = FindObjectOfType<DeathScreenController>();

        gameObject.GetComponent<Disparar>().area = area;
        gameObject.GetComponent<Disparar>().da�oBala = da�o;
        vidaMax = vida;
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
        if (regeneracionVida > 0)
        {
            if (vida < vidaMax)
            {
                tiempoTranscurridoRegeneracion += Time.deltaTime;
                if (tiempoTranscurridoRegeneracion >= 1)
                {
                    vida += regeneracionVida;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movimiento.normalized * velocidadMovimiento * 5 * Time.fixedDeltaTime);

        Vector2 direccion = posicionMouse - rigidbody.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) *Mathf.Rad2Deg;
        rigidbody.rotation = angulo;
    }

    public void BajarVida(float da�oRecibido)
    {
        da�oRecibido = da�oRecibido / defensa;
        vida = vida - (int)da�oRecibido;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void AumentarExperiencia(int experienciaRecibida)
    {
        experienciaActual += experienciaRecibida;
        if (experienciaActual > siguienteNivel)
        {
            nivel++;
            experienciaActual = 0;
            siguienteNivel = siguienteNivel * incrementoNivel;
            carta.SetActive(true);
        }
    }


    void Muerte()
    {
        deathScreenController.ShowDeathScreen();
    }

    public void AumentarDinero(int ganancia)
    {
        dinero += ganancia;
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString();
    }

    public void AumentarDa�o()
    {
        da�o = (int)(da�o * 1.2f);
        gameObject.GetComponent<Disparar>().da�oBala = da�o;
        carta.SetActive(false);
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
