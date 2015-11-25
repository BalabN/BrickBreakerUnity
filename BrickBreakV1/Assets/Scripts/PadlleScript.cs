using UnityEngine;
using System.Collections;

public class PadlleScript : MonoBehaviour {

	public float paddleSpeed = 0.4f;
	public GameObject ballPrefab;
	TextMesh guiLives;
	TextMesh guiScore;
	public Renderer rendPaddle;
	GameObject wallLeft;
	GameObject wallRight;
	float widthPaddle;
	float maxX = 1f;
	float minX = 1f;
	Rigidbody ballRigidbody;


	public int lives = 3;
	int score = 0;

	GameObject attachedBall = null;

	// Use this for initialization
	void Start () {
		wallLeft = GameObject.Find ("WallLeft");
		wallRight = GameObject.Find("WallRight");
		rendPaddle = GetComponent<Renderer> ();
		widthPaddle = rendPaddle.bounds.extents.x;
		DontDestroyOnLoad (gameObject);
		guiLives = (TextMesh)GameObject.Find ("GUILivesText").GetComponent(typeof(TextMesh));
		guiScore = (TextMesh)GameObject.Find ("GUIScoreText").GetComponent(typeof(TextMesh));
		DontDestroyOnLoad (GameObject.Find("SoundPlayer"));
		guiLives.text = "Lives: " + lives;
		guiScore.text = "Score: " + score;
		SpawnBall ();
	}

	public void OnLevelWasLoaded (int level) {
		wallLeft = GameObject.Find ("WallLeft");
		wallRight = GameObject.Find("WallRight");
		SpawnBall ();
	}

	public void AddPoint(int v) {
		score += v;
		guiScore.text = "Score: " + score;
	}

	public void LoseLife() {
		lives--;
		guiLives.text = "Lives: " + lives;
		if (lives > 0) {
			SpawnBall ();
		} else {
			Destroy(gameObject);
			Application.LoadLevel("GameOver");
		}
	}

	public void SpawnBall() {
		// Spawn/Instantiate new ball
		if (ballPrefab == null) {
			Debug.Log ("Forrgot the ball");
			return;
		}

		attachedBall = (GameObject)Instantiate (ballPrefab, transform.position + new Vector3(0.1f, 0.5f, 0), Quaternion.identity);
	}


	// Update is called once per frame
	void Update () {

		transform.Translate(paddleSpeed*Time.deltaTime*Input.GetAxis("Horizontal"), 0 ,0);


//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
//			// Get movement of the finger since last frame
//			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//			// Move object across XY plane
//			transform.Translate(touchDeltaPosition.x * paddleSpeed * Time.deltaTime, 0, 0);
//			PaddleBounds ();
//
//		}

		

		// Launch the ball
		if (attachedBall){
			ballRigidbody = attachedBall.GetComponent<Rigidbody>();
			ballRigidbody.position = transform.position + new Vector3(0.1f, 0.5f, 0);

			if (Input.GetButtonDown("Space")) {
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce(-50f * Input.GetAxis("Horizontal"), 630f, 0);
				attachedBall = null;
				PaddleBounds ();
			}
			
//			if (Input.GetTouch(0).phase == TouchPhase.Began) {
//				//Fire ball
//				ballRigidbody.isKinematic = false;
//				ballRigidbody.AddForce(-50f * Input.GetAxis("Horizontal"), 630f, 0);
//				attachedBall = null;
//			}
		}

	}

	void FixedUpdate() {
	}

	void OnCollisionEnter (Collision col) {
		foreach (ContactPoint contact in col.contacts) {
			if (contact.thisCollider == GetComponent<Collider>()) {
				// This is the paddle's contact point
				float english = ((contact.point.x - transform.position.x)/2f);
				if (ballRigidbody != null){
					ballRigidbody.isKinematic = true;
					ballRigidbody.isKinematic = false;
					Debug.Log(english.ToString());
					if (english > 0.7f) {
						ballRigidbody.AddForce(700f * 0.7f, 700f * (1f - 0.7f), 0);
					}else if (english < -0.7f) {
						ballRigidbody.AddForce(700f * -0.7f, 700f * (1f - 0.7f), 0);
					} else {
						ballRigidbody.AddForce(700f * english, 700f * (1f - Mathf.Abs(english)), 0);
					}
				}
			}

		}
	}

	void PaddleBounds() {
		// Bounds for padlle
		Vector3 distFromLeft = transform.position - wallLeft.transform.position;
		Vector3 distFromRight = wallRight.transform.position - transform.position;
		//Debug.Log ("Distance from left wall = " + distFromLeft);
		if (distFromRight.x <= ((widthPaddle / 2f) + 1.27f)) {
			if (maxX == 1f){
				maxX = transform.position.x;
			}
			transform.position = new Vector3 (maxX, transform.position.y, transform.position.z);
			//Debug.Log("Trigger");
		}
		
		if (distFromLeft.x <= ((widthPaddle / 2f) + 1.27f)) {
			if (minX == 1f){
				minX = transform.position.x;
			}
			transform.position = new Vector3 (minX, transform.position.y, transform.position.z);
			//Debug.Log("Trigger");
		}
	}
}
