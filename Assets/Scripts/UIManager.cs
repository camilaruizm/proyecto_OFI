using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
     public TextMeshPro scoreText;
    public TextMeshPro timeText;

    void Awake()
    {
        instance = this;
        UpdateUI();
        UpdateTime(1,0);
    }



    public void UpdateTime(int mins, int secs)
    {
        timeText.text = mins.ToString("D2") + ":" + secs.ToString("D2");
    }

    public void UpdateUI()
    {
        scoreText.text = "Puntaje: "  +    MyScoreManager.ReadScore();
    } 
}
