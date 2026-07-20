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
}
