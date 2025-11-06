using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision other) {
        if (!other.gameObject.name.Equals("Wall")) {
            Destroy(other.gameObject);
        }
        else {
            transform.position = Vector3.Reflect(transform.position, other.contacts[0].normal);
        }
    }
}
