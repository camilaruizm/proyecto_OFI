using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject vaso;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Ball")
        {
            print("ya");
            Destroy(collider.gameObject); //Bola
            Destroy(vaso); //Vaso
            Destroy(gameObject); //Cubo
        }
        
    }
}
