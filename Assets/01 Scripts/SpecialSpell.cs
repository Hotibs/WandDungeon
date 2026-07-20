using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpecialSpell : MonoBehaviour
{
    public SpecialSpellData data;

    Collider2D cd;
    CircleCollider2D ccd;
    SpecialType type;

    AttackSpell attackSpell;

    float speed;
    float dmg;
    bool isBounce = false;
    bool isExplosion = false;

    Transform target;

    public void Init(SpecialSpellData spellData)
    {
        data = spellData;
    }

    private void Awake()
    {
        cd = GetComponent<Collider2D>();
        ccd = GetComponent<CircleCollider2D>();
        attackSpell = GetComponent<AttackSpell>();
        
    }
    private void Start()
    {
        type = data.SpecialType;
    }

    public SpecialType Type => type;

    void Bounce()
    {
        isBounce = true;
    }
    void Piercing()
    {
        cd.isTrigger = true;

    }
    void Explosion()
    {
        isExplosion = true;
        cd.isTrigger = true;
        
    }
    void Homing()
    {
        
    }
    
    void PrevCast()
    {

    }

    bool DetectEnemy()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 10, 1 << LayerMask.NameToLayer("Monster"));

        if (collider.Length == 0)
        {
            return false;
        }
        else
        {

            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;

            return true;
        }
    }
    void Trace()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    IEnumerator ScaleUp()
    {
        speed = 0f;
        while (true)
        {
            transform.localScale *= 1.05f;
            ccd.radius += 0.05f;
            if (ccd.radius >= 2f) break;
            yield return null;
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,ccd.radius,1<<LayerMask.NameToLayer("Monster"));

        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<MonsterController>().TakeDamage(data.Value);
        }

        ReturnPool();
    }


    public void ReturnPool()
    {
        ProjectileObjectPoolManager.instance.ReturnObject(data.SpellName, this.gameObject);
        Destroy(gameObject.GetComponent<SpecialSpell>());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExplosion)
        {
            StartCoroutine(ScaleUp());
        }
        if (isBounce)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 180);
            isBounce = false;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall")&&!isBounce)
        {
            ReturnPool();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster")){
            collision.gameObject.GetComponent<MonsterController>().TakeDamage(dmg);
        }
    }


}
