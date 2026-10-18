using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Runtime.ConstrainedExecution;
public class BOOMB : MonoBehaviour
{
    public static BOOMB instance;
    public Sprite OrigenBOMB;
    public Sprite HitBomb;
    public Sprite BOMB;
    bool hit = false;
    public bool Lock = false;
    public bool Bomb = false;
    public int parentNM;
    int Count = 0;
    SpriteRenderer sprite;
    CapsuleCollider2D capsule;


     void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
        instance = this;
      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Bullet") && Count >= 3 && !Lock)
        {
            Lock = true;
            StartCoroutine(Explosion(2f));
            return;
        }
        if (!collision.CompareTag("Bullet") || hit || Lock )
            return;
 
        hit = true;
        Count++;
        StartCoroutine(hitBomb(0.7f));

    }

 
   IEnumerator hitBomb(float wait)
    {
        sprite.sprite = HitBomb;
        transform.DOScale(transform.localScale * 1.1f, 0.2f).SetEase(Ease.InExpo).SetLoops(2,LoopType.Yoyo);
        yield return new WaitForSeconds(wait/2);
        sprite.sprite = OrigenBOMB;
        yield return new WaitForSeconds(wait / 2);
        hit = false;
        
    }

    IEnumerator Explosion(float wait)
    {
       
        sprite.sprite = HitBomb;
        transform.DOScale(transform.localScale * 1.5f, wait).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(wait);
        sprite.sprite = BOMB;
        Bomb = true;
        transform.DOScale(transform.localScale * 2.5f, 1).SetEase(Ease.OutFlash).SetLoops(2,LoopType.Yoyo);
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
       

    }

}
