using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    [SerializeField] float moveSpeed;

    Transform target;

    float range;

    SpriteRenderer sr;

    private void Start()
    {
        moveSpeed = 2f;
        range = 2f;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckDist();
    }
    private void OnEnable()
    {
        maxHP = 100;
        nowHP = maxHP;

        target = GameObject.Find("Player").transform;
    }

    void CheckDist()
    {
        if(target == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                return;
            }
            target = player.transform;
        }
        float dist = Vector3.Distance(transform.position, target.position);

        if(dist < range)
        {
            //공격
        }
        else
        {
            //이동
            Trace();
        }
    }
    void Trace()
    {
        sr.flipX = CheckFlip();
        Vector3 dir = GetDir().normalized;

        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,target.position,moveSpeed*Time.deltaTime);
    }

    Vector3 GetDir()
    {
        return target.position - transform.position;
    }
    bool CheckFlip()
    {

        return transform.position.x > target.position.x;
    }

    public void TakeDamage(float dmg)
    {
        nowHP -= dmg;
        if(nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }

    void Die()
    {
        ObjectPoolManager.instance.ReturnObject("Monster",this.gameObject);
    }
    

}
