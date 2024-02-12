using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class Fish : MonoBehaviour
{
    private FishType type;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    private float screnleft;
    private Camera Maincamera;
    private Tweener tweener;
    
    public Fish.FishType Type
    {
        get {
            return type;                                  
        }
        set {
            type = value;
            circleCollider2D.radius= type.colliderRadias;
            spriteRenderer.sprite = type.sprite;
        }
    }
    
    void Awake()
    {
        Maincamera = Camera.main;
        circleCollider2D =GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        screnleft = Maincamera.ScreenToWorldPoint(Vector3.zero).x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetFish()
    {
        if (tweener != null)
           tweener.Kill(false);
        float num = UnityEngine.Random.Range(type.MinimuLength, type.MaximuLength);
        circleCollider2D.enabled = true;
        Vector3 position = transform.position;
        position.y =num;
        position.x =screnleft;
        transform.position =position;

        float num2 = 1;
        float y = UnityEngine.Random.Range(num - num2, num + num2);
        Vector2 v = new Vector2(-position.x, y);

        float num3 = 3;
        float delay = UnityEngine.Random.Range(0, 2 * num3);
        tweener = transform.DOMove(v, num3, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(delegate
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        });
    }
    public void Hooked()
    {
        circleCollider2D.enabled = false;
        tweener.Kill(false);
    }
    [Serializable]
    public class FishType
    {
        public int Price;
        public float FishCount;
        public float MinimuLength;
        public float MaximuLength;
        public float colliderRadias;
        public Sprite sprite;
    }
}
