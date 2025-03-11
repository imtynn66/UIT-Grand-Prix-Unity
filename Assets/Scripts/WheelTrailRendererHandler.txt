using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    public bool isOverpassEmitter = false;
    //Component
    TopDownCarController topDownCarController;
    TrailRenderer trailRenderer;
    CarLayerHandler carLayerHandler;
    //Awake is called when the script instance is being loaded
    void Awake()
    {
        //Get the top down car controller
        topDownCarController = GetComponentInParent<TopDownCarController>();

        carLayerHandler = GetComponentInParent<CarLayerHandler>();

        //Get the trail renderer component
        trailRenderer = GetComponent<TrailRenderer>();

        //Set the trail renderer to not emit in the start
        trailRenderer.emitting = false;

    }
    void Update()
    {
        trailRenderer.emitting = false;
        //If the car tires are screeching then we'll emit a trail
        if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (CarLayerHandler.isDrivingOnOverPass() && isOverpassEmitter)
            {
                trailRenderer.emitting = true;
            }
            if (!carLayerHandler.IsDrivingOnOverPass() && && isOverpassEmitter)
            {
                trailRenderer.emitting = true;
            }
        }
    }
}