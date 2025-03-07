using UnityEngine;

public class SmokeHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    TDCController TDCController;
    TrailRenderer trailRenderer;

    private void Awake()
    {
        TDCController = GetComponentInParent<TDCController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TDCController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            trailRenderer.emitting = true;
        }
        else trailRenderer.emitting = false;
    }
}
