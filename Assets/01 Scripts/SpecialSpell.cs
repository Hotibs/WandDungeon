using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpecialSpell : MonoBehaviour
{
    public SpecialSpellData data;

    Collider2D cd;
    CircleCollider2D ccd;
    BoxCollider2D bcd;
    SpecialType type;

    AttackSpell attackSpell;

    float speed;
    float dmg;
    bool isBounce = false;
    bool isExplosion = false;

    public Transform target;

    public void Init(SpecialSpellData spellData)
    {
        data = spellData;
        type = data.SpecialType;
    }

    private void Awake()
    {
        cd = GetComponent<Collider2D>();
        ccd = GetComponent<CircleCollider2D>();
        bcd = GetComponent<BoxCollider2D>();
        attackSpell = GetComponent<AttackSpell>();
        
    }
    private void Start()
    {
        switch (type)
        {
            case SpecialType.None:
                break;
            case SpecialType.Bounce:
                Bounce();
                break;
            case SpecialType.Piercing:
                Piercing();
                break;
            case SpecialType.Explosion:
                Explosion();
                break;
            case SpecialType.Homing:
                Homing();
                break;
            default:
                break;
        }
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
        DetectEnemy();
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
        Collider2D[] hits = null ;
        speed = 0f;
        if (ccd != null)
        {
            while (true)
            {
                transform.localScale *= 1.05f;
                ccd.radius += 0.05f;
                if (ccd.radius >= 2f) break;
                yield return null;
            }
            hits = Physics2D.OverlapCircleAll(transform.position, ccd.radius, 1 << LayerMask.NameToLayer("Monster"));
        }
        else
        {
            while (true)
            {
                transform.localScale *= 1.05f;
                bcd.size *= new Vector2(1.05f,1.05f);
                if (bcd.size.x >= 2f) break;
                yield return null;
            }
            hits = Physics2D.OverlapBoxAll(transform.position, bcd.size, transform.eulerAngles.z, 1 << LayerMask.NameToLayer("Monster"));
        }
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<MonsterController>().TakeDamage(data.Value);
        }

        ReturnPool();
    }
    
    public void ReturnPool()
    {
        Destroy(GetComponent<SpecialSpell>());
        if (type == SpecialType.Bounce) return;
        attackSpell.ReturnPool();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExplosion)
        {
            StartCoroutine(ScaleUp());
            return;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            ReturnPool();
        }
        if (collision.gameObject.CompareTag("Monster")){
            MonsterController monster = collision.gameObject.GetComponent<MonsterController>();
            collision.gameObject.GetComponent<MonsterController>().TakeDamage(attackSpell.damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && !isBounce)
        {
            ReturnPool();
        }
        if (isBounce)
        {
            Vector2 reflect = Vector2.Reflect(transform.right,collision.contacts[0].normal);
            transform.right = reflect;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            collision.gameObject.GetComponent<MonsterController>().TakeDamage(attackSpell.damage);
            ReturnPool();
        }
    }

}
