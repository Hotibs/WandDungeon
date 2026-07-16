using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSpell : MonoBehaviour
{
    public AttackSpellData data;

    
    float damage;
    float speed;
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

        cd = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        timer = 0f;
    }
    void Update()
    {
        if (timer > lifeTime) ReturnPool();
    }
    private void FixedUpdate()
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
        Debug.Log(data.SpellName);
        ObjectPoolManager.instance.ReturnObject(data.SpellName, this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
    }
}
