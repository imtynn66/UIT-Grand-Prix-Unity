using UnityEngine;

public class ParticalHandle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float particleEmmisionRate = 0;
    TDCController TDCController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemSmokeEmissionModule;

    private void Awake()
    {
        TDCController = GetComponentInParent<TDCController>();
        particleSystemSmoke = GetComponent<ParticleSystem>();
        particleSystemSmokeEmissionModule = particleSystemSmoke.emission;
        particleSystemSmokeEmissionModule.rateOverTime = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        particleEmmisionRate = Mathf.Lerp(particleEmmisionRate, 0, Time.deltaTime * 5);
        particleSystemSmokeEmissionModule.rateOverTime = particleEmmisionRate;

        if (TDCController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                particleEmmisionRate = 60;
            }
            else particleEmmisionRate = Mathf.Abs(lateralVelocity) * 4;
        }
    }
}
