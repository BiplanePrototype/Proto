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
	Sound sounds;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        dets = GetComponent<Plane_Details>();
        maxHealth = health;
		sounds = GetComponent<Sound>();
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
			sounds.EnemyDeathSound();
        }
        dets.SetHealthBar(getHealthPercent());
    }

    public float getHealthPercent()
    {
        return health / maxHealth;
    }
}
