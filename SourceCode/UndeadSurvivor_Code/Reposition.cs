using UnityEngine;

public class Reposition : MonoBehaviour
{
    Renderer rend;
    Collider2D coll;
    public GameObject Bomb;
    public int NM;
    public GameObject BombManager;
    
    void Awake()
    {
        coll = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        int C1 = 0; 
        int C2 = 0;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffx = Mathf.Abs( playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirx = playerDir.x < 0 ? -1 : 1;
        float diry = playerDir.y < 0 ? -1 : 1;

       

        switch (transform.tag)
        {
            case "Ground":
                if (diffx > diffy)
                {
                    if (BombManager)
                    {
                        foreach (Transform Bomb in BombManager.transform)
                        {


                            if (Bomb.gameObject.GetComponent<BOOMB>().parentNM == NM && Bomb.gameObject.GetComponent<BOOMB>().Lock)
                            {
                                Destroy(Bomb.gameObject);
                            }

                        }
                    }
                    transform.Translate(Vector3.right * dirx * 40);
                   

                    GameObject BombCL = Instantiate(Bomb);
                    BombCL.GetComponent<BOOMB>().parentNM = NM;
                    BombCL.transform.SetParent(BombManager.transform);
                    Bounds bounds = rend.bounds;
                    float x = Random.Range(bounds.min.x, bounds.max.x);
                    float y = Random.Range(bounds.min.y, bounds.max.y);

                    BombCL.transform.position = new Vector2(
                       x,
                       y
                        );
                  
            
                }
                else if (diffy > diffx)
                {

                    if (BombManager)
                    {
                        foreach (Transform Bomb in BombManager.transform)
                        {


                            if (Bomb.gameObject.GetComponent<BOOMB>().parentNM == NM && Bomb.gameObject.GetComponent<BOOMB>().Lock)
                            {
                                Destroy(Bomb.gameObject);
                            }

                        }
                    }

                    transform.Translate(Vector3.up * diry * 40);

                   

                    GameObject BombCL = Instantiate(Bomb);
                    BombCL.GetComponent<BOOMB>().parentNM = NM;
                    BombCL.transform.SetParent(BombManager.transform);
                    Bounds bounds = rend.bounds;
                    float x = Random.Range(bounds.min.x, bounds.max.x);
                    float y = Random.Range(bounds.min.y, bounds.max.y);

                    BombCL.transform.position = new Vector2(
                       x,
                       y
                        );

                }

                break;
            case "Enemy":
                if (coll.enabled)
                {


                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f,3f), Random.Range(-3f, 3f),0f));

                }


                break;
        }
    }

}
