using UnityEngine;
using System.Collections;

public class textHidden : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color c = GetComponent<tk2dTextMesh>().color;
		c.a = 0;
		GetComponent<tk2dTextMesh>().color = c;
	}
}
