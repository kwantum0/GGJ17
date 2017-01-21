using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	private LineRenderer line;
	private List<GameObject> colliders;
	private List<Vector3> points;
	private Vector3 start;
	private Vector3 end;
	private float totalDistance;
	private float radianDistance;
	private float segmentDistance;
	private float strokeWidth;

	[Range(-0.5f,0.5f)]
	public float colliderOffset = 0.1f;
	[Range(2f,10f)]
	public float frequencyLeft = 2f;
	[Range(2f,10f)]
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
		line = GetComponent<LineRenderer> ();
		strokeWidth = line.startWidth;
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
		for (int i = 1; i < points.Count-1; i++) {
			Vector3 point = points[i];
			point.y = amplitude * (CalcRightWave(point.x * radianDistance) + CalcLeftWave(point.x * radianDistance));
			points [i] = point;
		}
		line.SetPositions (points.ToArray ());




		// UPDATE THE BOX COLLIDERS - COMPLETE
		for (int i = 1; i < points.Count; i++) {
			BoxCollider2D current = colliders[i - 1].GetComponent<BoxCollider2D>();
			Vector3 point1 = points [i - 1];
			Vector3 point2 = points [i];
			current.size = new Vector2 (Vector3.Distance(point1, point2) + colliderOffset, strokeWidth + colliderOffset);
			current.transform.position = Vector3.Lerp (point1, point2, 0.5f);
			Vector3 direction = point1 - point2;
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			current.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	// ALL THE MATH!
	private float CalcRightWave(float x){
		return Mathf.Sin(frequencyRight*(x - Mathf.PI) + Time.fixedTime * speed);
	}
	private float CalcLeftWave(float x){
		return Mathf.Sin(frequencyLeft*(x + Mathf.PI) - Time.fixedTime * speed);
	}
		
}
