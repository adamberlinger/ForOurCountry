using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour {

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;


	void Start() {
		GameObject.Find ("CardSet").transform.Translate(
			new Vector3(0f,0f,100f));
		StartCoroutine(FadeInText (text1, 0.5f));
		StartCoroutine(FadeInText (text2, 1f));
		StartCoroutine(FadeInText (text3, 3f));
		StartCoroutine(FadeInText (text4, 3f));
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
