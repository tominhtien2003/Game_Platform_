using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }
}
