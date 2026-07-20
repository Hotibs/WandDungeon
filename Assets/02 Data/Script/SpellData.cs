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
    Explosion,
    Double
}

public enum SpellType
{
    None,
    Attack,
    Special
}

[CreateAssetMenu]
public class SpellData : ScriptableObject
{
    [SerializeField] SpellType type;
    [SerializeField] string spellName;
    [SerializeField] Sprite icon;
    [SerializeField] int tier;
    [SerializeField] float dropPer;

    public SpellType SpellType => type;
    public string SpellName => spellName;
    public Sprite Spriteicon => icon;
    public int Tier => tier;
    public float DropPer => dropPer;

}