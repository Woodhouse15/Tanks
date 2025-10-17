using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerTank : MonoBehaviour
{
    
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] public InputAction moveAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    void MovePlayer() {
        Vector2 directions = moveAction.ReadValue<Vector2>();
        if (directions == Vector2.left || directions == Vector2.right) {
            transform.Rotate(0f, directions.x * rotationSpeed * Time.deltaTime, 0f);
        }
        else {
            transform.Translate(0f,0f,directions.y * speed * Time.deltaTime);
        }
    }
}
