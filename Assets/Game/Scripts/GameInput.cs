using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance
    {  get; private set; }

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause
    }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnRebinding;

    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetKeyBinding(Binding binding)
    {
        switch (binding)
        {
            default:
            case (Binding.MoveUp):
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case (Binding.MoveDown):
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case (Binding.MoveLeft):
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case (Binding.MoveRight):
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case (Binding.Interact):
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case (Binding.InteractAlternate):
                return playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
            case (Binding.Pause):
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void ReBinding(Binding binding, Action onActionReBound)
    {
        playerInputAction.Player.Disable();

        InputAction inputAction;
        int bindingIdx;

        switch (binding)
        {
            default:
            case (Binding.MoveUp):
                inputAction = playerInputAction.Player.Move;
                bindingIdx = 1;
                break;
            case (Binding.MoveDown):
                inputAction = playerInputAction.Player.Move;
                bindingIdx = 2;
                break;
            case (Binding.MoveLeft):
                inputAction = playerInputAction.Player.Move;
                bindingIdx = 3;
                break;
            case (Binding.MoveRight):
                inputAction = playerInputAction.Player.Move;
                bindingIdx = 4;
                break;
            case (Binding.Interact):
                inputAction = playerInputAction.Player.Interact;
                bindingIdx = 0;
                break;
            case (Binding.InteractAlternate):
                inputAction = playerInputAction.Player.InteractAlternate;
                bindingIdx = 0;
                break;
            case (Binding.Pause):
                inputAction = playerInputAction.Player.Pause;
                bindingIdx = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIdx)
            .OnComplete(callback =>
            {
                callback.Dispose();
                this.playerInputAction.Player.Enable();
                onActionReBound();
                OnRebinding?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }
}
