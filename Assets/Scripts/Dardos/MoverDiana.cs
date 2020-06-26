using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverDiana : MonoBehaviour {
    [SerializeField]
    Transform[] paradas;
    int paradaActual = 0;
    Rigidbody rb;
    [SerializeField]
    float velocidadMovimiento = 5;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        movimiento();
	}

    void movimiento()
    {
        if (Vector3.Distance(transform.position, paradas[paradaActual].position)<.25f)
        {
            paradaActual += 1;
            paradaActual = paradaActual % paradas.Length;
        }
        Vector3 direccion = (paradas[paradaActual].position - transform.position).normalized;
        rb.MovePosition(transform.position + direccion * velocidadMovimiento * Time.deltaTime);
    }
}
