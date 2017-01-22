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
	public const float FREQUENCY_STEP = 0.2f;
	[Range(0f,3f)]
	public const float STEP_SECONDS = 1f;
	private float nextPlayerOneStepTime = -1f;
	private float nextPlayerTwoStepTime = -1f;


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

		// PLAYER ONE
		if (Input.GetKeyDown (KeyCode.A) && Time.fixedTime > nextPlayerOneStepTime) {
			PlayerOneStartFrequency = PlayerOneEndFrequency;
			PlayerOneEndFrequency += FREQUENCY_STEP;
			nextPlayerOneStepTime = Time.fixedTime + STEP_SECONDS;
			PlayerOneEndFrequency = Mathf.Min (PlayerOneEndFrequency, 1f);

		} else if (Input.GetKeyDown (KeyCode.S) && Time.fixedTime > nextPlayerOneStepTime) {
			PlayerOneStartFrequency = PlayerOneEndFrequency;
			PlayerOneEndFrequency -= FREQUENCY_STEP;
			nextPlayerOneStepTime = Time.fixedTime + STEP_SECONDS;
			PlayerOneEndFrequency = Mathf.Max (PlayerOneEndFrequency, 0f);

		}

		if (Time.fixedTime < nextPlayerOneStepTime  && PlayerOneStartFrequency != PlayerOneEndFrequency) {
			float percent = (nextPlayerOneStepTime - Time.fixedTime) / STEP_SECONDS;
			float value = Mathf.Lerp (PlayerOneEndFrequency, PlayerOneStartFrequency, percent);
			WaveController.setFrequencies ("one", value);
			WaveController.setWidth ("one", percent);
		}

		// PLAYER TWO
		if (Input.GetKeyDown (KeyCode.L) && Time.fixedTime > nextPlayerTwoStepTime) {
			PlayerTwoStartFrequency = PlayerTwoEndFrequency;
			PlayerTwoEndFrequency += FREQUENCY_STEP;
			PlayerTwoEndFrequency = Mathf.Min (PlayerTwoEndFrequency, 1f);
			nextPlayerTwoStepTime = Time.fixedTime + STEP_SECONDS;
		} else if (Input.GetKeyDown(KeyCode.Semicolon) && Time.fixedTime > nextPlayerTwoStepTime) {
			PlayerTwoStartFrequency = PlayerTwoEndFrequency;
			PlayerTwoEndFrequency -= FREQUENCY_STEP;
			PlayerTwoEndFrequency = Mathf.Max (PlayerTwoEndFrequency, 0f);
			nextPlayerTwoStepTime = Time.fixedTime + STEP_SECONDS;
		}

		if (Time.fixedTime < nextPlayerTwoStepTime && PlayerTwoStartFrequency != PlayerTwoEndFrequency) {
			float percent = (nextPlayerTwoStepTime - Time.fixedTime) / STEP_SECONDS;
			float value = Mathf.Lerp (PlayerTwoEndFrequency, PlayerTwoStartFrequency, percent);
			WaveController.setFrequencies ("two", value);
			WaveController.setWidth ("two", percent);
		}
			
	}

	void newGame() {
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
	}
}
