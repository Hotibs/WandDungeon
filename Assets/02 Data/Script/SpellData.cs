using UnityEditor.Rendering.Universal;
using UnityEngine;

enum UpgradeType
{
    Damage,
    CastDelay,
    Mana,
    ManaRegen
}

enum SpecialType
{
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
}