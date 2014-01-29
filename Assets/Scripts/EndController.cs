using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour {

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;


	void Start() {

		//if (true) {
		if(GameObject.Find ("LanguageController").GetComponent<LanguageController> ().getLanguage () == LanguageController.Language.CZ) {	
			text1.GetComponent<tk2dTextMesh>().text = "Díky za hru!";
			text2.GetComponent<tk2dTextMesh>().text = "Hra je inspirována skutečným příběhem skupiny bratrů Mašínů,\nznámou svým ozbrojeným odbojem proti komunistickému režimu\nv Československu v 50. letech.\n\n\"We don't see thing as they are, we see them as we are.\"\n    - Global Game Jam 2014 Theme";
		} else {
			text1.GetComponent<tk2dTextMesh>().text = "Thanks for playing!";
			text2.GetComponent<tk2dTextMesh>().text = "The story is based on actual events of Masin brothers group,\nknown for their armed resistance against the communist regime\nin Czechoslovakia in the 1950s.\n\n\"We don't see thing as they are, we see them as we are.\"\n    - Global Game Jam 2014 Theme";
		}


		StartCoroutine(FadeInText (text1, 0.5f));
		StartCoroutine(FadeInText (text2, 1f));
		StartCoroutine(FadeInText (text3, 3f));
		StartCoroutine(FadeInText (text4, 3f));

		GameObject.Find ("CardSet").transform.Translate(
			new Vector3(0f,0f,100f));
	}

	void Restart() {
		Destroy(GameObject.Find ("CardSet"));
		Destroy(GameObject.Find ("LanguageController"));
		Destroy(GameObject.Find ("BackgroundMusic"));
		StartCoroutine(FadeOutText (text1, 0.5f));
		StartCoroutine(FadeOutText (text2, 1f));
		StartCoroutine(FadeOutText (text3, 1.5f));
		StartCoroutine(FadeOutText (text4));
		StartCoroutine(SwitchScene ("menu", 2.5f));
	}

	IEnumerator FadeOutText(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dTextMesh>().color.a > 0) {
			Color c = obj.GetComponent<tk2dTextMesh>().color;
			c.a -= Time.deltaTime;
			obj.GetComponent<tk2dTextMesh>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator FadeInText(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dTextMesh>().color.a < 1) {
			Color c = obj.GetComponent<tk2dTextMesh>().color;
			c.a += Time.deltaTime;
			obj.GetComponent<tk2dTextMesh>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator SwitchScene(string scene, float delay) {
		yield return new WaitForSeconds(delay);
		Application.LoadLevel (scene);
	}

}
