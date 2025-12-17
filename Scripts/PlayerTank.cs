using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTank : MonoBehaviour
{
    
    [SerializeField] private float speed = 0.05f;
    private float bulletSpeed = 10f;
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] public InputAction moveAction;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject minePrefab;
    public int maxBullets = 5;
    public int maxLives = 3;
    public int currentLives;
    public int maxMines = 2;
    public int currentMines;
    GameObject[] _bullets;
    private Rigidbody _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        moveAction.Enable();
        currentLives = maxLives;
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
        Vector3 position = new Vector3(transform.position.x, -0.6f, transform.position.z);
        Instantiate(minePrefab, position, Quaternion.identity);
    }

    void Shoot() {
        Plane plane = new Plane(Vector3.up, transform.position);
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        _bullets = GameObject.FindGameObjectsWithTag("Bullet");
        if (_bullets.Length >= maxBullets) return;
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            Vector3 rawDirection = target - transform.position;
            Vector3 horizontal = new Vector3(rawDirection.x, 0, rawDirection.z);
            Vector3 direction = horizontal.normalized;
            Vector3 xz = new Vector3(direction.x, 0, direction.z).normalized;
            Vector3 offset = transform.position + (xz * 2f);
            Vector3 start = new Vector3(offset.x, 0.2f, offset.z);
            var newObject = Instantiate(bulletPrefab, start, Quaternion.LookRotation(direction));
            newObject.GetComponent<Bullet>().players = true;
            newObject.transform.Rotate(90,0,0);
            newObject.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }

    void MovePlayer() {
        Vector2 directions = moveAction.ReadValue<Vector2>();
        transform.Rotate(0f, directions.x * rotationSpeed * Time.deltaTime, 0f);
        Vector3 forward = transform.forward * (directions.y * speed);
        _rb.linearVelocity = new Vector3(forward.x, _rb.linearVelocity.y, forward.z);

    }
}
