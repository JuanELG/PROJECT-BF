using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private LayerMask kitchenObjectLayerMask;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private CharacterData baseCharacterData;

    public Action<KitchenObject> OnSelectedKitchenObjectChanged;

    private bool _isWalking = false;
    private Vector3 _lastInteractionDirection = Vector3.zero;
    private KitchenObject _selectedKitchenObject;
    private float _currentSpeed = 0f;
    
    private void Start()
    {
        InputManager.Instance.OnSprintAction += OnSprintAction;
        InputManager.Instance.OnInteractAction += OnInteractAction;
        _currentSpeed = movementSpeed;
    }

    private void OnSprintAction(bool isRunning)
    {
        _currentSpeed = isRunning ? runningSpeed : movementSpeed;
        playerAnimatorController.SetBoolAnimation(PlayerAnimations.IsRunning, isRunning);
    }

    private void OnInteractAction()
    {
        if(_selectedKitchenObject != null)
            _selectedKitchenObject.Interact();
    }

    private void Update()
    {
        MovePlayer();
        HandleInteractions();
    }
    
    private void MovePlayer()
    {
        Vector2 inputVector = InputManager.Instance.GetInputVector();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);
        
        float moveDistance = _currentSpeed * Time.deltaTime;
        (bool canMove, Vector3 newDirection) = CheckIfCanMove(direction, moveDistance);

        if (canMove)
            transform.position += newDirection * moveDistance;

        _isWalking = direction != Vector3.zero;
        playerAnimatorController.SetBoolAnimation(PlayerAnimations.IsWalking, _isWalking);
        transform.forward = Vector3.Slerp(transform.forward, newDirection, Time.deltaTime * rotateSpeed);
    }
    
    private (bool, Vector3) CheckIfCanMove(Vector3 direction, float moveDistance)
    {
        float playerRadius = 0.7f;
        float playerHeight = 1.8f;
        var playerPosition = transform.position;
        bool canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius, direction, moveDistance);

        if (!canMove)
        {
            Vector3 xDirection = new Vector3(direction.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius, xDirection, moveDistance);

            if (canMove)
            {
                direction = xDirection;
            }
            else
            {
                Vector3 zDirection = new Vector3(0, 0, direction.z).normalized;
                canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius, zDirection, moveDistance);
                if (canMove)
                {
                    direction = zDirection;
                }
            }
        }

        return (canMove, direction);
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = InputManager.Instance.GetInputVector();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);

        if (direction != Vector3.zero)
        {
            _lastInteractionDirection = direction;
        }
        
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractionDirection, out var raycastHit, interactDistance, kitchenObjectLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out KitchenObject kitchenObject))
            {
                if (kitchenObject != _selectedKitchenObject)
                    NotifySelectedKitchenObjectChanged(kitchenObject);
            }
            else
            {
                NotifySelectedKitchenObjectChanged(null);
            }
        }
        else
        {
            NotifySelectedKitchenObjectChanged(null);
        }
    }

    private void NotifySelectedKitchenObjectChanged(KitchenObject kitchenObject)
    {
        _selectedKitchenObject = kitchenObject;
        OnSelectedKitchenObjectChanged?.Invoke(kitchenObject);
    }
}
