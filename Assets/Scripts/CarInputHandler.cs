//CarInputHandler.cs
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    // Reference to TDCController to set the input vector for the car
    TDCController TDCController;

    // Ki?u input mà xe s? s? d?ng (phím m?i tên ho?c WASD)
    public enum ControlType
    {
        ArrowKeys,
        WASD
    }

    public ControlType controlType = ControlType.ArrowKeys;

    void Awake()
    {
        TDCController = GetComponent<TDCController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        // Ki?m tra ki?u ði?u khi?n và áp d?ng input týõng ?ng
        if (controlType == ControlType.ArrowKeys)
        {
            // Ði?u khi?n b?ng phím m?i tên
            inputVector.x = Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
            inputVector.y = Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
        }
        else if (controlType == ControlType.WASD)
        {
            // Ði?u khi?n b?ng phím WASD
            inputVector.x = Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0);
            inputVector.y = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);
        }

        // G?i input cho TDCController
        TDCController.SetInputVector(inputVector);
    }
}
