using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSpellData", menuName = "Spell/UpgradeSpellData")]
public class UpgradeSpellData : SpellData
{
    [SerializeField] UpgradeType upgradeType;
    [SerializeField] float value;

    public UpgradeType UpgradeType => upgradeType;
    public float Value => value;
}