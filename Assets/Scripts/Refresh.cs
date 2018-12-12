/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simply refreshes scene and tracking.
/// To be attached to button or triggered by tapping on screen
/// </summary>
public class Refresh : MonoBehaviour {
    
	void Start () {
		
	}

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { //for Cardboard button
            RefreshScene();
        }
    }

    public void RefreshScene() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
