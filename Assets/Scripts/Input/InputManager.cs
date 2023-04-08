using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance 
    { 
        get
        {
            return _instance;
        } 
    }
    private InputControls _inputControls;
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        _inputControls = new InputControls();
    }

    private void OnEnable()
    {
        _inputControls.Enable();    
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }

    public Vector2 GetPlayerMovement() 
    { 
        return _inputControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta() 
    { 
        return _inputControls.Player.Look.ReadValue<Vector2>();
    }
    public bool PlayerShooted()
    {
        return _inputControls.Player.Shoot.triggered;
    }
    public bool PlayerJumped()
    {
        return _inputControls.Player.Jump.triggered;
    }
}
