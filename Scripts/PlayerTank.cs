using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerTank : MonoBehaviour
{
    
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] public InputAction moveAction;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject minePrefab;
    public int maxBullets = 5;
    public int maxMines = 2;
    public int currentMines = 0;
    public int currBullets = 0;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Shoot();
        }
    }

    private void FixedUpdate() {
        MovePlayer();

        
    }

    void LayMine() {
        if (currentMines >= maxMines) return;
        currentMines++;
        Instantiate(minePrefab, transform.position, Quaternion.identity);
    }

    void Shoot() {
        var mouseX = Mouse.current.position.x.ReadValue();
        var mouseY = Mouse.current.position.y.ReadValue();
        if (currBullets >= maxBullets) return;
        currBullets++;
        var newObject = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
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
