using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource StartFishingSound;
    [SerializeField] public AudioSource FishingFinisSound;
    [SerializeField] private AudioSource BuySound;
    [SerializeField] private AudioSource ColletSound;
    [SerializeField] public AudioSource Fish;
    [SerializeField] public AudioSource WaterSound;
    public static SoundManager instance; 
    void Start()
    {
        WaterSound.Play();
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else 
            instance = this;
    }

    public void BuySoundPlay()
    {
        BuySound.Play();
    }
    public void ColletSoundPlay()
    {
        ColletSound.Play();
    }
    public void StartFishingSounds()
    {
        StartFishingSound.Play();
    }
    public void WaterSoundPlay()
    {
        WaterSound.Play();
    }
    public void WaterSoundStop()
    {
        WaterSound.Stop();
    }
    public void fishSound()
    {
        Fish.Play();
    }
    
}
