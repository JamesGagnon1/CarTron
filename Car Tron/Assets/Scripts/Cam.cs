using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	public Transform Car;
	public Vector3 Offset;
	Vector3 TargetPos;
	void Start () {
		
	}
	
	void Update () {
		transform.position = Vector3.Lerp (transform.position, new Vector3 (Car.position.x, Car.position.y + Offset.y, Car.position.z) + Car.forward * Offset.z, Time.deltaTime * 5);
		transform.LookAt (Car.position + Car.transform.forward * 0.5f);
	}
}