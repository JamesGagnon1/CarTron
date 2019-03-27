using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

	public float Speed;
	public float BrakePower;
	public float first;
	public float second;
	public GameObject Menu;
	Rigidbody RB;

	void Start () {
		RB = GetComponent <Rigidbody>();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		RB.centerOfMass = new Vector3(0, -0.5f, 0.5f);
		transform.position = new Vector3 (0, 1f, 0);
		transform.eulerAngles = new Vector3 (0, 0, 0);
		foreach (WheelCollider G in GetComponentsInChildren<WheelCollider>()) {
			G.enabled = true;
		}
		RB.useGravity = true;
	}
	
	void Update () {
		Vector3 LocVel = transform.InverseTransformDirection(RB.velocity);
		transform.RotateAround(transform.position, transform.up * Input.GetAxisRaw("Horizontal"), Time.deltaTime * 75);
		foreach (WheelCollider Wheel in GetComponentsInChildren<WheelCollider>()) {
			Wheel.radius += Mathf.Sin (Time.time * first) * Time.deltaTime * second;
			if (Input.GetAxisRaw("Vertical") > 0) {
				if (LocVel.z < 15) {
					Wheel.motorTorque = Input.GetAxisRaw("Vertical") * Speed;
				} else {
					LocVel.z = 15;
				}
			} else {
				if (LocVel.z > -5) {
					Wheel.motorTorque = Input.GetAxisRaw("Vertical") * (Speed / 4);
				} else {
					LocVel.z = -5;
				}
			}
			WheelHit hit;
			if (Wheel.GetGroundHit(out hit)) {
				if (Input.GetAxisRaw("Vertical") == 0) {
					RB.velocity -= new Vector3 (RB.velocity.x / BrakePower, 0, RB.velocity.z / BrakePower);
				} else {
					if (Mathf.Abs(LocVel.x) > 0) {
						LocVel.x = LocVel.x / BrakePower;
						RB.velocity = transform.TransformDirection(LocVel);
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			Menu.SetActive (!Menu.active);
		}
	}
}