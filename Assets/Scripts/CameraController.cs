using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float targetX = 0;
	public float targetY = 0;

	// Use this for initialization
	void Start () {
		//targetX = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x != targetX) {
			transform.Translate(new Vector3((targetX-transform.position.x)*0.1f, 0f,0f));
		}
		if (transform.position.y != targetY) {
			transform.Translate(new Vector3(0f, (targetY-transform.position.y)*0.1f, 0f));
		}
	}
}
