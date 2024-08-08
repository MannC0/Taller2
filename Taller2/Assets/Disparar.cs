using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Transform posicionGenerar;
    public GameObject bala;
    public float velocidadBala = 10f;
    public float ratioDeDisparo = 0.5f;
    public int dañoBala;
    public float area;

    private float tiempoTranscurrido = 1;

    private void Update()
    {
        tiempoTranscurrido = tiempoTranscurrido + Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (tiempoTranscurrido > ratioDeDisparo)
            {
                Disparo();
                tiempoTranscurrido = 0;
            }
        }
    }

    void Disparo()
    {
        GameObject bullet = Instantiate(bala, posicionGenerar.position, posicionGenerar.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(posicionGenerar.right * velocidadBala, ForceMode2D.Impulse);
        Destroy(bullet, area);
        bullet.GetComponent<Bala>().dañoBala = dañoBala;
    }
}
