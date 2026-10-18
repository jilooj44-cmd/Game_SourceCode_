using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rigid;
    public static Coin instence;
    public float GetCoin = 1f;

      bool Touch = true;
      Transform tf;

     void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        instence = this;
    }


    
    
     void OnTriggerEnter2D(Collider2D collision)
    {

      

        if (!collision.CompareTag("Collection"))
            return;

        

        Touch = false;
      
        
        if (!Touch)
        {

           tf = collision.transform.parent.GetComponent<Transform>();
            Touch = true;
        }
       
       
    }

     void Update()
    {

        if (!tf)
            return;


        Vector2 der = (tf.position - transform.position).normalized;

        rigid.AddForce(der * 15 * Time.timeScale);
        if ((tf.position - transform.position).magnitude < 1f)
        {
            
            Money.instance.Bank(Mathf.RoundToInt(Random.Range(50,60) * GetCoin));
            Destroy(gameObject);
        }
    }
}
