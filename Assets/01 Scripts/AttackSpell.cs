using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSpell : MonoBehaviour
{
    public AttackSpellData data;

    SpecialSpell specialSpell;

    SpecialType type;
    
    float damage;
    public float speed;
    float lifeTime;
    GameObject projectile;

    float timer;

    Collider2D cd;

    public void Init(AttackSpellData spellData)
    {
        data = spellData;
    }

    private void Start()
    {
        damage = data.Damage;
        speed = data.Speed;
        lifeTime = data.LifeTime;
        
        specialSpell = GetComponent<SpecialSpell>();
        if (specialSpell != null)
        {
            type = specialSpell.Type;
        }
        else
        {
            type = SpecialType.None;
        }

        cd = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        speed = data.Speed;
        timer = 0f;
    }
    void Update()
    {
        if (timer > lifeTime) ReturnPool();
    }
    private void FixedUpdate()
    {
        
        Move();
    }

    private void Move()
    {
        timer += Time.deltaTime;
        transform.position += speed * Time.deltaTime * transform.right;
    }

    public void Damage(float dmg)
    {
        damage = dmg;
    }

    void ReturnPool()
    {
        if (specialSpell!=null)
            specialSpell.ReturnPool();
        else
            ProjectileObjectPoolManager.instance.ReturnObject(data.SpellName, this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Monster")) 
        {
            ReturnPool();
            collision.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
        }

    }
}
