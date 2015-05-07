using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Camera cam;
	public GameObject g;
	public GameObject cube;
	public Vector3 focusBlock;
	public float speed = 0.05f;

	private Vector3 lastFocusBlock;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 10F;
	public float sensitivityY = 10F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;

	
	
	// Use this for initialization
	void Start () {
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

	}
	
	// Update is called once per frame
	void Update () {
		if (axes == RotationAxes.MouseX)
		{
			cam.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
		
		Vector3 forward = cam.transform.forward;
		forward.y = 0; // this should be changed to being relative to the ground. later.
		if (Input.GetKey (KeyCode.W)) {
			this.transform.position = this.transform.position + forward * speed;
		} else if (Input.GetKey (KeyCode.S)) {
			this.transform.position = this.transform.position - forward *speed;
		} else if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.up * Time.deltaTime * 100);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (Vector3.down * Time.deltaTime * 100);
		} else if (Input.GetKey (KeyCode.Mouse0) && focusBlock != lastFocusBlock) {
			//ideally check if other cubes exist/check map, but for now. make sure to only make one cube/ click
			lastFocusBlock = focusBlock;
			cube.layer = LayerMask.NameToLayer("default");
			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.name = "block" + lastFocusBlock.x + "." + lastFocusBlock.y + "." + lastFocusBlock.z; //doesn't work - should be named based on placement
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
