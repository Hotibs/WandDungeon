using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


public enum DragType
{
    None,
    Inventory,
    Wand
}
public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,IPointerUpHandler
{
    SpellInventory spellInventory;
    public static SpellSlot spellSlot;

    public static SpellData spellData;

    public static Vector2 saveInvenPos;

    public static int endPos;

    public static DragType dragType;
    public static DragType dropType;

    public static List<Drag> allDrags = new List<Drag>();
    
    bool isBeginDrag = true;

    [SerializeField] Image dragIcon;

    Image img;


    private void Awake()
    {
        spellInventory = GetComponentInParent<SpellInventory>();
        spellSlot = GetComponentInParent<SpellSlot>();
        allDrags.Add(this);    
        img = GetComponent<Image>();

        transform.SetAsLastSibling();
        dragIcon.enabled = false;
    }
    private void OnEnable()
    {
        SetRaycastTarget();
    }
    private void OnDestroy()
    {
        allDrags.Remove(this);
    }
    void SetRaycastTarget()
    {
        foreach (Drag drag in allDrags)
        {
            drag.img.raycastTarget = isBeginDrag;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        isBeginDrag = false;
        SetRaycastTarget();
        dropType = DragType.None;
        if (spellInventory != null)
        {
            saveInvenPos = transform.position;
            spellData = spellInventory.spellDatas[transform.parent.GetSiblingIndex()];
            dragType = DragType.Inventory;
            dragIcon.sprite = spellData.Spriteicon;
            dragIcon.enabled = true;
        }
        else
        {
            spellData = spellSlot.spells[transform.parent.GetSiblingIndex()];
            dragType = DragType.Wand;
            
            dragIcon.sprite = spellData.Spriteicon;
            dragIcon.enabled = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragIcon.enabled = false;
        if (dragType == DragType.Inventory)
        {
            if (dropType == DragType.Inventory)
            {
                transform.position = saveInvenPos;
            }
            else if (dropType == DragType.Wand)
            {
                spellInventory.DestorySpell(spellData);
                if(Drop.spell != null)
                {
                    spellInventory.AddSpell(Drop.spell);
                }
            }
        }
        else
        {
            if (dropType == DragType.Inventory)
            {
                spellSlot.UnequipSpell(transform.parent.GetSiblingIndex());
                
            }
            else if(dropType == DragType.Wand) 
            {
                spellSlot.ChangeSpell(endPos,transform.parent.GetSiblingIndex());
            }
        }

        Reset();
    }
    void Reset()
    {
        isBeginDrag = true;
        SetRaycastTarget();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }


}
