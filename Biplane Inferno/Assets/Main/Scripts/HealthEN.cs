using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEN : MonoBehaviour {

    // Use this for initialization
    public int health;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }
        if (collision.gameObject.tag == "Player")
        {
            health =0;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
