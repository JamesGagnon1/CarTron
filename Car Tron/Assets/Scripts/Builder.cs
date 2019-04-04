using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class CarParts {
	public string PartType;
	public GameObject[] CarPrefabs;
}
public class Builder : MonoBehaviour {

	public CarParts[] PartPrefabs;
	int TypePick;
	public static int[] PartsP1 = {0, 0, 0, 0, 0};
	public static int[] PartsP2 = {0, 0, 0, 0, 0};
	int[] Parts;
	public GameObject BuildCanvas;
	public GameObject Dot;
	public bool P1;
    public GameObject PauseCanvas;

	void Start () {
		for (int i = 0; i < PartPrefabs.Length; i++) {
			foreach (GameObject G in PartPrefabs[i].CarPrefabs) {
				GameObject NewG = Instantiate(G);
				NewG.name = G.name + gameObject.tag;
			}
			
		}
		TypePick = 0;
		print (transform.position);
		print (transform.rotation);
		GetComponent<Rigidbody>().isKinematic = true;
		if (P1) {
			Parts = PartsP1;
		} else {
			Parts = PartsP2;
		}
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        PauseCanvas.SetActive(false);
	}
	

	void Update () {
		if ((P1 && Input.GetKeyDown(KeyCode.W)) || (!P1 && Input.GetKeyDown(KeyCode.UpArrow)) ) {
			TypePick --;
			Dot.transform.localPosition += new Vector3 (0, 75, 0);
			if (TypePick < 0) {
				TypePick = 4;
				Dot.transform.localPosition = new Vector3 (Dot.transform.localPosition.x, -480, 0);
			}
		}

		if ((P1 && Input.GetKeyDown(KeyCode.S)) || (!P1 && Input.GetKeyDown(KeyCode.DownArrow)) ) {
			TypePick ++; 
			Dot.transform.localPosition += new Vector3 (0, -75, 0);
			if (TypePick > 4) {
				TypePick -= 5;
				Dot.transform.localPosition = new Vector3 (Dot.transform.localPosition.x, -180, 0);
			}
		}

		if ((P1 && Input.GetKeyDown(KeyCode.D)) || (!P1 && Input.GetKeyDown(KeyCode.RightArrow)) ) {
			Parts[TypePick]++;
			if (Parts[TypePick] + 1 > PartPrefabs [TypePick].CarPrefabs.Length) {
				Parts[TypePick] -= PartPrefabs [TypePick].CarPrefabs.Length;
			}
		}
		
		if ((P1 && Input.GetKeyDown(KeyCode.A)) || (!P1 && Input.GetKeyDown(KeyCode.LeftArrow)) ) {
			Parts[TypePick]--;
			if (Parts[TypePick] < 0) {
				Parts[TypePick] = PartPrefabs [TypePick].CarPrefabs.Length - 1;
			}
		}

		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Body")) {
			if (G.name.Contains(gameObject.tag)) {
				if (G.name == "Body (" + Parts[0] + ")" + gameObject.tag) {
					G.transform.position = transform.position;
					G.transform.rotation = transform.rotation;
					G.transform.SetParent(transform);
				} else {
					G.transform.SetParent(null);
					G.transform.position = new Vector3 (0, -100, 0);
				}
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Hat")) {
			if (G.name.Contains(gameObject.tag)) {
				if (G.name == "Hat (" + Parts[1] + ")" + gameObject.tag) {
					G.transform.position = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag + "/HatPos").transform.position;
					G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag + "/HatPos").transform.rotation;
					G.transform.SetParent(transform);
				} else {
					G.transform.SetParent(null);
					G.transform.position = new Vector3 (0, -100, 0);
				}
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Nose")) {
			if (G.name.Contains(gameObject.tag)) {
				if (G.name == "Nose (" + Parts[2] + ")" + gameObject.tag) {
					G.transform.position = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag + "/NosePos").transform.position;
					G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag + "/NosePos").transform.rotation;
					G.transform.SetParent(transform);
				} else {
					G.transform.SetParent(null);
					G.transform.position = new Vector3 (0, -100, 0);
				}
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Wing")) {
			if (G.name.Contains(gameObject.tag)) {
				if (G.name == "Wing (" + Parts[3] + ")" + gameObject.tag) {
					G.transform.position = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag).transform.position;
					G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag).transform.rotation;
					G.transform.SetParent(transform);
				} else {
					G.transform.SetParent(null);
					G.transform.position = new Vector3 (0, -100, 0);
				}
			}
		}
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("Engine")) {
			if (G.name.Contains(gameObject.tag)) {
				if (G.name == "Engine (" + Parts[4] + ")" + gameObject.tag) {
					G.transform.position = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag).transform.position;
					G.transform.rotation = GameObject.Find("Body (" + Parts[0] + ")" + gameObject.tag).transform.rotation;
					G.transform.SetParent(transform);
				} else {
					G.transform.SetParent(null);
					G.transform.position = new Vector3 (0, -100, 0);
				}
			}
		}
	
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50);
		transform.position = new Vector3(transform.position.x, Mathf.Sin (Time.time * 3) * 0.25f + 1, transform.position.z);
		if (P1) {
			PartsP1 = Parts;
		} else {
			PartsP2 = Parts;
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
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
			GameObject C = GameObject.Find("Car P2");
			C.GetComponent<CarMove>().enabled = true;
			GetComponent<CarMove>().enabled = true;
			C.GetComponent<Builder>().enabled = false;
			MenuSong.SetActive(false);
			GameSong.SetActive(true);
			GetComponent<Builder>().enabled = false;
		}
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
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("MainCamera")) {
			G.GetComponent<Cam>().enabled = true;
		}
		GameObject C = GameObject.Find("Car P2");
		C.GetComponent<CarMove>().enabled = true;
		GetComponent<CarMove>().enabled = true;
		C.GetComponent<Builder>().enabled = false;
		GetComponent<Builder>().enabled = false;

        PauseCanvas.SetActive(true);
	}
}