using UnityEngine;

[CreateAssetMenu(fileName = "WandData", menuName = "Wand/WandData")]
public class WandData : ScriptableObject
{
    [SerializeField] string wandName;

    [SerializeField] int slotCount;

    [SerializeField] float castDelay;

    [SerializeField] float rechargeTime;

    [SerializeField] int maxMana;

    [SerializeField] float manaRegen;

    [SerializeField] int castCount;
}
