using Photon.Pun;
using UnityEngine;
using TMPro;

public class UITurnManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI gameStatusText = null;

    [SerializeField] private TextMeshProUGUI youPlayer = null;
    [SerializeField] private TextMeshProUGUI rondasText = null;
    [SerializeField] private TextMeshProUGUI turnoText = null;
    [SerializeField] private TextMeshProUGUI winningText = null;

    [SerializeField] private TextMeshProUGUI[] scoreText = null;
    [SerializeField] private TextMeshProUGUI[] rondasGanadasText = null;

    [SerializeField] private GameObject scoreInterface = null;
    [SerializeField] private GameObject salirButton = null;

    #region SINGLETON PATTERN
    public static UITurnManager _instance;
    public static UITurnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UITurnManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("UiTurn");
                    _instance = container.AddComponent<UITurnManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    public void GameStarts()
    {
        if (salirButton)
        {
            salirButton.SetActive(false);
        }
    }
    //Enciende la interfaz de puntajes
    public void TurnScoreInterface(bool active)
    {
        if (scoreInterface)
        {
            Debug.Log("Se enciende la interfaz");
            scoreInterface.SetActive(active);
        }
    }

    public void TurnRoundWinnerInterface(bool active, int winner)
    {
        if (winningText)
        {
            winningText.gameObject.SetActive(active);
            winningText.text = "Ganador de ronda: Jugador " + winner;
        }
    }

    public void TurnWinnerInterface(bool active, int winner)
    {
        if (winningText)
        {
            winningText.gameObject.SetActive(active);
            winningText.text = "Ganador definitivo: Jugador " + winner;
        }
    }

    public void GameStatus(string status)
    {
        if (gameStatusText)
        {
            gameStatusText.text = status;
        }
    }
    public void YourPlayer(int yourPlayer)
    {
        if (youPlayer)
        {
            youPlayer.text = "Tu eres el jugador: " + yourPlayer;
            youPlayer.gameObject.SetActive(true);
        }
    }

    //Actualiza la ronda actual
    public void RondaActual(int ronda)
    {
        if (rondasText)
        {
            rondasText.text = "Ronda: " + ronda;
        }
    }

    //Actualiza el puntaje de un jugador en específico
    public void ChangeScore(int player, int score)
    {
        if (player < scoreText.Length)
        {
            scoreText[player].text = "Puntaje Jugador " + player + ": "  + score;
        }
    }

    //Actualiza qué jugador está jugando
    public void TurnoPlayer(int turno)
    {
        if (turnoText)
        {
            turnoText.text = "Jugando: Jugador " + (turno);
        }
    }
    //Actualiza la ronda para el jugador
    public void RondasGanadasPlayer(int player, int rondasText)
    {
        if (player < scoreText.Length)
        {
            rondasGanadasText[player].text = "Rondas ganadas: " + rondasText;
        }
    }
    //Actualiza el ganador de la ronda
    public void Winner(int winner)
    {
        if (winningText)
        {
            winningText.text = "Ganador de ronda: Jugador " + winner;
        }
    }

    public void GameEnds()
    {
        if (salirButton)
        {
            TurnScoreInterface(false);
            salirButton.SetActive(true);
        }
    }

}
