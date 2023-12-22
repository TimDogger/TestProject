using UnityEngine;
using UnityEngine.UI;

public class CubePlayerUI : MonoBehaviour
{
    [SerializeField]
    private Button moveForwardButton;

    [SerializeField]
    private CubePlayer cubePlayer;
    
    private Movement movement;

    public void InitializePlayer(CubePlayer cubePlayer)
    {
        this.cubePlayer = cubePlayer;
        movement = cubePlayer.GetComponent<Movement>();
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
        Image image = moveForwardButton.GetComponent<Image>();
        if (image)
        {
            image.color = movement.BlockRoll ? Color.red : Color.white;
        }
    }
}
