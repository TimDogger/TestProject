using UnityEngine;
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
    
    public void Shoot()
    {
        if (!cubePlayer) return;

        cubePlayer.Shoot();
    }
}
