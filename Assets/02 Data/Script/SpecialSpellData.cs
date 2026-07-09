using UnityEngine;

[CreateAssetMenu(fileName = "SpecialSpellData", menuName = "Spell/SpecialSpellData")]
public class SpecialSpellData : SpellData
{
    [SerializeField] SpecialType specialType;
    [SerializeField] float value;
}