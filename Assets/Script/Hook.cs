using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    [SerializeField] private Camera _gameCamer;
    [SerializeField] private Collider2D coll;
    [SerializeField] private Transform HookTransform;
    private bool _canMove =true;
    private int _length;
    private int _strength;
    private int _fishCount;
    private Tweener _CameraTweener;
    private List<Fish> _FishList;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
        _FishList =new List<Fish>();
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_canMove && Input.GetMouseButton(0))
        {

            Vector3 _vector = _gameCamer.ScreenToWorldPoint(Input.mousePosition);
            
            
            Vector3 _position = transform.position;
            _position.x = _vector.x;
            transform.position = _position;
        }
    }
    public void StartFishing()
    {
        _canMove =true;
        _length =  IdleManager.instance.length - 20;
        _strength=  IdleManager.instance.strength;
        _fishCount =0;
        float time = (-_length) *.1f;
        _CameraTweener = _gameCamer.transform.DOMoveY(_length,1 *time ,false).OnUpdate(delegate
        {
            if(_gameCamer.transform.position.y <= -10)
            {
                transform.SetParent(_gameCamer.transform);

            }
        }).OnComplete(delegate
        {
            coll.enabled = true;
            _CameraTweener = _gameCamer.transform.DOMoveY(0,time*5,false).OnUpdate(delegate
            {
                if(_gameCamer.transform.position.y >-25)
                {
                    StopFishing();
                }
            });
        });
         ScreensManager.instance.ChangeScreen(Screens.GAME);
        coll.enabled =false;
        _FishList.Clear();

    }

    public void StopFishing()
    {
        _canMove = false;
        _CameraTweener.Kill(false);
        
        _CameraTweener =_gameCamer.transform.DOMoveY(0,2,false).OnUpdate(delegate
        {
            if(_gameCamer.transform.position.y >-11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x,-5);
            }
        }).OnComplete(delegate
        {
            transform.position =Vector2.down*5;
            coll.enabled =true;
            int num = 0;
            for (int i = 0; i < _FishList.Count; i++)
            {
                _FishList[i].transform.SetParent(null);
                _FishList[i].ResetFish();
                num +=_FishList[i].Type.Price;
            }
            IdleManager.instance.TotalGain = num;
            ScreensManager.instance.ChangeScreen(Screens.END);
        });
    }

    void OnTriggerEnter2D(Collider2D Terget)
    {
        if(Terget.CompareTag("Fish") && _fishCount != _strength)
        {
           _fishCount ++;
           SoundManager.instance.fishSound();
           Fish component = Terget.GetComponent<Fish>();
           component.Hooked();
           _FishList.Add(component);
           Terget.transform.SetParent(transform);
           Terget.transform.position = HookTransform.position;
           Terget.transform.rotation = HookTransform.rotation;
           Terget.transform.localScale = HookTransform.localScale;
           Terget.transform.DOShakeRotation(5,Vector3.forward*45,10,90,false).SetLoops(1,LoopType.Yoyo).OnComplete(delegate
           {
            Terget.transform.rotation = Quaternion.identity;
           });
           if(_fishCount == _strength)
           {
            StopFishing();
           }

        }
    }
}
