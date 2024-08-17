using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public int daño = 100;
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

    public GameObject[] cartas;
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
        gameObject.GetComponent<Disparar>().dañoBala = daño;
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

    public void BajarVida(float dañoRecibido)
    {
        dañoRecibido = dañoRecibido / defensa;
        vida = vida - (int)dañoRecibido;
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
            Time.timeScale = 0;
            nivel++;
            experienciaActual = 0;
            siguienteNivel = siguienteNivel * incrementoNivel;
            int randomAnterior = -1;
            int randomTrasAnterior = -1;
            for (int i = 0; i < 3; i++)
            {
                int random = randomAnterior;
                while (random == randomAnterior || random == randomTrasAnterior)
                {
                    random = Random.Range(0, 5); //Esto son las cartas que hay
                }
                if (i == 0)
                {
                    randomAnterior = random;
                }
                else
                {
                    randomTrasAnterior = randomAnterior;
                    randomAnterior = random;
                }
                cartas[i].SetActive(true);
                cartas[i].GetComponent<CargarCarta>().CargarDatos(random);
            }
        }
    }

    public void AumentarDinero(int ganancia)
    {
        dinero += ganancia;
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString();
    }

    public void ElegirEfecto(int numCarta)
    {
        string efecto = cartas[numCarta].GetComponent<CargarCarta>().efecto;
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++)
        {
            cartas[i].SetActive(false);
        }
        switch (efecto)
        {
            case "Area":
                AumentarArea();
                break;
            case "Vida":
                AumentarVida();
                break;
            case "Ataque":
                Aumentardaño();
                break;
            case "Velocidad":
                AumentarVelocidad();
                break;
            case "Regeneracion":
                AumentarRegeneracion();
                break;
            case "Defensa":
                AumentarDefensa();
                break;
        }
    }

    public void AumentarArea()
    {
        area = area * 2;
        gameObject.GetComponent<Disparar>().area = area;
        Debug.Log("Area");
    }
    public void AumentarVida()
    {
        int incrementoVida = (int)(vidaMax * 0.5f);
        vidaMax = vidaMax + incrementoVida;
        vida = vida + incrementoVida;
        Debug.Log("Vida");
    }

    private void Aumentardaño()
    {
        daño = (int)(daño * 1.5f);
        gameObject.GetComponent<Disparar>().dañoBala = daño;
        Debug.Log("Ataque");
    }
    public void AumentarVelocidad()
    {
        velocidadMovimiento = velocidadMovimiento * 1.3f;
        Debug.Log("Velocidad");
    }

    public void AumentarRegeneracion()
    {
        regeneracionVida = regeneracionVida + 150;
        Debug.Log("Regeneracion");
    }
    public void AumentarDefensa()
    {
        defensa = defensa * 2;
        Debug.Log("Defensa");
    }

    void Muerte()
    {
        deathScreenController.ShowDeathScreen();
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
