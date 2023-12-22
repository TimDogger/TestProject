using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CubePlayerUI : MonoBehaviour
{
    [SerializeField]
    private Button blockRollButton;

    [SerializeField]
    private Canvas canvas3d;

    [SerializeField]
    private CubePlayer cubePlayer;
    
    private Movement movement;

    public void InitializePlayer(CubePlayer cubePlayer)
    {
        this.cubePlayer = cubePlayer;
        movement = cubePlayer.GetComponent<Movement>();
        canvas3d.worldCamera = Camera.main;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    public void OnMoveForwardDown()
    {
        if (!movement) return;
        
        movement.SetMovementInput(Vector2.up);
    }
    
    public void OnMoveForwardUp()
    {
        if (!movement) return;
        
        movement.SetMovementInput(Vector2.zero);
    }
    
    public void ToggleBlockRoll()
    {
        if (!movement) return;
        
        movement.BlockRoll = !movement.BlockRoll;
        Image image = blockRollButton.GetComponent<Image>();
        if (image)
        {
            image.color = movement.BlockRoll ? Color.red : Color.white;
        }
    }
    
    public void SpawnNpc()
    {
        if (!cubePlayer) return;

        cubePlayer.SpawnNPC();
    }
    
    public void OnPointerDown(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (Application.platform == RuntimePlatform.Android)
        {
            PointerEventData pointerEventData = GetPointerEventDataFromTouch(context);

            if (!EventSystem.current.IsPointerOverGameObject(pointerEventData.pointerId))
            {
                Shoot();
            }
        }
        else
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {
        if (!cubePlayer) return;

        cubePlayer.Shoot();
    }
    
    private PointerEventData GetPointerEventDataFromTouch(InputAction.CallbackContext context)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.pointerId = context.control.device.deviceId;
        pointerEventData.position = context.ReadValue<Vector2>();

        return pointerEventData;
    }
}
