using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITurnManager : Singleton<UITurnManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected UITurnManager() { }

    [SerializeField] private TextMeshProUGUI gameStatusText = null;

    [SerializeField] private TextMeshProUGUI youPlayer = null;
    [SerializeField] private TextMeshProUGUI rondasText = null;
    [SerializeField] private TextMeshProUGUI turnoText = null;
    [SerializeField] private TextMeshProUGUI winningText = null;

    [SerializeField] private TextMeshProUGUI[] scoreText = null;
    [SerializeField] private TextMeshProUGUI[] rondasGanadasText = null;

    [SerializeField] private GameObject scoreInterface = null;
    [SerializeField] private GameObject salirButton = null;

    public void GameStarts()
    {
        salirButton.SetActive(false);
    }
    //Enciende la interfaz de puntajes
    public void TurnScoreInterface(bool active)
    {
        scoreInterface.SetActive(active);
    }

    public void TurnRoundWinnerInterface(bool active, int winner)
    {
        winningText.gameObject.SetActive(active);
        winningText.text = "Ganador de ronda: Jugador " + winner;
    }

    public void TurnWinnerInterface(bool active, int winner)
    {
        winningText.gameObject.SetActive(active);
        winningText.text = "Ganador definitivo: Jugador " + winner;
    }

    //Actualiza el estado de juego
    public void GameStatus(string status)
    {
        gameStatusText.text = status;
    }
    //Actualiza el texto de cuál es tu jugador
    public void YourPlayer(int yourPlayer)
    {
        youPlayer.text = "Tu eres el jugador: " + yourPlayer;
        youPlayer.gameObject.SetActive(true);
    }

    //Actualiza la ronda actual
    public void RondaActual(int ronda)
    {
        rondasText.text = "Ronda: " + ronda;
    }

    //Actualiza el puntaje de un jugador en específico
    public void ChangeScore(int player, int score)
    {
        scoreText[player].text = "Puntaje ronda " + score;
    }

    //Actualiza qué jugador está jugando
    public void TurnoPlayer(int turno)
    {
        turnoText.text = "Jugando: Jugador " + (turno);
    }
    //Actualiza la ronda para el jugador
    public void RondasGanadasPlayer(int player, int rondasText)
    {
        rondasGanadasText[player].text = "Rondas ganadas: " + rondasText;
    }
    //Actualiza el ganador de la ronda
    public void Winner(int winner)
    {
        winningText.text = "Ganador de ronda: Jugador " + winner;
    }

    public void GameEnds()
    {
        TurnScoreInterface(false);
        salirButton.SetActive(true);
    }

}
