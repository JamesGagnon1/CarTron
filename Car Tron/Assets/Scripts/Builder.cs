using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class CarParts {
	public string PartType;
	public GameObject[] Parts;
}
public class Builder : MonoBehaviour {

	public CarParts[] CarPrefabs;
	int TypePick;
	int[] Parts = {0, 0, 0, 0, 0};
	int[] PartShow = {0, 0, 0, 0, 0};
	public GameObject BuildCanvas;
	public GameObject Dot;
	public bool P1;

	void Start () {
		for (int i = 0; i < CarPrefabs.Length; i++) {
			foreach (GameObject G in CarPrefabs[i].Parts) {
				GameObject NewG = Instantiate(G);
				NewG.name = G.name + gameObject.tag;
			}
			
		}
		TypePick = 0;
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
			PartShow[TypePick]++;
			if (PartShow[TypePick] + 1 > CarPrefabs [TypePick].Parts.Length) {
				PartShow[TypePick] -= CarPrefabs [TypePick].Parts.Length;
			}
			Parts[TypePick] = PartShow[TypePick];
		}
		
		if ((P1 && Input.GetKeyDown(KeyCode.A)) || (!P1 && Input.GetKeyDown(KeyCode.LeftArrow)) ) {
			PartShow[TypePick]--;
			if (PartShow[TypePick] < 0) {
				PartShow[TypePick] = CarPrefabs [TypePick].Parts.Length - 1;
			}
			Parts[TypePick] = PartShow[TypePick];
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
		if (PartShow[TypePick] + 1 > CarPrefabs [TypePick].Parts.Length) {
			PartShow[TypePick] -= CarPrefabs [TypePick].Parts.Length;
		}
		Parts[TypePick] = PartShow[TypePick];
	}
	public void Left() {
		PartShow[TypePick]--;
		if (PartShow[TypePick] < 0) {
			PartShow[TypePick] = CarPrefabs [TypePick].Parts.Length - 1;
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
		foreach (GameObject G in GameObject.FindGameObjectsWithTag("MainCamera")) {
			G.GetComponent<Cam>().enabled = true;
		}
		GameObject C = GameObject.Find("Car P2");
		C.GetComponent<CarMove>().enabled = true;
		GetComponent<CarMove>().enabled = true;
		C.GetComponent<Builder>().enabled = false;
		GetComponent<Builder>().enabled = false;
	}
}