using UnityEngine;
using System.Collections;

public class CardMasterHolderScript : MonoBehaviour {

	public float offsetX;

	public int index;
	protected SimpleAnimation<Vector3> moveAnimation;

	// Use this for initialization
	void Start () {
		moveAnimation = new SimpleAnimation<Vector3>(transform.position,transform.position,
		                                             60,Vector3.Lerp);
		transform.GetChild(0).GetComponent<CardHolderScript>().enableCards ();
		Application.targetFrameRate = 60;
	}

	public void nextSet(){
		Vector3 newPos = new Vector3(transform.position.x - offsetX,transform.position.y,transform.position.z);
		moveAnimation.refresh(newPos);
		/*if(++index < transform.childCount)
			transform.GetChild(index).GetComponent<CardHolderScript>().enableCards();*/
	}

	public void endSet(){
		Vector3 newPos = new Vector3(transform.position.x,transform.position.y - 30,transform.position.z);
		moveAnimation.refresh(newPos,180);
		/*if(++index < transform.childCount)
			transform.GetChild(index).GetComponent<CardHolderScript>().enableCards();*/
	}

	// Update is called once per frame
	void Update () {
		if(moveAnimation.isActive()){
			transform.position = moveAnimation.step();
		}
	}
}
