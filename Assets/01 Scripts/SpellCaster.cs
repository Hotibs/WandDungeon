using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    SpellData[] datas;
    SlotManager slotManager;
    private void Awake()
    {
        slotManager = SlotManager.Instance;
    }

    void GetPool(string name,int index)
    {
        GameObject obj = ObjectPoolManager.instance.GetObject(name);
        AttackSpell attack = obj.GetComponent<AttackSpell>();
        if (datas[index] is AttackSpellData attackdata)
        attack.Init(attackdata);
    }

    

    public void Cast(int slotSpell)
    {
        datas = slotManager.spellDatas;
        if (datas[slotSpell] == null) return;
        GetPool(datas[slotSpell].name,slotSpell);
    }

    

}
