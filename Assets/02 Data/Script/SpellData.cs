using UnityEngine;

public enum UpgradeType
{
    None,
    Damage,
    CastDelay,
    Mana,
    ManaRegen
}

public enum SpecialType
{
    None,
    Bounce,
    Piercing,
    Homing,
    Explosion
}

[CreateAssetMenu]
public class SpellData : ScriptableObject
{
    [SerializeField] string spellName;
    [SerializeField] Sprite icon;
    [SerializeField] int tier;
    [SerializeField] float dropPer;


    public string SpellName => spellName;
    public Sprite Spriteicon => icon;
    public int Tier => tier;
    public float DropPer => dropPer;

}