using System.Diagnostics;
using UnityEngine;

public class SmokeHandler : MonoBehaviour
{
    public bool isOverpassEmitter = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    TDCController TDCController;
    TrailRenderer trailRenderer;
    CarLayerHandler carLayerHandler;
    private void Awake()
    {
        TDCController = GetComponentInParent<TDCController>();
        carLayerHandler = GetComponentInParent<CarLayerHandler>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        trailRenderer.emitting = false;
        if (TDCController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (carLayerHandler.IsDrivingOnOverPass() && isOverpassEmitter)
            {
                trailRenderer.emitting = true;
            }
            if (!carLayerHandler.IsDrivingOnOverPass() && !isOverpassEmitter)
            {
                trailRenderer.emitting = true;
            }
        }
    }
}
