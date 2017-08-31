using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour {
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.tag == "Bullet" || transform.tag == "Missile")
        {
            Destroy(gameObject, 5f);
        }
    }
    // triggered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Plane" || collision.gameObject.tag == "Pickup")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (rb.velocity.magnitude < 1)
        {
            Destroy(gameObject);
        }
    }
}
