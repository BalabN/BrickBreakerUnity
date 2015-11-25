using UnityEngine;
using System.Collections;

public class TextMeshControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.LoadLevel("level1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
