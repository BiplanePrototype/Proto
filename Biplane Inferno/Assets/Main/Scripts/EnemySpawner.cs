using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    /* SERIALIZED FIELDS */

    [SerializeField]
    private int spawnDelay; // length of time between spawns in seconds (will be randomized)

    [SerializeField]
    private GameObject[] entities; // array of gameobject entities to randomly spawn

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
	}

    // get random position for spawning
    private Vector3 GetRandomPosition(Renderer renderer)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(renderer.bounds.size.x, Screen.width - renderer.bounds.size.x), Random.Range(Screen.height + 100, Screen.height + 200), 0));
        position.z = 0;
        return position;
    }

    // get random entity to spawn
    private GameObject GetRandomEntity() {
        return entities[Random.Range(0, entities.Length - 1)];
    }

    // spawn coroutine
    IEnumerator SpawnEnemy() {
        while(true) {
            GameObject entity = GetRandomEntity();
            Vector3 position = GetRandomPosition(entity.GetComponent<Renderer>());
            Quaternion rotation = Quaternion.Euler(0, 0, 180);
            Instantiate(entity, position, rotation);
            yield return new WaitForSeconds(Random.Range(spawnDelay - 0.5f, spawnDelay + 2f));
        }
    }
}
