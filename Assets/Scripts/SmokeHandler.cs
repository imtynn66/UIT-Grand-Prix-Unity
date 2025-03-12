using UnityEngine;

public class SmokeHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    TDCController TDCController;
    TrailRenderer trailRenderer;
    public bool isOverpassEmitter = false;
    CarLayerHandler carLayerHandler;
    private void Awake()
    {
        TDCController = GetComponentInParent<TDCController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
        carLayerHandler = GetComponentInParent<CarLayerHandler>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
