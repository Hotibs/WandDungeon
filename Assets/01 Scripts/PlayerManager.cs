using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    int gold;
    int level;
    int exp;
    int maxExp;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gold = 0;
        level = 1;
        exp = 0;
        maxExp = 20;
    }

    public void LevelUp()
    {
        level++;
        exp -= maxExp;
        LevelUpManager.instance.OpenPopup();
        GameManager.instance.GamePause();
    }
    public void GetExp(int exp)
    {
        this.exp += exp;
        if(this.exp >= maxExp)
        {
            LevelUp();
        }
    }
    public void GetGold(int gold)
    {
        this.gold += gold;
    }

    public void UseGold(int gold)
    {
        this.gold -= gold;
    }

}
