using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	public float ballMaxSpeed = 10f;
	public AudioClip[] blipAudio;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Die() {
		Destroy (gameObject);
		GameObject padlleObject = GameObject.Find ("Paddle");
		PadlleScript paddleScript = padlleObject.GetComponent<PadlleScript> ();
		paddleScript.LoseLife ();
	}

	void OnCollisionEnter (Collision collision) {
		// Play a blip
		AudioSource.PlayClipAtPoint(blipAudio[0], transform.position);
	}


}
