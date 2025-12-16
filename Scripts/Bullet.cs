using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class Bullet : MonoBehaviour {
    private Rigidbody _rb;
    public bool players;

    int reflects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player") && !players) {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player") && players && reflects > 0) {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Breakable")) {
            if (reflects == 0) {
                reflects++;
                ContactPoint contact = other.contacts[0];
                Vector3 newVelocity = Vector3.Reflect(-other.relativeVelocity, contact.normal);
                _rb.AddForce(newVelocity);
                Quaternion target = Quaternion.LookRotation(newVelocity);
                Quaternion offset = Quaternion.Euler(90,0,0);
                transform.rotation = target * offset;
                _rb.angularVelocity = Vector3.zero;
            }
            else {
                Destroy(gameObject);
            }
            
        }
    }
}
