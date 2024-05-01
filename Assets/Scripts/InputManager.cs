using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    private Animations animations;

    [SerializeField] private GameObject child;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        animations = child.GetComponent<Animations>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Atack.performed += ctx => animations.AtackAnimation();
        onFoot.Block.started += ctx => animations.BlockAnimations(true);
        onFoot.Block.canceled += ctx => animations.BlockAnimations(false);
        onFoot.Movement.performed += ctx => animations.RunAnimation(onFoot.Movement.ReadValue<Vector2>());
        onFoot.Movement.canceled += ctx => animations.RunAnimation(new Vector2(0, 0));
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void Update()
    {
        if(onFoot.Dash.inProgress)
        {
            StartCoroutine(motor.Dash(onFoot.Movement.ReadValue<Vector2>()));
        }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
