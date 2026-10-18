using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public GameObject Coin;
    bool isLive;


    
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") ) 
            return;
            
     

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;


        spriter.flipX = target.position.x < rigid.position.x;

    }

      void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;


    }
    public void Init(SpawnData data)
    {
     anim.runtimeAnimatorController =  animCon[data.spriteType];

        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;


    }


    void OnTriggerEnter2D(Collider2D collision)
    {


        if (!collision.CompareTag("Bullet") && !collision.CompareTag("bomb") || !isLive)
            return;

        if (collision.CompareTag("bomb"))
        {
            if (collision.GetComponent<BOOMB>().Bomb == false)
            {
                return;
            }
          
            
        }


        if (collision.CompareTag("Bullet"))
        {
            health -= collision.GetComponent<Bullet>().damage;
            StartCoroutine(KnockBack(3));

            if (health > 0)
            {
                anim.SetTrigger("Hit");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                if (GameManager.instance.Reaper)
                    Reaper();
                GameManager.instance.Kill++;
                Drop(Random.Range(1,2));
                GameManager.instance.GetExp();
                if (GameManager.instance.isLive)
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }
       else
        {
            StartCoroutine(KnockBack(10));
            health -= maxHealth;

            if (health > 0)
            {
                anim.SetTrigger("Hit");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                Drop(Random.Range(1, 2));
                if (GameManager.instance.isLive)
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }

    }


    IEnumerator KnockBack(int power)
    {

        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * power,ForceMode2D.Impulse);
    
    }


    void Dead()
    {

        gameObject.SetActive(false);

    }

    void Drop(int Number)
    {


        if (Number < 1 || !GameManager.instance.isLive)
            return;


        for (int i = 0; i <= Number; i++)
        {
            GameObject CoinCL = Instantiate(Coin);
            Vector2 pos = gameObject.transform.position;
            CoinCL.transform.position = pos + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

    }

    void Reaper()
    {

        if (GameManager.instance.health + GameManager.instance.maxHealth * 0.025f > GameManager.instance.maxHealth)
            GameManager.instance.health = GameManager.instance.maxHealth;
       else
        {

            GameManager.instance.health += GameManager.instance.maxHealth * 0.025f;

        }



    }
}
