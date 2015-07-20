using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {

	public GameObject cameraHolder;
	public GameObject camera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("left")){
			rotate(cameraHolder, 1.0f);
		}
		if(Input.GetKey("right")){
			rotate(cameraHolder, -1.0f);
		}

		if(Input.GetKey("up")){
			moveCamera(camera, 1.0f);
		}
		if(Input.GetKey("down")){
			moveCamera(camera, -1.0f);
		}
	}

	void rotate(GameObject obj, float angle){
		obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + angle, obj.transform.eulerAngles.z);
	}

	void moveCamera(GameObject camera, float dist){
		float xPos = camera.transform.localPosition.x;
		float yPos = camera.transform.localPosition.y;
		float zPos = camera.transform.localPosition.z;

		float move = 1.0f*dist;

		camera.transform.localPosition = new Vector3(xPos, yPos, zPos + move);
	}
}
