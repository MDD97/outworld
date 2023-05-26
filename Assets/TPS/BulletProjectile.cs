using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    public Ennemi ennemi;
    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<BulletTarget>() != null) {
            // Hit target
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            if(other.gameObject.GetComponent<Ennemi>() != null)
            {
                other.gameObject.GetComponent<Ennemi>().ReceiveDammage(5);
            }
        } else {
            // Hit something else
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);
    }

}