using UnityEngine;
using UnityEngine.UI;


public class SpellSlot : MonoBehaviour
{
    [SerializeField] WandData wandData;
    public SpellData[] spells;

    Image[] icons;
    private void Start()
    {
        spells = new SpellData[wandData.SlotCount];
        UpdateSlotIcon();
    }
    private void OnEnable()
    {
        icons = new Image[wandData.SlotCount];
        
        
    }
    void UpdateSlotEnable()
    {
        for (int i = wandData.SlotCount; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);
            slot.gameObject.SetActive(false);
        }
    }
    void UpdateSlotIcon()
    {
        UpdateSlotEnable();
        for (int i = 0; i < wandData.SlotCount; i++)
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
