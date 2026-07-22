
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;

    [SerializeField] SpriteRenderer rewardChest;
    [SerializeField] Image icon;
    [SerializeField] GameObject rewardPopUp;
    [SerializeField] List<SpellData> spellDatas; //전체 스펠

    [SerializeField] Sprite openChest;

    bool isOpen=false;

    int randIndex;

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
    }

    private void Start()
    {
        rewardPopUp.SetActive(false);
    }

    public void GetReward()
    {
        if (isOpen) return;

        OpenPopup();
        GetIcon();
        isOpen = true;
        rewardChest.sprite = openChest;
        GameManager.instance.GamePause();
    }
    void GetIcon()
    {
        randIndex = Random.Range(0, spellDatas.Count);
        icon.sprite = spellDatas[randIndex].Spriteicon;
    }
    public void GetButton()
    {
        SlotManager.Instance.AddSpell(spellDatas[randIndex]);
        ClosePopup();
    }
    public void DropButton() 
    {

        ClosePopup();
    }
    void OpenPopup()
    {
        rewardPopUp.SetActive(true);
    }
    void ClosePopup()
    {
        GameManager.instance.GameStart();
        rewardPopUp.SetActive(false);
    }
}
