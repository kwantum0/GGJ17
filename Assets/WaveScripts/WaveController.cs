using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	private static WaveController _instance;

	public const float MAX_FREQUENCY = 12f;
	public const float MIN_FREQUENCY = 2f;
	public const float WIDTH_MIN = 0.15f;
	public const float WIDTH_MAX = 0.75f;

	private LineRenderer line;
	private List<GameObject> colliders;
	private List<Vector3> points;
	private Vector3 start;
	private Vector3 end;
	private float totalDistance;
	private float radianDistance;
	private float segmentDistance;
	private float strokeWidth;

	public AnimationCurve alwaysBevel;

	[Range(-0.5f,0.5f)]
	public float colliderOffset = 0.1f;
	[Range(2f,12f)]
	public float frequencyLeft = 2f;
	[Range(2f,12f)]
	public float frequencyRight = 2f;
	[Range(0f,4f)]
	public float amplitude = 1;
	public GameObject startPosition;
	public GameObject endPosition;
	[Range(0f,10f)]
	public float speed;
	[Range(0,400)]
	public int segments;

	// Use this for initialization
	void Start () {
		if (_instance == null) {
			_instance = this;
		}

		line = GetComponent<LineRenderer> ();
		strokeWidth = line.startWidth;
		resetWidths ();
		colliders = new List<GameObject>();
		points = new List<Vector3> ();
		start = new Vector3 (startPosition.transform.position.x, startPosition.transform.position.y, 0);
		end = new Vector3 (endPosition.transform.position.x, endPosition.transform.position.y, 0);
		totalDistance = Vector3.Distance (start, end);
		segmentDistance = totalDistance / segments;
		radianDistance = (2 * Mathf.PI) / totalDistance;

		// CREATE THE LINE
		points.Add (start);
		Vector3 cpoint = start;
		for (int i = 1; i < segments; i++) {
			cpoint = Vector3.MoveTowards(cpoint, end, segmentDistance);
			points.Add (cpoint);
		}
		points.Add (end);
		line.numPositions = points.Count;
		line.SetPositions(points.ToArray());

		// CREATE THE BOX COLLIDERS
		for (int i = 1; i < points.Count; i++) {
			GameObject newObject = new GameObject ("collider" + i.ToString());
			newObject.AddComponent<BoxCollider2D> ();
			colliders.Add (newObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		// UPDATE THE LINE
		for (int i = 0; i < points.Count; i++) {
			float bevel = alwaysBevel.Evaluate ((float)i / (float)points.Count);
			Vector3 point = points[i];
			point.y = amplitude * bevel * (CalcRightWave(point.x * radianDistance) + CalcLeftWave(point.x * radianDistance));
			points [i] = point;
		}
		line.SetPositions (points.ToArray ());




		// UPDATE THE BOX COLLIDERS - COMPLETE
		for (int i = 1; i < points.Count; i++) {
			float StrokePercent = line.widthCurve.Evaluate((float)i / (float)points.Count);
			BoxCollider2D current = colliders[i - 1].GetComponent<BoxCollider2D>();
			Vector3 point1 = points [i - 1];
			Vector3 point2 = points [i];
			current.size = new Vector2 (Vector3.Distance(point1, point2) + colliderOffset, StrokePercent + colliderOffset);
			current.transform.position = Vector3.Lerp (point1, point2, 0.5f);
			Vector3 direction = point1 - point2;
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			current.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	public static void setFrequencies(float player1, float player2){
		if (_instance != null) {
			_instance.frequencyLeft = Mathf.Lerp (MIN_FREQUENCY, MAX_FREQUENCY, player1);
			_instance.frequencyRight = Mathf.Lerp (MIN_FREQUENCY, MAX_FREQUENCY, player2);
		}
	}
	public static void setFrequencies(string i, float player){
		if (_instance != null) {
			if (i == "one") {
				_instance.frequencyLeft = Mathf.Lerp (MIN_FREQUENCY, MAX_FREQUENCY, player);
			} else {
				_instance.frequencyRight = Mathf.Lerp (MIN_FREQUENCY, MAX_FREQUENCY, player);
			}
		}
	}

	public void resetWidths(){
		List<Keyframe> keys = new List<Keyframe> (4);
		keys.Insert(0, new Keyframe (0f, WIDTH_MIN, 0f, 0f));
		keys.Insert(1, new Keyframe (0.15f, WIDTH_MIN, 0f, 0f));
		keys.Insert(2, new Keyframe (0.85f, WIDTH_MIN, 0f, 0f));
		keys.Insert(3, new Keyframe (1f, WIDTH_MIN, 0f, 0f));
		line.widthCurve = new AnimationCurve (keys.ToArray());
	}

	public static void setWidth(string i, float percent){
		if (_instance != null) {
			AnimationCurve current = _instance.line.widthCurve;
			List<Keyframe> newKeys = new List<Keyframe> (current.keys.Length);
			float value = Mathf.Lerp (WIDTH_MIN, WIDTH_MAX, percent);
			int index = (i == "one" ? 0 : 3);

			for(int j = 0; j < current.keys.Length; j++){
				if(index == j){
					newKeys.Insert(j, new Keyframe(current.keys[j].time, value));
				} else {
					newKeys.Insert(j, current.keys[j]);
				}
			}

			// current = new Keyframe(current.keys[index].time, Mathf.Lerp (WIDTH_MIN, WIDTH_MAX, percent));
			//_instance.line.widthCurve.SmoothTangents (0, 1);

			_instance.line.widthCurve = new AnimationCurve(newKeys.ToArray());
			_instance.line.widthCurve.SmoothTangents (0, 0.5f);

		}

	}

	// ALL THE MATH!
	private float CalcRightWave(float x){
		return Mathf.Sin(frequencyRight*(x + Mathf.PI) + Time.fixedTime * speed);
	}
	private float CalcLeftWave(float x){
		return Mathf.Sin(frequencyLeft*(x - Mathf.PI) - Time.fixedTime * speed);
	}
		
}
