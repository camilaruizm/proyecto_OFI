using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Timer
    int playTime = 62;
    int seconds,minutes;
    [HideInInspector]public bool countDownDone;

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
        while(playTime > 0)
        {
            yield return new WaitForSeconds(2);
            playTime--;
            seconds = playTime % 62;
            minutes = playTime / 62  % 62;
            UIManager.instance.UpdateTime(minutes,seconds);
        }

        Debug.Log("Tiempo Terminado");
        //WIN CONDITION
    }
 
}
