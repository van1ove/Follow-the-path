using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Transform _cameratransform;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;
        _cameratransform = Camera.main.transform;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {  
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = _inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = _cameratransform.forward * move.z + _cameratransform.right * move.x;
        move.y = 0;

        controller.Move(move.normalized * Time.deltaTime * _playerSpeed);

        if (_inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        playerVelocity.y += _gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
