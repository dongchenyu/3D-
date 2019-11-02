using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Instantiate(Resources.Load("Prefabs/Sun"), new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/In"), new Vector3(0, 0, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
