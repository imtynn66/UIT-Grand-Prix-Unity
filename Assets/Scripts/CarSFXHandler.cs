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
        UpdateEngineSFX();
        UpdateSkidSFX();
    }

    void UpdateEngineSFX()
    {
        //Động cơ
        float velocityMagnitude = tdcController.GetVelocityMagnitude();
        float desiredEngineVolume = velocityMagnitude * 0.05f;
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);
        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2.0f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }
    void UpdateSkidSFX()
    {
        //lốp xe trượt
        if (tdcController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                skidAudioSource.volume = Mathf.Lerp(skidAudioSource.volume, 1.0f, Time.deltaTime * 10);
                desiredSkidPitch = Mathf.Lerp(desiredSkidPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                skidAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.1f;
                desiredSkidPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else
        {
            skidAudioSource.volume = Mathf.Lerp(skidAudioSource.volume, 0.0f, Time.deltaTime * 10);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVelocity = collision.relativeVelocity.magnitude;
        float volume = relativeVelocity * 0.1f;
        carHitAudioSource.pitch = Random.Range(0.8f, 1.2f);
        carHitAudioSource.volume = volume;
        carHitAudioSource.PlayOneShot(carHitAudioSource.clip);
    }
}
