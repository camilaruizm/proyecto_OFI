using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static  void AddScore(int cantidad)
    {
        score+= cantidad;
        Debug.Log("Score: " + score);
        UIManager.instance.UpdateUI();
    }
   
   public static int ReadScore()
   {
       return score;
   }
}
