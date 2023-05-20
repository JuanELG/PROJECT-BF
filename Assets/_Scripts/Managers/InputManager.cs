using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : BaseSingleton<InputManager>
{
    public Action OnSprintAction;

    private PlayerInputs _inputs;

    protected override void Awake()
    {
        base.Awake();
        _inputs = new PlayerInputs();
        _inputs.Movement.Enable();
    }

    private void OnEnable()
    {
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
