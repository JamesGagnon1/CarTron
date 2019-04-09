using System.Collections;
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
	public float TrailTime;
	public bool P1;
	float HorInput;
	float VerInput;
	float time;
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
		time = TrailTime;
	}
	
	void Update () {
		time += Time.deltaTime;
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Body")) {
			if (G.name.Contains(gameObject.tag)) {
				TrailColor = GameObject.Find (G.name + "/chassis").GetComponent<Renderer>().material.GetColor("_Color");
				TR.startColor = TrailColor;
				TR.endColor = TrailColor;
			}
		}
		if (P1) {
			HorInput = Input.GetAxisRaw("A-D");
			VerInput = Input.GetAxisRaw("W-S");
		} else {
			HorInput = Input.GetAxisRaw("<->");
			VerInput = Input.GetAxisRaw("^-V");
		}
		
		Vector3 LocVel = transform.InverseTransformDirection(RB.velocity);
		RB.angularVelocity = new Vector3 (RB.angularVelocity.x, TurnSpeed * HorInput * 3, RB.angularVelocity.z);
		foreach (WheelCollider Wheel in GetComponentsInChildren<WheelCollider>()) {
			if (VerInput == 0) {
				if (LocVel.z < 15) {
					Wheel.motorTorque = Speed;
				} else {
					LocVel.z = 15;
				}
			} else {
				if (LocVel.z < 30) {
					Wheel.motorTorque = Speed * 2;
				} else {
					LocVel.z = 30;
				}
			}
		}
		if (Mathf.Abs(LocVel.x) > 0) {
			LocVel.x = LocVel.x / BrakePower;
			RB.velocity = transform.TransformDirection(LocVel);
		}
		if (time >= TrailTime) {
			time = 0;
			Instantiate(Trail, transform.position + transform.forward * -0.5f,transform.rotation);
		}
	}
	void OnCollisionEnter (Collision Hit) {
		if (Hit.gameObject.tag == "Death" || Hit.gameObject.tag == "P1" || Hit.gameObject.tag == "P2") {
			Instantiate (Explosion, transform.position, transform.rotation);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			if (Hit.gameObject.tag == "Death") {
				GameObject OtherPlayer;
				if (P1) {
					OtherPlayer = GameObject.Find("Car P2");
					OtherPlayer.GetComponent<Rigidbody>().isKinematic = true;
					GameObject C = GameObject.Find("Main Camera " + OtherPlayer.tag);
					C.tag = "Win Camera";
					foreach (Renderer MR in OtherPlayer.GetComponentsInChildren<MeshRenderer>()) {
						MR.enabled = false;
					}
					foreach (ParticleSystem PS in OtherPlayer.GetComponentsInChildren<ParticleSystem>()) {
						PS.enableEmission = false;
					}
					P2Wins.SetActive(true);
				} else {
					OtherPlayer	= GameObject.Find("Car P1");
					OtherPlayer.GetComponent<Rigidbody>().isKinematic = true;
					GameObject C = GameObject.Find("Main Camera " + OtherPlayer.tag);
					C.tag = "Win Camera";
					foreach (Renderer MR in OtherPlayer.GetComponentsInChildren<MeshRenderer>()) {
						MR.enabled = false;
					}
					foreach (ParticleSystem PS in OtherPlayer.GetComponentsInChildren<ParticleSystem>()) {
						PS.enableEmission = false;
					}
					P1Wins.SetActive(true);
				}
			} else {
				Draw.SetActive(true);
				GameObject C = GameObject.Find("Main Camera P1");
				C.tag = "Win Camera";
			}
			EndScreen.SetActive(true);
			foreach (GameObject G in GameObject.FindGameObjectsWithTag("Death")) {
				if (G.name == "Death Trail(Clone)") {
					Destroy(G);
				}
			}
			RB.isKinematic = true;
			foreach (Renderer MR in GetComponentsInChildren<MeshRenderer>()) {
				MR.enabled = false;
			}
			foreach (ParticleSystem PS in GetComponentsInChildren<ParticleSystem>()) {
				PS.enableEmission = false;
			}
		}
	}
}