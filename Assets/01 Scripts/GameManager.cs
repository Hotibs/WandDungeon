using TMPro;
using Unity.VisualScripting;
using UnityEngine;

enum GameState
{
    None,
    GameStart,
    GamePause,
    GameOver,
    GameClear
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gameSpeed;
    public TextMeshProUGUI speedText;
    GameState gameState;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        gameState = GameState.GameStart;
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        gameState = GameState.GamePause;
    }
    public void GameClear()
    {
        Time.timeScale = 0;
        gameState = GameState.GameClear;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameState = GameState.GameOver;
    }
    public void Speedbtn()
    {
        if (gameSpeed >= 8) gameSpeed = 0;
        gameSpeed++;
        Time.timeScale = gameSpeed;
        speedText.text = $"{gameSpeed}x spd";
    }
}
