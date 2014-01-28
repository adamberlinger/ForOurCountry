using UnityEngine;
using System.Collections;

public class CenterController : MonoBehaviour {

	public string mainPosition = "center";
	public string position = "left";

	public GameObject cam;
	private tk2dCamera c;
		
		// Use this for initialization
	void Start () {

		c = cam.GetComponent<tk2dCamera> ();

		if(mainPosition == "center") {
			if (position == "left") {
				transform.position = new Vector3 (-10.24f*(c.TargetResolution.x/c.TargetResolution.y/1024f*768f)/2f,0,0);
			} else {
				transform.position = new Vector3 (10.24f*(c.TargetResolution.x/c.TargetResolution.y/1024f*768f)/2f,0,0);
			}
		} else if(mainPosition == "left") {
			if (position == "left") {
				transform.position = new Vector3 (-30-10.24f*(c.TargetResolution.x/c.TargetResolution.y/1024f*768f)/2f,0,0);
			} else {
				transform.position = new Vector3 (-30+10.24f*(c.TargetResolution.x/c.TargetResolution.y/1024f*768f)/2f,0,0);
			}
		} else if(mainPosition == "right") {
			if (position == "left") {
				transform.position = new Vector3 (30-10.24f*(c.TargetResolution.x/c.TargetResolution.y/1024f*768f)/2f,0,0);
			} else {
				transform.position = new Vector3 (30+10.24f*(c.TargetResolution.x/c.TargetResolution.y/1024f*768f)/2f,0,0);
			}
		}
	}

}
