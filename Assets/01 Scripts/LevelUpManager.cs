using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager instance;

    [SerializeField] GameObject popup;
    [SerializeField] List<SpellData> spellDatas;
    [SerializeField] Image[] icons;
    [SerializeField] TextMeshProUGUI[] texts;
    List<SpellData> spellDataList;

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
        spellDataList = new List<SpellData>();
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

        for (int i = 0; i < spellDatas.Count; i++)
            index.Add(i);

        for (int i = 0; i < index.Count; i++)
        {
            int rand = Random.Range(i, index.Count);
            (index[rand], index[i]) = (index[i], index[rand]);
        }

        for (int i = 0; i < icons.Length; i++)
        {
            SpellData spell = spellDatas[index[i]];

            icons[i].sprite = spell.Spriteicon;
            texts[i].text = spell.SpellName;
            spellDataList.Add(spell);
        }
    }
    public void AddSpell(int num)
    {
        SlotManager.Instance.AddSpell(spellDataList[num]);
        spellDataList.Clear();
    }
}
