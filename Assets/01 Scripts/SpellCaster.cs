using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    SpellData[] datas;
    [SerializeField] Transform firePos;

    [SerializeField] Transform wand;

    SlotManager slotManager;

    int prevSlot;

    bool isDouble;

    GameObject GetPool(string name, int index)
    {
        GameObject obj = ProjectileObjectPoolManager.instance.GetObject(name);
        if (obj == null) return null;

        obj.transform.position = firePos.position;
        obj.transform.rotation = wand.rotation * Quaternion.Euler(0, 0, -90); ;

        AttackSpell attack = obj.GetComponent<AttackSpell>();
        if (datas[index] is AttackSpellData attackData)
        {
            attack.Init(attackData);
        }
        if (datas[index] is SpecialSpellData specialData)
        {
            obj.AddComponent<SpecialSpell>();
            SpecialSpell special = obj.GetComponent<SpecialSpell>();
            special.Init(specialData);
            if(specialData.SpecialType == SpecialType.Double)
            {
                isDouble = true;
            }
        }    
        return obj;
        
    }
    private void Start()
    {
           slotManager = SlotManager.Instance;
    }



    public void Cast(int slotSpell)
    {
        datas = slotManager.spellDatas;
        if (datas[slotSpell] == null) return;
        if (datas[slotSpell].SpellType == SpellType.Special)
        {
            GameObject obj = GetPool(datas[prevSlot].name, slotSpell);
            if (isDouble)
            {
                GameObject obj2 = GetPool(datas[prevSlot].name, slotSpell);
                obj2.transform.rotation *= Quaternion.Euler(0, 0, Random.Range(-10,10));
                isDouble = false;
            }
            return;
        }
        GetPool(datas[slotSpell].name,slotSpell);
        prevSlot = slotSpell;
    }

}
