using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isDashing = false;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float dashSpeed = 2f;
    [SerializeField] private AnimationCurve dashSpeedCurve;
    [SerializeField] private float dashTime = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public IEnumerator Dash(Vector2 direction)
    {
        if (direction == Vector2.zero) yield break;
        if (isDashing) yield break;

        isDashing = true;

        var elapsedTime = 0f;
        while (elapsedTime < dashTime)
        {
            var velocityMultiplier = dashSpeed * dashSpeedCurve.Evaluate(elapsedTime);

            ApplyVelocity(direction, velocityMultiplier);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isDashing = false;
        yield break;
    }

    private void ApplyVelocity(Vector3 desiredVelocity, float multiplier)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = desiredVelocity.x;
        moveDirection.z = desiredVelocity.y;
        controller.Move(transform.TransformDirection(moveDirection) * multiplier);


    }
}