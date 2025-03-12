using UnityEngine;
using UnityEngine.Audio;
using UnityEngine;
public class CarSFXHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Audio mixer")]
    public AudioMixer audioMixer;
    [Header ("Audio sources")]
    public AudioSource engineAudioSource;
    public AudioSource skidAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float desiredSkidPitch = 0.5f;
    TDCController tdcController;
    void Awake()
    {
        tdcController = GetComponentInParent<TDCController>();
    }

    void Start()
    {
        audioMixer.SetFloat("SFXVolume", 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
