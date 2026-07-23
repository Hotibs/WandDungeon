using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiManger : MonoBehaviour
{
    public static PlayerUiManger instance;

    [SerializeField] Image hp;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI hpText;

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

    public void SetGold(int gold)
    {
        goldText.text = $"gold : {gold}";
    }

    public void SetHPBar(float nowHp,float maxHp)
    {
        hpText.text = $"HP : {nowHp}/{maxHp}";
        hp.fillAmount = nowHp / maxHp;
    }
}
