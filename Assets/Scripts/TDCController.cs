using UnityEngine;

public class TDCController : MonoBehaviour
{
    [Header("Car Settings")]
    public float accelerationFactor = 30.0f; // Tăng giá trị này để tăng tốc độ
    public float turnFactor = 3.5f;
    public float driftFactor = 0.98f; // Giảm lực cản để xe duy trì tốc độ cao hơn
    public float maxSpeed = 20.0f;
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 90; // Hướng xe ban đầu
    float velocityVsUp = 0;

    [Header("Sprites")]
    public SpriteRenderer carSpriteRenderer;
    public SpriteRenderer carShadowRenderer;

    Rigidbody2D carRigidbody2D;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.linearVelocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if (carRigidbody2D.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (accelerationInput == 0)
        {
            carRigidbody2D.linearDamping = 3.0f;
        }
        else
        {
            carRigidbody2D.linearDamping = 0.0f;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    public void ApplySteering()
    {
        float minSpeedBeforeAllowTurning = (carRigidbody2D.linearVelocity.magnitude / 8);
        minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);
    
        float reverseModifier = (velocityVsUp >= 0) ? 1.0f : -1.0f;

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurning * reverseModifier;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.linearVelocity, transform.right);

        carRigidbody2D.linearVelocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.linearVelocity);
    }
    public bool isTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationFactor < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(lateralVelocity) > 1.5f)
        {
            return true;
        }

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        accelerationInput = inputVector.y;
        steeringInput = inputVector.x;
    }
}

