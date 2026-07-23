using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ButtonGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    
}
