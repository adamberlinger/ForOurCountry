using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameObject.Find("LanguageController").GetComponent<LanguageController>().getLanguage() == LanguageController.Language.CZ){
			transform.FindChild("BlackPlayerText").GetComponent<tk2dTextMesh>().text = "Hraje černý hráč";
			transform.FindChild("WhitePlayerText").GetComponent<tk2dTextMesh>().text = "Hraje bílí hráč";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
