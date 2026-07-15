using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    Wand
}
public enum DragType
{
    Inventory,
    Wand
}
public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    SlotType slotType;

    SpellInventory spellInventory;
    SpellSlot spellSlot;

    public static SpellData spellData;

    public static Vector2 saveInvenPos;

    public static int StartPos;

    public static DragType dragType;
    public static DragType dropType;

    [SerializeField] Image dragIcon;

    private void Awake()
    {
        spellInventory = GetComponentInParent<SpellInventory>();
        spellSlot = GetComponentInParent<SpellSlot>();
        transform.SetAsLastSibling();
        dragIcon.enabled = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
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
            StartPos = transform.parent.GetSiblingIndex();
            dragIcon.sprite = spellData.Spriteicon;
            dragIcon.enabled = true;
        }
        Debug.Log("드래그 시작");
        Debug.Log(spellData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragIcon.transform.position = eventData.position;
        Debug.Log("드래그 중");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragIcon.enabled = false;
        if (dragType == DragType.Inventory)
        {
            if (dropType == DragType.Inventory)
            {
                transform.position = saveInvenPos;
                Debug.Log("인벤 to 인벤 ");
            }
            else
            {
                spellSlot.SetSpell(transform.parent.GetSiblingIndex(), spellData);
                //스펠 장착
                Debug.Log("인벤 to 완드 ");
            }
        }
        else
        {
            if (dropType == DragType.Inventory)
            {
                spellSlot.UnequipSpell(transform.parent.GetSiblingIndex());
                spellInventory.AddSpell(spellData);
                //스펠 해제
                Debug.Log("완드 to 인벤 ");
            }
            else
            {
                spellSlot.ChangeSpell(StartPos, transform.parent.GetSiblingIndex(), spellData);
                // 순서변경
                Debug.Log("완드 to 완드 ");
            }
        }
        Debug.Log("드래그 끝");
    }


}
