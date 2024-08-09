using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanarDinero : MonoBehaviour
{
    public int valor = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().AumentarDinero(valor);
            Destroy(gameObject);
        }
    }

}
