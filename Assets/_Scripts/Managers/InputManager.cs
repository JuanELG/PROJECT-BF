using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class InputManager : BaseSingleton<InputManager>
{
    public Action<bool> OnSprintAction;
    public Action OnInteractAction;

    private PlayerInputs _inputs;

    protected override void Awake()
    {
        base.Awake();
        _inputs = new PlayerInputs();
        _inputs.Movement.Enable();
    }

    private void OnEnable()
    {
        _inputs.Movement.Sprint.started += NotifySprintActionStarted;
        _inputs.Movement.Sprint.canceled += NotifySprintActionEnded;
        _inputs.Movement.Interact.performed += NotifyInteractAction;
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
    
    public Vector2 GetInputVector()
    {
        return _inputs.Movement.WASD.ReadValue<Vector2>();
    }
    
    private void NotifySprintActionStarted(InputAction.CallbackContext callbackContext)
    {
        bool isRunning = true;
        OnSprintAction?.Invoke(isRunning);
    }
    
    private void NotifySprintActionEnded(InputAction.CallbackContext callbackContext)
    {
        bool isRunning = false;
        OnSprintAction?.Invoke(isRunning);
    }

    public void NotifyInteractAction(InputAction.CallbackContext callbackContext)
    {
        OnInteractAction?.Invoke();
    }
}

public class WebRequest : MonoBehaviour
{
    public void GetAPI()
    {
        StartCoroutine(GetCoroutine());
    }

    public void PostAPI()
    {
        StartCoroutine(PostCoroutine());
    }

    private IEnumerator GetCoroutine()
    {
        UnityWebRequest web = UnityWebRequest.Get("URL HERE");
        yield return web.SendWebRequest();
        switch (web.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogWarning("Hubo un error en la red");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogWarning("Error de protocolo");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(web.downloadHandler.text);
                break;
        }
    }
    
    private IEnumerator PostCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("clave", "valor");
        form.AddField("clave", "valor");

        UnityWebRequest web = UnityWebRequest.Post("URL HERE", form);
        yield return web.SendWebRequest();
        switch (web.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogWarning("Hubo un error en la red");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogWarning("Error de protocolo");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(web.downloadHandler.text);
                break;
        }
    }
}
