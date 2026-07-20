using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    SpellData[] datas;
    [SerializeField] Transform firePos;

    [SerializeField] Transform wand;
    void GetPool(string name,int index)
    {
        GameObject obj = ProjectileObjectPoolManager.instance.GetObject(name);
        if (obj == null) return;
        obj.transform.position = firePos.position;
        obj.transform.rotation = wand.rotation * Quaternion.Euler(0, 0, -90); ;
        AttackSpell attack = obj.GetComponent<AttackSpell>();
        if (datas[index] is AttackSpellData attackdata)
        attack.Init(attackdata);
    }

    

    public void Cast(int slotSpell)
    {
        datas = SlotManager.Instance.spellDatas;
        if (datas[slotSpell] == null) return;
        GetPool(datas[slotSpell].name,slotSpell);
    }

    

}
