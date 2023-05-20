using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    public Action OnSprintAction;

    private PlayerInputs _inputs;

    public void Awake()
    {
        _inputs.Movement.Enable();
        _inputs.Movement.Sprint.performed += NotifySprintAction;
    }

    public void OnDisable()
    {
        _inputs.Disable();
    }
    
    private void NotifySprintAction(InputAction.CallbackContext callbackContext)
    {
        OnSprintAction?.Invoke();
    }
}
