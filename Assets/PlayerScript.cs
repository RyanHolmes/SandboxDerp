using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Camera cam;
	public GameObject g;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			this.transform.position = this.transform.position + cam.transform.forward;
			Debug.Log(this.transform.forward.ToString());
		}
		else if (Input.GetKey (KeyCode.DownArrow)) {
			this.transform.position = this.transform.position - cam.transform.forward;
		}
		else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.up * Time.deltaTime * 100);
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate(Vector3.down * Time.deltaTime * 100);
		}
	}
}
