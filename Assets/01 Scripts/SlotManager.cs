using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance;

    public SpellData[] spellDatas; //완드슬롯
    public List<SpellData> invenData;

    SpellInventory inventory;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void SetInventory(SpellInventory inventory)
    {
        this.inventory = inventory;
    }
    public void GetInven(List<SpellData> data)
    {
        invenData = data;
    }
    public void GetSlot(SpellData[] sd)
    {
        spellDatas = sd;
    }
    public void AddSpell(SpellData data)
    {
        inventory.AddSpell(data);
    }
    public void DestroySpell(SpellData data)
    {
        inventory.DestorySpell(data);
    }

}
