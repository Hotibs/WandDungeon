using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class SpellInventory : MonoBehaviour
{
    [SerializeField] List<SpellData> spellDatas;

    Image[] icons;


    private void Start()
    {
        icons = new Image[transform.childCount];
        UpdateInventory();
    }
    void UpdateInventory()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i).GetChild(1);

            icons[i] = slot.GetComponent<Image>();
            if (i >= spellDatas.Count)
            {
                icons[i].color = Color.white;
                continue;
            }
            icons[i].sprite = (spellDatas[i].Spriteicon);
        }
    }
    public void EquipSpell(SpellData spell)
    {
        spellDatas.Remove(spell);
        //SpellSlot에게 데이터 줘야됨
    }

    public void AddSpell(SpellData spell)
    {
        spellDatas.Add(spell);
    }
    void DestorySpell(SpellData spell) 
    {
        spellDatas.Remove(spell);
    }



}
