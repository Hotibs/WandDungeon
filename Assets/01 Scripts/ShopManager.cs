using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [SerializeField] GameObject popup;
    [SerializeField] List<WandData> wandDatas;
    [SerializeField] Image[] icons;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] TextMeshProUGUI[] statTexts;
    List<WandInstance> wandDataList;

    WandManager wandManager;

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
        wandDataList = new List<WandInstance>();
        wandManager = WandManager.instance;
        popup.SetActive(false);
    }
    public void OpenPopup()
    {
        SetPopup();
        popup.SetActive(true);
    }
    public void ClosePopup()
    {
        GameManager.instance.GameStart();
        popup.SetActive(false);
    }
    void SetPopup()
    {
        List<int> index = new List<int>();

        for (int i = 0; i < wandDatas.Count; i++)
            index.Add(i);

        for (int i = 0; i < index.Count; i++)
        {
            int rand = Random.Range(i, index.Count);
            (index[rand], index[i]) = (index[i], index[rand]);
        }

        for (int i = 0; i < icons.Length; i++)
        {
            WandData wand = wandDatas[index[i]];
            WandInstance wandInstance = wandManager.WandCreate(wand);
            icons[i].sprite = wand.Icon;
            icons[i].preserveAspect = true;
            texts[i].text = wand.WandName;
            statTexts[i].text = $"Slot : {wandInstance.slot}\n" +
                $"Castdelay : {wandInstance.castDelay}\n" +
                $"Rechargetime : {wandInstance.rechargeTime}\n" +
                $"Mana : {wandInstance.maxMana}\n" +
                $"Manaregen : {wandInstance.manaRegen}\n" +
                $"Price : {wandInstance.price}\n";
            wandDataList.Add(wandInstance);
        }
    } 
    public void ChangeWand(int num)
    {
        wandManager.SetWand(wandDataList[num]);
        wandDataList.Clear();
        FindAnyObjectByType<SpellSlot>(FindObjectsInactive.Include).SlotWand();
    }
}
