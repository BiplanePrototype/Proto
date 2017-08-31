using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1); // the index of the scene to load. In this case, the level is index 1. Could also use string name of level but this would break if the name changes
        }
	}
}
