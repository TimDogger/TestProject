using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
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
            canvas3d.worldCamera = this.cubePlayer.PlayerCamera;
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void OnView(InputAction.CallbackContext context)
        {
            if (!movement) return;
            movement.OnView(context);
        }

        public void OnViewGyro(InputAction.CallbackContext context)
        {
            if (!movement) return;
            movement.OnViewGyro(context);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!movement) return;
            movement.OnMove(context);
        }

        public void OnSpawnNPC(InputAction.CallbackContext context)
        {
            if (!cubePlayer) return;
            cubePlayer.OnSpawnNPCDown(context);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (!cubePlayer) return;
            Debug.Log("OnShoot");
            cubePlayer.OnShootPerfomed(context);
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
            if (context.performed)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    PointerEventData pointerEventData = GetPointerEventDataFromTouch(context);

                    if (!IsPointerOverUI(pointerEventData))
                    {
                        Shoot();
                    }
                }
                else
                {
                    Shoot();
                }
            }
        }

        public void Shoot()
        {
            if (!cubePlayer) return;
            cubePlayer.Shoot();
        }

        public void ClearNPCs()
        {
            NPC_Manager.Instance.RequestRemoveAllNPCs();
        }

        private PointerEventData GetPointerEventDataFromTouch(InputAction.CallbackContext context)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.pointerId = context.control.device.deviceId;
            pointerEventData.position = context.ReadValue<Vector2>();
            return pointerEventData;
        }
    
        private bool IsPointerOverUI(PointerEventData pointerEventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            return results.Count > 0;
        }

    }
}