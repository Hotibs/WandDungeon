using UnityEngine;
using UnityEngine.UI;


public class SpellSlot : MonoBehaviour
{
    [SerializeField] WandData wandData;
    public SpellData[] spells;

    int slot;
    WandInstance wand;

    SlotManager slotManager;

    Image[] icons;

    private void Start()
    {
        slotManager = SlotManager.Instance;
        spells = new SpellData[10];
        icons = new Image[10];
        slot = wandData.SlotCount;
        UpdateSlotIcon();
    }
    public void SlotWand()
    {
        wand = WandManager.instance.GetWand();
        for (int i = 0; i < slot; i++)
        {
            if (spells[i] != null) slotManager.AddSpell(spells[i]);
            UnequipSpell(i);
        }
        slot = wand.slot;
        UpdateSlotIcon();
    }
    void UpdateSlotEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);
            if(i < this.slot)
            {
                slot.gameObject.SetActive(true);
                continue;
            }
            slot.gameObject.SetActive(false);
        }
    }
    void UpdateSlotIcon()
    {
        UpdateSlotEnable();
        for (int i = 0; i < slot; i++)
        {
            Transform slot = transform.GetChild(i).GetChild(1);
            icons[i] = slot.GetComponent<Image>();
            if (spells[i]==null)
            {
                icons[i].sprite = null;
                icons[i].color = Color.white; 
                icons[i].enabled = false;
            }
            else
            {
                icons[i].enabled = true;
                icons[i].sprite = spells[i].Spriteicon;
            }
        }
        slotManager.GetSlot(spells);
    }

    public void SetSpell(int slotCnt,SpellData spell)
    {
        spells[slotCnt] = spell;
        UpdateSlotIcon();
    }
    public void ChangeSpell(int targetSlot,int startSlot)
    {
        SpellData sd;
        if (spells[targetSlot] == null)
        {
            spells[targetSlot] = spells[startSlot];
            spells[startSlot] = null;
            UpdateSlotIcon();
            return;
        }
        sd = spells[targetSlot];
        spells[targetSlot] = spells[startSlot];
        spells[startSlot] = sd;
        UpdateSlotIcon();
    }

    public void ClearSpell()
    {
        for (int i = 0; i < spells.Length; i++) 
        {
            UnequipSpell(i);
        }
        UpdateSlotIcon();
    }
    public void UnequipSpell(int slotCnt)
    {
        spells[slotCnt] = null;
        UpdateSlotIcon();
    }
}
