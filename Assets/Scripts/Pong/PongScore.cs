using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongScore : MonoBehaviour
{
    public static int score;
    
    public static void AddPongScore(int cantidad)
    {
        score += cantidad;
        Debug.Log("Score: " + score);

        if(score == 3)
        {
            UIPong.instance.Win();
        }
        else
        {
            UIPong.instance.UpdateUI();
        }
    }
    
    public static int ReadScore()
    {
        return score;
    }
}
