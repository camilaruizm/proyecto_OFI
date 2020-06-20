using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Timer
    int playTime = 60;
    int seconds,minutes;
    [HideInInspector] public bool waitTime;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        StartCoroutine(PlayTimer());
    }

    IEnumerator PlayTimer()
    {
        yield return new WaitForSeconds(3);
        waitTime = true;
        while (playTime > 0)
        {
            
           
            playTime -= 1;
            seconds = playTime % 60;
            minutes = playTime / 60 % 60;
            Debug.Log("Segundo " + playTime);
            UIManager.instance.UpdateTime(minutes,seconds);
            yield return new WaitForSeconds(1);
        }
       
        Debug.Log("Tiempo Terminado");
        //WIN CONDITION
    }


  
 
}
