using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEN : MonoBehaviour {

    // Use this for initialization
    public int health;
    bool died = false;
    [SerializeField]
    GameObject Pickup;
	Sound sounds;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		sounds = GetComponent<Sound>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health-= 10;
        } else if (collision.gameObject.tag == "Missile")
        {
            health -= 20;
			sounds.ExplosionSound();
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
			sounds.EnemyDeathSound();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
