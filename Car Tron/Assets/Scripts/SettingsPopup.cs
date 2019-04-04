using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour {


	public void Open () {
		gameObject.SetActive (true);
        Time.timeScale = 0;
	}

	public void Close () {
		gameObject.SetActive (false);
        Time.timeScale = 1;
    }

	public void OnSubmitName(string name){
		Debug.Log (name);
	}

	public void OnSpeedValue(float speed){
		Messenger<float>.Broadcast (GameEvent.SPEED_CHANGED, speed);
	}
}
