using UnityEngine;

public class Mine : MonoBehaviour {
    private float radius = 3f;
    [SerializeField] Collider bodyCollider;
    [SerializeField] Collider radiusCollider;
    Material material;
    private bool player;
    private bool triggered;
    private float timer;

    private float initTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initTime = Time.time;
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - initTime >= 7) {
            material.color = Color.red;
        }
        if (Time.time - initTime >= 10) {
            DestroyRadius();
        }

        if (triggered) {
            material.color = Color.red;
            if (Time.time - timer >= 2) {
                DestroyRadius();
            }
        }
    }

    public void DestroyRadius() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders) {
            if (!collider.gameObject.CompareTag("Wall") && collider.gameObject.layer != 3) {
                Destroy(collider.gameObject);
            }
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (other.bounds.Intersects(bodyCollider.bounds) && other.gameObject.CompareTag("Bullet")) {
            DestroyRadius();
        }
        else {
            if (other.gameObject.CompareTag("Player") && !player || other.gameObject.CompareTag("Enemy") && player) {
                triggered = true;
                timer = Time.time;
            }
        }
    }
}
