using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Stuff related to Camera
    private Transform _playerTransform;
    private Vector2 _rotation;
    private Vector3 _movement;

    //Stuff related to player
    private float _playerX;
    private float _playerY;
    
    public CharacterController controller;
    private Vector3 _velocity;
    public Camera playerCamera;
    public float yCameraLimit = 90f;
    public float speed = 10;
    public float gravity = -9.81f;

    public Transform groundedCheck;
    public float groundedDistance;
    public LayerMask groundedMask;

    private bool _isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = gameObject.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundedCheck.position, groundedDistance, groundedMask);
        _playerX = Input.GetAxisRaw("Horizontal");
        _playerY = Input.GetAxisRaw("Vertical");

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        Vector3 move = transform.right * _playerX + transform.forward * _playerY;

        controller.Move(move * speed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
        
        //movement of the camera
        _rotation.x = Input.GetAxis("Mouse Y");
        _rotation.y = Input.GetAxis("Mouse X");
        _rotation.x = Mathf.Clamp(_rotation.x, -yCameraLimit, yCameraLimit);
        playerCamera.transform.Rotate(-_rotation.x, 0, 0);
        _playerTransform.Rotate(Vector3.up * _rotation.y);
    }
 }
