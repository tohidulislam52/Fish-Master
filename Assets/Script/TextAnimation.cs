using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        textanimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void textanimation()
    {
        transform.DOScale(1.2f,1.5F).OnComplete(delegate
        {
            transform.DOScale(.6f,1.3F).OnComplete(delegate
            {
                textanimation();
            });
        });
    }
    
}
