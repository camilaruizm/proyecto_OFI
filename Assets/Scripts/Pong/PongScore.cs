using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongScore : MonoBehaviour
{
    public static int score1;
    public static int score2;

    public static void AddPongScore(int cantidad, string jugador)
    {
        if(jugador == "1")
        {
            score1 += cantidad;
            Debug.Log("Score J1: " + score1);

            if (score1 == 7)
            {
                UIPong.instance.Win();
            }
            else
            {
                UIPong.instance.UpdateUI();
            }
        } else if(jugador == "2")
        {
            score2 += cantidad;
            Debug.Log("Score J2: " + score2);

            if (score2 == 7)
            {
                UIPong.instance.Win();
            }
            else
            {
                UIPong.instance.UpdateUI();
            }
        }
        

        
    }
    
    public static int ReadScore1()
    {
        return score1;
    }

    public static int ReadScore2()
    {
        return score1;
    }
}
