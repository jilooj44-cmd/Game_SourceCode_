using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour 

{
    
   public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

     void OnEnable()
    {
        speed *= Character.Speed;

        anime.runtimeAnimatorController = animCon[GameManager.instance.playerId];
     
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");



    }



    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; 
        rigid.MovePosition(rigid.position + nextVec);

    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        anime.SetFloat("Speed",inputVec.magnitude);


        if (inputVec.x != 0) {

            spriter.flipX = inputVec.x < 0;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive) return;

        GameManager.instance.health -= Time.deltaTime * 10;


        if (GameManager.instance.health <= GameManager.instance.maxHealth * 0.05f)
        {
            GameManager.instance.health += GameManager.instance.maxHealth * 0.2f;

        }

        if (GameManager.instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anime.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}

