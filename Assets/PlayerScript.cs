using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Camera cam;
	public GameObject g;
	public GameObject cube;
	public Vector3 focusBlock;
	public float speed = 0.05f;

	private Vector3 lastFocusBlock;


	// Use this for initialization
	void Start () {
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forward = cam.transform.forward;
		forward.y = 0; // this should be changed to being relative to the ground. later.
		if (Input.GetKey (KeyCode.UpArrow)) {
			this.transform.position = this.transform.position + forward * speed;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			this.transform.position = this.transform.position - forward *speed;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (Vector3.up * Time.deltaTime * 100);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (Vector3.down * Time.deltaTime * 100);
		} else if (Input.GetKey (KeyCode.Space) && focusBlock != lastFocusBlock) {
			//ideally check if other cubes exist/check map, but for now. make sure to only make one cube/ click
			lastFocusBlock = focusBlock;
			cube.layer = LayerMask.NameToLayer("default");
			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.layer = LayerMask.NameToLayer("Ignore Raycast");
		}

		Ray ray = this.cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit[] hits = Physics.RaycastAll (ray, 500);
		if (hits.Length > 0) {

			//normalize point
			focusBlock = hits[0].point;
			focusBlock.x = Mathf.Round(focusBlock.x);
			focusBlock.y = Mathf.Floor((focusBlock.y+1));
			focusBlock.z = Mathf.Round(focusBlock.z);

			cube.transform.position = focusBlock;
		}
	}
}
