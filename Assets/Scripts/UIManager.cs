using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    void Awake()
    {
        instance = this;
        UpdateUI();
        UpdateTime(1,0);
    }

    public TextMeshPro scoreText;
    public TextMeshPro timeText;

    public void UpdateTime(int mins, int secs)
    {
        timeText.text = mins.ToString("D2") + ":" + secs.ToString("D2");
    }

    public void UpdateUI()
    {
        scoreText.text = "Puntaje: "  +    ScoreManager.ReadScore();
    } 
}
