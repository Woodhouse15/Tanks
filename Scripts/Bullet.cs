using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class Bullet : MonoBehaviour {
    private Rigidbody _rb;
    public bool players;
    private float _speedMod = 1f;
    private int _maxReflects = 2;

    int _reflects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    public void FireBullet(bool player, Vector3 direction) {
        players = player;
        transform.Rotate(90,0,0);
        _rb.linearVelocity = direction;
    }

    public void SetupBullet(int maxReflects, int speedMod) {
        _maxReflects = maxReflects;
        _speedMod = speedMod;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player") && !players) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Mine")) {
            other.gameObject.GetComponent<Mine>().DestroyRadius();
        }
        if (other.gameObject.CompareTag("Player") && players && _reflects != 0) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Breakable")) {
            if (_reflects < _maxReflects) {
                _reflects++;
                ContactPoint contact = other.contacts[0];
                Vector3 newVelocity = Vector3.Reflect(-other.relativeVelocity, contact.normal);
                _rb.linearVelocity = newVelocity * _speedMod;
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
