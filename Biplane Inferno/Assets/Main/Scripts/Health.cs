using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    // Use this for initialization
    public float health;
    public float maxHealth;
    Rigidbody2D rb;
    Plane_Details dets;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        dets = GetComponent<Plane_Details>();
        maxHealth = health;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }
        if (collision.gameObject.tag == "Plane")
        {
            health-=5;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        dets.SetHealthBar(getHealthPercent());
    }

    public float getHealthPercent()
    {
        return health / maxHealth;
    }
}
