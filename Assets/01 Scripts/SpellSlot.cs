using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class SpellSlot : MonoBehaviour
{
    [SerializeField] WandData wandData;
    SpellData[] spells;

    Image[] icons;

    private void Start()
    {
        icons = new Image[wandData.SlotCount];
        spells = new SpellData[wandData.SlotCount];
        UpdateSlotIcon();
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
            }
            else
            {
                icons[i].sprite = spells[i].Spriteicon;
            }
        }
    }

    public void SetSpell(int slotCnt,SpellData spell)
    {
        spells[slotCnt] = spell;
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
