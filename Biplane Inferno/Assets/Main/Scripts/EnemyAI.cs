using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    /* SERIALIZED FIELDS */
    [SerializeField]
    private float speed; // speed at which enemy flies

    [SerializeField]
    private float fireDelay; // delay between enemy shots, will be randomized

    [SerializeField]
    private GameObject projectile; // the projectile object to fire

    [SerializeField]
    private float projectileSpeed; // the speed at which projectiles fly

    /* PRIVATE VARIABLES */
    private Rigidbody2D rigidBody; // rigidbody of the plane

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, -speed);
        StartCoroutine(Fire());
	}

    // shooting function
    private void Shoot() {
        Vector3 position = gameObject.transform.position;
        position.y -= gameObject.GetComponent<Renderer>().bounds.size.y;
        GameObject spawnedProjectile = Instantiate(projectile, position, Quaternion.identity);
        spawnedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    // fire coroutine
    IEnumerator Fire() {
        while (true) {
            Shoot();
            yield return new WaitForSeconds(Random.Range(fireDelay - 1f, fireDelay + 1f));
        }
    }
}
