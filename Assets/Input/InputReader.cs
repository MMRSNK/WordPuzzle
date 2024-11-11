using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class InputReader : Singleton<InputReader>
{
    private PlayerInput playerInput;

    public override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    public Vector2 GetTouchPosition()
    {
        return playerInput.Game.TouchPosition.ReadValue<Vector2>();
    }
}
