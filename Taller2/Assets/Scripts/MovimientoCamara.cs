using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Transform personaje;
    public Vector3 offset;
    public float velocidadMovimiento;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, personaje.position + offset, velocidadMovimiento * Time.deltaTime); 
    }
}
