using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {

	public int pointValue = 10;
	static int numBricks = 0;
	public int hitPoint = 1;
	public int loadedLevel = 1;

	// Use this for initialization
	void Start () {
		numBricks++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		hitPoint--;
		GameObject.Find ("Paddle").GetComponent<PadlleScript> ().AddPoint (pointValue);

		if (hitPoint <= 0) {
			Die();
		}
	}

	void Die(){
		Destroy (gameObject);
		numBricks--;
		Debug.Log (numBricks);
		if (numBricks <= 0) {
			// Load new level
			if (Application.loadedLevelName.Equals("level" + loadedLevel.ToString())){
				loadedLevel += 1;
				Application.LoadLevel("level" + loadedLevel.ToString());
		}
	}
}
}