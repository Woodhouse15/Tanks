using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class Bullet : MonoBehaviour {
    Rigidbody rb;
    public bool players = false;

    int reflects = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
        
        if (other.gameObject.CompareTag("Player") && !players) {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player") && players && reflects > 0) {
            Debug.Log("Destroy player - own bullet");
            Destroy(other.gameObject);
        }

        if ((!other.gameObject.CompareTag("Wall") || reflects != 0) &&
            (!other.gameObject.CompareTag("Breakable") || reflects != 0)) return;
        Debug.Log("Hit Wall");
        reflects++;
        rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, transform.forward);
    }
}
