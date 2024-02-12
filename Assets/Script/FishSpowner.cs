using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpowner : MonoBehaviour
{
    [SerializeField] private Fish FishPrefab;
    [SerializeField] private Fish.FishType[] FishType;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < FishType.Length; i++)
        {
            int num= 0;
            while(num <FishType[i].FishCount)
            {
                Fish fish = Instantiate(FishPrefab,transform);
                fish.Type = FishType[i];
                fish.ResetFish();
                num++;
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
