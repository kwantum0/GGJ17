using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Range(0,6)]
	public const int DEFAULT_START_HEALTH = 6;
	[Range(0,9)]
	public const int DEFAULT_START_ENERGY = 0;
	[Range(0f,1f)]
	public const float DEFAULT_START_FREQUENCY = 0f;
	[Range(0f,1f)]
	public const float FREQUENCY_STEP = 0.1f;
	[Range(0f,3f)]
	public const float STEP_SECONDS = 0.5f;
	private float nextPlayerOneStepTime = -1f;
	private float nextPlayerTwoStepTime = -1f;

	public int PlayerOneScore;
	public int PlayerTwoScore;

	public int PlayerOneHealth;
	public int PlayerTwoHealth;
	public int PlayerOneEnergy;
	public int PlayerTwoEnergy;

	public float PlayerOneStartFrequency;
	public float PlayerOneEndFrequency;
	public float PlayerTwoStartFrequency;
	public float PlayerTwoEndFrequency;


	// Use this for initialization
	void Start () {
		newGame ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*if (Input.GetKey (KeyCode.A)) {
			PlayerOneFrequency += FREQUENCY_STEP * Time.smoothDeltaTime;
		} else {
			PlayerOneFrequency -= FREQUENCY_STEP * Time.smoothDeltaTime;
		}
		PlayerOneFrequency = Mathf.Max (PlayerOneFrequency, 0);
		PlayerOneFrequency = Mathf.Min (PlayerOneFrequency, 1);

		if (Input.GetKey(KeyCode.Semicolon)) {
			PlayerTwoFrequency += FREQUENCY_STEP * Time.smoothDeltaTime;
		} else {
			PlayerTwoFrequency -= FREQUENCY_STEP * Time.smoothDeltaTime;
		}
		PlayerTwoFrequency = Mathf.Max (PlayerTwoFrequency, 0);
		PlayerTwoFrequency = Mathf.Min (PlayerTwoFrequency, 1);

		WaveController.setFrequencies (PlayerOneFrequency, PlayerTwoFrequency);
		*/
		// SET BOMB 
		if (Input.GetKeyDown (KeyCode.Y) && Debug.isDebugBuild) {
			respawnBomb ();
		}
		// PLAYER ONE
		if (Input.GetKeyDown (KeyCode.A)) {
			IncreaseFrequencyStateP1 ();
		} else if (Input.GetKeyDown (KeyCode.S)) {
			DecreaseFrequencyStateP1 ();
		}

		// WAVE ANIMTION - PLAYER ONE
		if (Time.fixedTime < nextPlayerOneStepTime  && PlayerOneStartFrequency != PlayerOneEndFrequency) {
			float percent = (nextPlayerOneStepTime - Time.fixedTime) / STEP_SECONDS;
			float value = Mathf.Lerp (PlayerOneEndFrequency, PlayerOneStartFrequency, percent);
			WaveController.setFrequencies ("one", value);
			WaveController.setWidth ("one", percent);
		}

		// PLAYER TWO
		if (Input.GetKeyDown (KeyCode.L)) {
			IncreaseFrequencyStateP2 ();
		} else if (Input.GetKeyDown(KeyCode.Semicolon)) {
			DecreaseFrequencyStateP2 ();
		}

		// WAVE ANIMATION - PLAYER TWO
		if (Time.fixedTime < nextPlayerTwoStepTime && PlayerTwoStartFrequency != PlayerTwoEndFrequency) {
			float percent = (nextPlayerTwoStepTime - Time.fixedTime) / STEP_SECONDS;
			float value = Mathf.Lerp (PlayerTwoEndFrequency, PlayerTwoStartFrequency, percent);
			WaveController.setFrequencies ("two", value);
			WaveController.setWidth ("two", percent);
		}
			
	}

// PLAYER STATE FUNCTIONS
	public void IncreaseFrequencyStateP1() {
		// If previous animation is done
		if (Time.fixedTime > nextPlayerOneStepTime) {
			PlayerOneStartFrequency = PlayerOneEndFrequency;
			PlayerOneEndFrequency += FREQUENCY_STEP;
			nextPlayerOneStepTime = Time.fixedTime + STEP_SECONDS;
			PlayerOneEndFrequency = Mathf.Min (PlayerOneEndFrequency, 1f);
		}
	}

	public void DecreaseFrequencyStateP1() {
		// If previous animation is done
		if (Time.fixedTime > nextPlayerOneStepTime) {
			PlayerOneStartFrequency = PlayerOneEndFrequency;
			PlayerOneEndFrequency -= FREQUENCY_STEP;
			nextPlayerOneStepTime = Time.fixedTime + STEP_SECONDS;
			PlayerOneEndFrequency = Mathf.Max (PlayerOneEndFrequency, 0f);
		}
	}

	public void IncreaseFrequencyStateP2() {
		if (Time.fixedTime > nextPlayerTwoStepTime) {
			PlayerTwoStartFrequency = PlayerTwoEndFrequency;
			PlayerTwoEndFrequency += FREQUENCY_STEP;
			PlayerTwoEndFrequency = Mathf.Min (PlayerTwoEndFrequency, 1f);
			nextPlayerTwoStepTime = Time.fixedTime + STEP_SECONDS;
		}
	}

	public void DecreaseFrequencyStateP2() {
		if (Time.fixedTime > nextPlayerTwoStepTime) {
			PlayerTwoStartFrequency = PlayerTwoEndFrequency;
			PlayerTwoEndFrequency -= FREQUENCY_STEP;
			PlayerTwoEndFrequency = Mathf.Max (PlayerTwoEndFrequency, 0f);
			nextPlayerTwoStepTime = Time.fixedTime + STEP_SECONDS;
		}
	}

// CHANGE GAME STATE
	public void newGame() {
		// Reset Player Variables
		PlayerOneHealth = DEFAULT_START_HEALTH;
		PlayerTwoHealth = DEFAULT_START_HEALTH;
		PlayerOneEnergy = DEFAULT_START_ENERGY;
		PlayerTwoEnergy = DEFAULT_START_ENERGY;
		PlayerOneStartFrequency = DEFAULT_START_FREQUENCY;
		PlayerOneEndFrequency = DEFAULT_START_FREQUENCY;
		PlayerTwoStartFrequency = DEFAULT_START_FREQUENCY;
		PlayerTwoEndFrequency = DEFAULT_START_FREQUENCY;
		WaveController.setFrequencies (DEFAULT_START_FREQUENCY, DEFAULT_START_FREQUENCY);
		respawnBomb ();
	}

	public void respawnBomb() {
		GameObject bomb = GameObject.FindGameObjectWithTag ("BOMB");
		if (bomb != null) {
			bomb.transform.position = new Vector3 (0f, 5.5f, -2f);
			bomb.transform.rotation = new Quaternion ();

			Rigidbody2D bombPhysics = bomb.GetComponent<Rigidbody2D> ();
			bombPhysics.velocity = new Vector2 ();
			bombPhysics.angularVelocity = 0f;
		}
	}
}
