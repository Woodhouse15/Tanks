using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerTank : MonoBehaviour
{
    
    [SerializeField] private float speed = 0.05f;
    private float bulletSpeed = 10f;
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] public InputAction moveAction;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject minePrefab;
    public int maxBullets = 5;
    public int maxMines = 2;
    public int currentMines;
    GameObject[] bullets;
    
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
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            LayMine();
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
        Plane plane = new Plane(Vector3.up, transform.position);
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        if (bullets.Length >= maxBullets) return;
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            Vector3 rawDirection = target - transform.position;
            Vector3 horizontal = new Vector3(rawDirection.x, 0, rawDirection.z);
            Vector3 direction = horizontal.normalized;
            Vector3 xz = new Vector3(direction.x, 0, direction.z).normalized;
            Vector3 offset = transform.position + (xz * 1.5f);
            Vector3 start = new Vector3(offset.x, 0.2f, offset.z);
            var newObject = Instantiate(bulletPrefab, start, Quaternion.LookRotation(direction));
            newObject.GetComponent<Bullet>().players = true;
            newObject.transform.Rotate(90,0,0);
            newObject.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
            //Debug stuff - ignore
            if (false) {
                LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
                lineRenderer.endColor = Color.red;
                lineRenderer.startColor = Color.red;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + direction * distance);
            }
        }
    }

    void MovePlayer() {
        Vector2 directions = moveAction.ReadValue<Vector2>();
        transform.Rotate(0f, directions.x * rotationSpeed * Time.deltaTime, 0f);
        transform.Translate(0f,0f,directions.y * speed * Time.deltaTime);
    }
}
