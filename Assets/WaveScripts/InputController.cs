using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	// KEY MAPPINGS
	public KeyCode[] PlayerOneGuitar;
	public KeyCode PlayerOneAttack;
	public KeyCode[] PlayerTwoGuitar;
	public KeyCode PlayerTwoAttack;

	public string RandomSeed;
	public int NoiseSize = 100;
	public NoteType[] Notes;

	private float[] PlayerOneNoise;
	private float[] PlayerTwoNoise;

	public int[] PlayerOneSequence;
	public int[] PlayerTwoSequence;

	// Use this for initialization
	void Awake () {
		generateRandomNoise ();
		generateSequences();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void generateRandomNoise(){
		Random.seed = RandomSeed.GetHashCode ();
		PlayerOneNoise = new float[NoiseSize];
		PlayerTwoNoise = new float[NoiseSize];
		int i = 0;
		while (i < NoiseSize) {
			PlayerOneNoise[i] = Random.value;
			PlayerTwoNoise[i] = Random.value;
			i++;
		}
	}

	void generateSequences() {
		int i = 0;
		while (i < NoiseSize) {
			PlayerOneNoise[i]
			i++;
		}
	}

	[System.Serializable]
	public struct NoteType {
		public int id;
		public Color color;
		[Range(0f,1f)]
		public float minNoiseIncl;
		[Range(0f,1f)]
		public float maxNoiseExcl;
	}
}
