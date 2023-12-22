using System;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public bool BlockRoll
    {
        get => blockRoll;
        set => blockRoll = value;
    }

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Single smoothMovementTime = 0.1f;

    [SerializeField]
    private Single viewSencitivity = 0.05f;

    private NetworkIdentity networkIdentity;
    private CharacterController characterController;
    private IA_General iaGeneral;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private Vector2 inputVector;
    private Vector3 rotationDelta;

    private bool blockRoll = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!networkIdentity.isLocalPlayer) return;

        inputVector = context.ReadValue<Vector2>();
    }

    public void SetMovementInput(Vector2 input)
    {
        inputVector = input;
    }

    public void OnView(InputAction.CallbackContext context)
    {
        if (!networkIdentity.isLocalPlayer) return;

        Vector2 mouseDelta = context.ReadValue<Vector2>();
        rotationDelta = new Vector3(-mouseDelta.y, mouseDelta.x, 0) * viewSencitivity * Time.fixedDeltaTime;
    }

    public void OnViewGyro(InputAction.CallbackContext context)
    {
        if (!networkIdentity.isLocalPlayer) return;

        Vector3 gyroDelta = context.ReadValue<Vector3>();
        rotationDelta = gyroDelta * -1;
    }

    private void Awake()
    {
        networkIdentity = GetComponent<NetworkIdentity>();
        characterController = GetComponent<CharacterController>();

        iaGeneral = new IA_General();
        if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
    }

    private void OnEnable()
    {
        if (!networkIdentity.isLocalPlayer) return;

        iaGeneral.Enable();
    }

    private void OnDisable()
    {
        if (!networkIdentity.isLocalPlayer) return;

        iaGeneral.Disable();
    }

    private void FixedUpdate()
    {
        if (!networkIdentity.isLocalPlayer) return;

        UpdateView();
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 currentLocation = transform.position;
        float gravity = Physics.gravity.y;
        Vector3 targetLocation = currentLocation +
                                 transform.TransformDirection(new Vector3(inputVector.x, gravity, inputVector.y) *
                                                              (moveSpeed * Time.fixedDeltaTime));
        Vector3 nextLocation = Vector3.SmoothDamp(currentLocation, targetLocation, ref velocity, smoothMovementTime);
        characterController.Move(nextLocation - currentLocation);
    }

    private void UpdateView()
    {
        if (mainCamera)
        {
            transform.Rotate(Vector3.up * rotationDelta.y);
            mainCamera.transform.Rotate(Vector3.right * rotationDelta.x);
            if (!blockRoll)
            {
                mainCamera.transform.Rotate(Vector3.forward * -rotationDelta.z);
            }
        }
        else
        {
            transform.Rotate(Vector3.up * rotationDelta.y);
            transform.Rotate(Vector3.right * rotationDelta.x);
        }
    }
}