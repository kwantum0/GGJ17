using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Range(0,6)]
	public const int DEFAULT_START_HEALTH = 6;
	[Range(0,9)]
	public const int DEFAULT_START_ENERGY = 0;

	public int PlayerOneHealth;
	public int PlayerTwoHealth;
	public int PlayerOneEnergy;
	public int PlayerTwoEnergy;


	// Use this for initialization
	void Start () {
		newGame ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	void newGame() {
		// Reset Player Variables
		PlayerOneHealth = DEFAULT_START_HEALTH;
		PlayerTwoHealth = DEFAULT_START_HEALTH;
		PlayerOneEnergy = DEFAULT_START_ENERGY;
		PlayerTwoEnergy = DEFAULT_START_ENERGY;



	}
}
