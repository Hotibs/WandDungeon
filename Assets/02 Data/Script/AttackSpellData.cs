using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpellData", menuName = "Spell/AttackSpellData")]
public class AttackSpellData : SpellData
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject projectile;
}