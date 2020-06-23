using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPong : MonoBehaviour
{
    public static UIPong instance;
    void Awake()
    {
        instance = this;
    }

    public TextMeshPro scoreText;

    public void Win()
    {
        scoreText.text = "GANASTE";
    }

    public void UpdateUI()
    {
        scoreText.text = "Puntaje: " + PongScore.ReadScore();
    }
}
