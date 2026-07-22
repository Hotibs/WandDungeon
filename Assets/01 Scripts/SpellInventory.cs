using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellInventory : MonoBehaviour
{
    [SerializeField] public List<SpellData> spellDatas;

    Image[] icons;
    SlotManager slotManager;
    private void Awake()
    {
        slotManager = SlotManager.Instance;
        slotManager.SetInventory(this);
    }
    private void Start()
    {
        
        UpdateInventory();
    }
    private void OnEnable()
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
            icons[i].color = Color.white;
            icons[i].enabled = true;
            if (i >= spellDatas.Count)
            {
                icons[i].color = Color.white;
                icons[i].enabled = false;
                continue;
            }
            icons[i].sprite = (spellDatas[i].Spriteicon);
        }
        if (slotManager == null) return;
        slotManager.GetInven(spellDatas);
    }
    
    public void AddSpell(SpellData spell)
    {
        spellDatas.Add(spell);
        UpdateInventory();
    }
    public void DestorySpell(SpellData spell) 
    {
        spellDatas.Remove(spell);
        UpdateInventory();
    }



}
