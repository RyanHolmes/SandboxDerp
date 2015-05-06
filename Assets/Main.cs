using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public GameObject block;

	// Use this for initialization
	void Start () {
		GameObject t = (GameObject)Instantiate (block, new Vector3(1.1f, 2.2f, 4.5f), Quaternion.identity);
		t.name = "fuck";	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
