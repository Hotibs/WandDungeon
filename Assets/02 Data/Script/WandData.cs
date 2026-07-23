using UnityEngine;

[CreateAssetMenu(fileName = "WandData", menuName = "Wand/WandData")]
public class WandData : ScriptableObject
{
    [SerializeField] string wandName;

    [SerializeField] int slotCount;

    [SerializeField] float castDelay;

    [SerializeField] float rechargeTime;

    [SerializeField] float maxMana;

    [SerializeField] float manaRegen;

    [SerializeField] Sprite icon;

    [SerializeField] int price;


    public string WandName => wandName;
    public int SlotCount => slotCount;
    public float CastDelay => castDelay;
    public float RechargeTime => rechargeTime;
    public float MaxMana => maxMana;
    public float ManaRegen => manaRegen;
    public Sprite Icon => icon;
    public int Price => price;
}
