using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cam : MonoBehaviour {

	public Transform Car;
	public Vector3 Offset;
	Vector3 TargetPos;
	public Vector4 GameRect;
	Camera Camera;
	public GameObject P1Wins;
	public GameObject P2Wins;
	public GameObject Draw;


	void Start () {
		Camera = GetComponent<Camera>();
		Camera.rect = new Rect (GameRect.x, GameRect.y, GameRect.z, GameRect.w);
	}
	
	void Update () {
		if (GameObject.Find("End Game") == null) {
			transform.position = Vector3.Lerp (transform.position, Car.position + Car.up * Offset.y + Car.forward * Offset.z, Time.deltaTime * 5);
			transform.LookAt (Car.position + Car.transform.forward * 0.5f);
		} else {
			if (P1Wins.active && P2Wins.active) {
				P1Wins.SetActive (false);
				P2Wins.SetActive (false);
				Draw.SetActive (true);
			}
			if (tag == "Win Camera") {
				Camera.depth = 1;
				Camera.rect = new Rect (0, Mathf.Lerp (Camera.rect.position.y, 0, Time.deltaTime * 2), 1, Mathf.Lerp (Camera.rect.size.y, 1, Time.deltaTime * 2));
				transform.position = Vector3.Lerp (transform.position, new Vector3(0, 55, 0), Time.deltaTime * 2);
				transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, new Vector3 (90, 0, 0), Time.deltaTime * 2);
			}
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
				Restart();
			}
		}
	}
	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Quit () {
		Application.Quit();
	}
}