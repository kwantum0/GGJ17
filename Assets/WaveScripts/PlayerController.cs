using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Range(0,6)]
	public const int DEFAULT_START_HEALTH = 6;
	[Range(0,9)]
	public const int DEFAULT_START_ENERGY = 0;
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

		if (Input.GetKeyDown (KeyCode.A) && Time.fixedTime > nextPlayerOneStepTime) {
			PlayerOneStartFrequency = PlayerOneEndFrequency;
			PlayerOneEndFrequency += FREQUENCY_STEP;

			if(PlayerOneEndFrequency < 1f){
				nextPlayerOneStepTime = Time.fixedTime + STEP_SECONDS;
			}

			PlayerOneEndFrequency = Mathf.Min (PlayerOneEndFrequency, 1f);

		} else if (Input.GetKeyDown (KeyCode.S) && Time.fixedTime > nextPlayerOneStepTime) {
			PlayerOneStartFrequency = PlayerOneEndFrequency;
			PlayerOneEndFrequency -= FREQUENCY_STEP;

			if(PlayerOneEndFrequency > 0f){
				nextPlayerOneStepTime = Time.fixedTime + STEP_SECONDS;
			}

			PlayerOneEndFrequency = Mathf.Max (PlayerOneEndFrequency, 0f);
		}

		if (Time.fixedTime < nextPlayerOneStepTime) {
			float percent = (nextPlayerOneStepTime - Time.fixedTime) / STEP_SECONDS;
			float value = Mathf.Lerp (PlayerOneEndFrequency, PlayerOneStartFrequency, percent);
			WaveController.setFrequencies ("one", value);
			WaveController.setWidth ("one", percent);
		}

		if (Input.GetKeyDown (KeyCode.L) && Time.fixedTime > nextPlayerTwoStepTime) {
			PlayerTwoStartFrequency = PlayerTwoEndFrequency;
			PlayerTwoEndFrequency += FREQUENCY_STEP;

			if(PlayerTwoEndFrequency < 1f){
				nextPlayerTwoStepTime = Time.fixedTime + STEP_SECONDS;
			}

			PlayerTwoEndFrequency = Mathf.Min (PlayerTwoEndFrequency, 1f);

		} else if (Input.GetKeyDown(KeyCode.Semicolon) && Time.fixedTime > nextPlayerTwoStepTime) {
			PlayerTwoStartFrequency = PlayerTwoEndFrequency;
			PlayerTwoEndFrequency -= FREQUENCY_STEP;

			if(PlayerTwoEndFrequency > 0f){
				nextPlayerTwoStepTime = Time.fixedTime + STEP_SECONDS;
			}

			PlayerTwoEndFrequency = Mathf.Max (PlayerTwoEndFrequency, 0f);
		}

		if (Time.fixedTime < nextPlayerTwoStepTime) {
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
		PlayerOneStartFrequency = 0f;
		PlayerOneEndFrequency = 0f;
		PlayerTwoStartFrequency = 0f;
		PlayerTwoEndFrequency = 0f;
		WaveController.setFrequencies (0f, 0f);
	}
}
