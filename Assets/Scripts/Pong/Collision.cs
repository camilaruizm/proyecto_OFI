using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject vaso;
    public int score1 = 1;
    public int score2 = 1;
    public string jugador1 = "1";
    public string jugador2 = "2";

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
        if(collider.gameObject.CompareTag("Ball"))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            if(gameObject.transform.parent.gameObject.tag == "VasoA")
            {
                PongScore.AddPongScore(score1, jugador1);
                Debug.Log("Punto jugador 1");
            }
            else if(gameObject.transform.parent.gameObject.tag == "VasoB")
            {
                PongScore.AddPongScore(score2, jugador2);
                Debug.Log("Punto jugador 2");
            }

           Destroy(collider.gameObject); //Bola
           // Destroy(vaso); //Vaso
           // Destroy(gameObject); //Cubo
           // PongScore.AddPongScore(score);
        }
    }
}
