using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlAuto : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    private ControlRueda[] wheels;
    private Rigidbody rigidBody;

    // Jugador 1 o 2 unicamente
    [SerializeField, Range(1, 2)] private int jugador;
    // Input
    private InputAction moveAction;
    private InputAction giraCamaraAction;

    public float valorCambioX { get => giraCamaraAction.ReadValue<float>(); } // Esto para el gimbal

    // Getter para el jugador 1 o jugador 2
    public int jugador_actual { get { return jugador; }}

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass to improve stability and prevent rolling
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        // Get all wheel components attached to the car
        wheels = GetComponentsInChildren<ControlRueda>();

        switch (jugador)
        {
            case 1:
                moveAction = InputSystem.actions.FindAction("MoveJ1");
                giraCamaraAction = InputSystem.actions.FindAction("GiraCamaraJ1");
                break;
            case 2:
                moveAction = InputSystem.actions.FindAction("MoveJ2");
                giraCamaraAction = InputSystem.actions.FindAction("GiraCamaraJ2");
                break;
        }
    }

    // FixedUpdate is called at a fixed time interval 
    void FixedUpdate()
    {
        // Get player input for acceleration and steering
        //        float vInput = Input.GetAxis("Vertical"); // Forward/backward input
        //        float hInput = Input.GetAxis("Horizontal"); // Steering input
        Vector2 movement = moveAction.ReadValue<Vector2>();


        // Calculate current speed along the car's forward axis
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor

        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Determine if the player is accelerating or trying to reverse
        bool isAccelerating = Mathf.Sign(/*vInput*/movement.y) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            // Apply steering to wheels that support steering
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = /*hInput*/movement.x * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to motorized wheels
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = /*vInput*/movement.y * currentMotorTorque;
                }
                // Release brakes when accelerating
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                // Apply brakes when reversing direction
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(/*vInput*/movement.y) * brakeTorque;
            }
        }
    }
}