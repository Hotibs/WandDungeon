using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    SlotType slotType;

    SpellInventory spellInventory;
    SpellSlot spellSlot;

    DragType dropType;
    private void Awake()
    {
        spellInventory = GetComponentInParent<SpellInventory>();
        spellSlot = GetComponentInParent<SpellSlot>();
        dropType = Drag.dropType;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (spellInventory != null)
        {
            dropType = DragType.Inventory;
        }
        else
        {
            dropType = DragType.Wand;
        }
        Debug.Log("드랍당함 ");
    }
}
