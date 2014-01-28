using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
