using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class AttackSpell : MonoBehaviour
{
    public AttackSpellData data;

    SpecialSpell specialSpell;

    SpecialType type;
    
    public float damage;
    public float speed;
    float lifeTime;
    GameObject projectile;

    float timer;

    Collider2D cd;

    bool isHit = false;

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
        timer += Time.deltaTime;
        if (specialSpell != null && specialSpell.Type == SpecialType.Homing)
        {
            if (specialSpell.target == null)
            {
                Move();
            }
            transform.position = Vector3.MoveTowards(transform.position, specialSpell.target.position, speed * Time.deltaTime);
            Vector2 dir = specialSpell.target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,angle);

        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
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
        if(specialSpell != null)
        {
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Monster")) 
        {
            if (isHit) return;
            isHit = true;
            collision.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
            ReturnPool();
        }
    }
    
}
