using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEN : MonoBehaviour {

    // Use this for initialization
    public int health;

    [SerializeField]
    GameObject Pickup;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool died = false;
        if (collision.gameObject.tag == "Bullet")
        {
            health-= 10;
        } else if (collision.gameObject.tag == "Missile")
        {
            health -= 20;
        }
        if (collision.gameObject.tag == "Player")
        {
            health =0;
        }
        if (health <= 0 && !died)
        {
            died = true;
            Instantiate(Pickup, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
