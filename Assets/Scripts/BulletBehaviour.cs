using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody bulletRB;

    private float bulletSpeed = 5f;
    private void Awake()
    {
        bulletRB= GetComponent<Rigidbody>();
    }

    void Update()
    {
        bulletRB.AddForce(bulletRB.transform.forward * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
