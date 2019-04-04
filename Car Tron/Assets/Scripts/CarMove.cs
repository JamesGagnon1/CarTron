﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

	public GameObject Trail;
	public GameObject P1Wins;
	public GameObject P2Wins;
	public GameObject Draw;
	public GameObject EndScreen;
	public ParticleSystem Explosion;
	public Vector3 StartPoint;
	public float Speed;
	public float TurnSpeed;
	public float BrakePower;
	public float first;
	public float second;
	public float TrailTime;
	public bool P1;
	float BtnInput;
	float trail_time;
	Rigidbody RB;
	public TrailRenderer TR;
	Color TrailColor;

	void Start () {
		RB = GetComponent <Rigidbody>();
		RB.isKinematic = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		RB.centerOfMass = new Vector3(0, -0.5f, 0.5f);
		transform.position = StartPoint;
		if (P1) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}
		foreach (WheelCollider G in GetComponentsInChildren<WheelCollider>()) {
			G.enabled = true;
		}
		RB.useGravity = true;
		
		TR.enabled = true;
		trail_time = TrailTime;
		GetComponent<BoxCollider>().enabled = true;
	}
	
	void Update () {
		trail_time += Time.deltaTime;
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Body")) {
			if (G.name.Contains(gameObject.tag)) {
				TrailColor = GameObject.Find (G.name + "/chassis").GetComponent<Renderer>().material.GetColor("_Color");
				TR.startColor = TrailColor;
				TR.endColor = TrailColor;
			}
		}
		if (P1) {
			BtnInput = Input.GetAxisRaw("A-D");
		} else {
			BtnInput = Input.GetAxisRaw("<->");
		}
		
		Vector3 LocVel = transform.InverseTransformDirection(RB.velocity);
		RB.AddTorque(transform.up * TurnSpeed * BtnInput * Time.deltaTime * 50);
		foreach (WheelCollider Wheel in GetComponentsInChildren<WheelCollider>()) {
			Wheel.radius += Mathf.Sin (Time.time * first) * Time.deltaTime * second;
			if (LocVel.z < 15) {
				Wheel.motorTorque = Speed;
			} else {
				LocVel.z = 15;
			}
			WheelHit hit;
			if (Wheel.GetGroundHit(out hit)) {
				if (BtnInput == 0) {
					RB.angularVelocity = new Vector3 (RB.angularVelocity.x, 0, RB.angularVelocity.z);
				}
				if (Mathf.Abs(LocVel.x) > 0) {
					LocVel.x = LocVel.x / BrakePower;
					RB.velocity = transform.TransformDirection(LocVel);
				}
			}
		}
		if (trail_time >= TrailTime) {
			trail_time = 0;
			Instantiate(Trail, transform.position + transform.forward * -0.5f,transform.rotation);
		}
    }
	void OnCollisionEnter (Collision Hit) {
		if (Hit.gameObject.tag == "Death" || Hit.gameObject.tag == "P1" || Hit.gameObject.tag == "P2") {
			Instantiate (Explosion, transform.position, transform.rotation);
			//Cursor.visible = true;
			//Cursor.lockState = CursorLockMode.None;
			if (Hit.gameObject.tag == "Death") {
				if (P1) {
					Destroy(GameObject.Find("Car P2"));
					P2Wins.SetActive(true);
				} else {
					Destroy(GameObject.Find("Car P1"));
					P1Wins.SetActive(true);
				}
			} else {
				Draw.SetActive(true);
			}
			EndScreen.SetActive(true);
			foreach (GameObject G in GameObject.FindGameObjectsWithTag("Death")) {
				if (G.name == "Death Trail(Clone)") {
					Destroy(G);
				}
			}
			Destroy(gameObject);
		}
	}
}