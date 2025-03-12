using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CarLayerHandler : MonoBehaviour
{
    //public SpriteRenderer carOutlineSpriteRenderer;
    List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();

    List<Collider2D> overpassColliderList = new List<Collider2D>();
    List<Collider2D> underpassColliderList = new List<Collider2D>();

    Collider2D carCollider;

    //State
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
        foreach (GameObject overpassColliderGameObject in GameObject.FindGameObjectsWithTag("OverpassCollider"))
        {
            overpassColliderList.Add(overpassColliderGameObject.GetComponent<Collider2D>());

        }
        foreach (GameObject underpassColliderGameObject in GameObject.FindGameObjectsWithTag("UnderpassCollider"))
        {
            underpassColliderList.Add(underpassColliderGameObject.GetComponent<Collider2D>());
        }
        carCollider = GetComponentInChildren<Collider2D>();
    }
    void Start()
    {
        UpdateSortingAndCollisionLayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSortingAndCollisionLayers()
    {
        if (isDrivingOnOverPass)
        {
            SetSortingLayer("RaceTrackOverpass");
            

        }
        else
        {
            SetSortingLayer("Default");

            
        }
        SetCollisionWithOverPass();
    }

    void SetCollisionWithOverPass()
    {
        foreach (Collider2D collider2D in overpassColliderList)
        {
            Physics2D.IgnoreCollision(carCollider, collider2D, !isDrivingOnOverPass);
        }
        foreach (Collider2D collider2D in underpassColliderList)
        {
            if (isDrivingOnOverPass)
                Physics2D.IgnoreCollision(carCollider, collider2D, true);
            else Physics2D.IgnoreCollision(carCollider, collider2D, false);
        }
    }
    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in defaultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    //public bool isDrivingOnOverPass()
    //{
    //    return isDrivingOnOverPass;
    //}
    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("UnderpassTrigger"))
        {
            isDrivingOnOverPass = false;
            UpdateSortingAndCollisionLayers();
        }
        else if (collider2d.CompareTag("OverpassTrigger"))
        {
            isDrivingOnOverPass = true;
            UpdateSortingAndCollisionLayers();
        }
    }
}
