using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CarLayerHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();
    bool isDrivingOnOverPass = false;
    public void Awake()
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.sortingLayerName == "Default")
            {
                defaultLayerSpriteRenderers.Add(spriteRenderer);
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

    public void UpdateSortingAndCollisionLayers()
    {
        if (isDrivingOnOverPass)
        {
            setSortingLayer("Over");
        }
        else
        {
            setSortingLayer("Default");
        }
    }

    public void setSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in defaultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag ("Under"))
        {
            isDrivingOnOverPass = false;
            UpdateSortingAndCollisionLayers();
        }
        else if (collider2D.CompareTag("Over"))
        {
            isDrivingOnOverPass = true;
        }
    }
}
