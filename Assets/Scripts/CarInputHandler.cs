//CarInputHandler.cs
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    TDCController TDCController;
    void Awake()
    {
        TDCController = GetComponent<TDCController>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        TDCController.SetInputVector(inputVector);
    }
}
