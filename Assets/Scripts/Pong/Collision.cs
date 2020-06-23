using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject vaso;
    public int score = 1;

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
            gameObject.SetActive(false);
            vaso.SetActive(false);
            Destroy(collider.gameObject); //Bola
            //Destroy(vaso); //Vaso
            //Destroy(gameObject); //Cubo
            PongScore.AddPongScore(score);
        }

        if(PongScore.ReadScore() == 2)
        {
            gameObject.SetActive(true);
            vaso.SetActive(true);
        }
    }
}
