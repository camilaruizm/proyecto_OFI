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
    }

    public TextMeshPro scoreText;

    public void UpdateUI()
    {
        scoreText.text = "Puntaje: "  +    ScoreManager.ReadScore();
    } 
}
