using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	private LineRenderer line;
	private List<Vector3> points;
	private MeshCollider collider;
	private Vector3 start;
	private Vector3 end;
	private float totalDistance;
	private float radianDistance;
	private float segmentDistance;

	public float frequencyLeft = 1f;
	public float frequencyRight = 1f;
	public GameObject startPosition;
	public GameObject endPosition;
	public float speed;
	public int segments;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
		collider = GetComponent<MeshCollider> ();
		points = new List<Vector3> ();
		start = new Vector3 (startPosition.transform.position.x, 0, startPosition.transform.position.z);
		end = new Vector3 (endPosition.transform.position.x, 0, endPosition.transform.position.z);
		totalDistance = Vector3.Distance (start, end);
		segmentDistance = totalDistance / segments;
		radianDistance = (2 * Mathf.PI) / totalDistance;
			
		points.Add (start);
		Vector3 cpoint = start;
		for (int i = 1; i < segments; i++) {
			cpoint = Vector3.MoveTowards(cpoint, end, segmentDistance);
			points.Add (cpoint);
		}
		points.Add (end);
		line.numPositions = points.Count;
		line.SetPositions(points.ToArray());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 0; i < points.Count; i++) {
			Vector3 point = points[i];
			point.z = Mathf.Sin(frequencyLeft*(Time.fixedTime * speed + radianDistance * point.x));
			points [i] = point;
		}
		line.SetPositions (points.ToArray ());
	}
}
