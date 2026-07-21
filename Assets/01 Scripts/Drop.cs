using UnityEngine;
using UnityEngine.EventSystems;


public class Drop : MonoBehaviour, IDropHandler
{
    
    SpellInventory spellInventory;
    SpellSlot spellSlot;

    int slotIndex;

    public static SpellData spell;

    private void Awake()
    {
        spellInventory = GetComponentInParent<SpellInventory>();
        spellSlot = GetComponentInParent<SpellSlot>();
        slotIndex = transform.parent.GetSiblingIndex();
    }

    
    public void OnDrop(PointerEventData eventData)
    {
        
        
        
        
        if (spellInventory != null)
        {
            Drag.dropType = DragType.Inventory;
        }
        else
        {
            Drag.dropType = DragType.Wand;
        }
        if(Drag.dropType == DragType.Wand)spell = spellSlot.spells[slotIndex];
        
        if (Drag.dragType == DragType.Inventory)
        {
            if (Drag.dropType == DragType.Wand)
            {
                spellSlot.SetSpell(slotIndex, Drag.spellData);
                
            }
        }
        else if(Drag.dragType == DragType.Wand)
        {
            if(Drag.dropType == DragType.Inventory)
            {
                spellInventory.AddSpell(Drag.spellData);
            }
            else
            {
                Drag.endPos = slotIndex;
            }
        }
        
    }
}
