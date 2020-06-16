using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Timer
    int playTime =60;
    int seconds,minutes;


    void Start()
    {
        StartCoroutine("PlayTimer");
    }

    IEnumerator PlayTimer()
    {
        while(playTime > 0)
        {
            yield return new WaitForSeconds(1);
            playTime--;
            seconds = playTime % 60;
            minutes = playTime /60 % 60;
            UIManager.instance.UpdateTime(minutes,seconds);
        }

        Debug.Log("Tiempo Terminado");
        //WIN CONDITION
    }
 
}
