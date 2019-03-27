using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	int TypePick;
	int[] Parts = {0, 0, 0, 0, 0};
	int[] PartShow = {0, 0, 0, 0, 0};
	public int[] PartLength;
	public GameObject BuildCanvas;
	public GameObject GameCanvas;

	void Start () {
		TypePick = 0;
	}
	

	void Update () {
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Body")) {
			if (G.name == "Body (" + Parts[0] + ")") {
				G.transform.position = transform.position;
				G.transform.rotation = transform.rotation;
				G.transform.SetParent(transform);
			} else {
				G.transform.SetParent(null);
				G.transform.position = new Vector3 (0, -100, 0);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Hat")) {
			if (G.name == "Hat (" + Parts[1] + ")") {
				G.transform.position = GameObject.Find("Body (" + Parts[0] + ")/HatPos").transform.position;
				G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")/HatPos").transform.rotation;
				G.transform.SetParent(transform);
			} else {
				G.transform.SetParent(null);
				G.transform.position = new Vector3 (0, -100, 0);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Nose")) {
			if (G.name == "Nose (" + Parts[2] + ")") {
				G.transform.position = GameObject.Find("Body (" + Parts[0] + ")/NosePos").transform.position;
				G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")/NosePos").transform.rotation;
				G.transform.SetParent(transform);
			} else {
				G.transform.SetParent(null);
				G.transform.position = new Vector3 (0, -100, 0);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Wing")) {
			if (G.name == "Wing (" + Parts[3] + ")") {
				G.transform.position = GameObject.Find("Body (" + Parts[0] + ")").transform.position;
				G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")").transform.rotation;
				G.transform.SetParent(transform);
			} else {
				G.transform.SetParent(null);
				G.transform.position = new Vector3 (0, -100, 0);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Engine")) {
			if (G.name == "Engine (" + Parts[4] + ")") {
				G.transform.position = GameObject.Find("Body (" + Parts[0] + ")").transform.position;
				G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")").transform.rotation;
				G.transform.SetParent(transform);
			} else {
				G.transform.SetParent(null);
				G.transform.position = new Vector3 (0, -100, 0);
			}
		}
	
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50);
		transform.position += transform.up * Mathf.Sin (Time.time * 3) * Time.deltaTime ;
	}
	public void BodyBtn() {
		TypePick = 0;
	}
	public void HatBtn() {
		TypePick = 1;
	}
	public void NoseBtn() {
		TypePick = 2;
	}
	public void WingBtn() {
		TypePick = 3;
	}
	public void EngineBtn() {
		TypePick = 4;
	}
	public void Right() {
		PartShow[TypePick]++;
		if (PartShow[TypePick] + 1 > PartLength[TypePick]) {
			PartShow[TypePick] -= PartLength[TypePick];
		}
		Parts[TypePick] = PartShow[TypePick];
	}
	public void Left() {
		PartShow[TypePick]--;
		if (PartShow[TypePick] < 0) {
			PartShow[TypePick] = PartLength[TypePick] - 1;
		}
		Parts[TypePick] = PartShow[TypePick];
	}

	public void StartBtn() {
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Body")) {
			if (G.transform.parent == null) {
				Destroy(G);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Hat")) {
			if (G.transform.parent == null) {
				Destroy(G);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Nose")) {
			if (G.transform.parent == null) {
				Destroy(G);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Wing")) {
			if (G.transform.parent == null) {
				Destroy(G);
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Engine")) {
			if (G.transform.parent == null) {
				Destroy(G);
			}
		}
		BuildCanvas.SetActive(false);
		GameCanvas.SetActive(true);
		GetComponent<CarMove>().enabled = true;
		GameObject.Find("Main Camera").GetComponent<Cam>().enabled = true;
		GetComponent<Builder>().enabled = false;
	}
}