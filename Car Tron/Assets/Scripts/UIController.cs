using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    [SerializeField] private Text timer;
    [SerializeField] private Text health;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private PlayerController playerController;
    private int _minute;
    private double _seconds;
    public void OnOpenSettings() {
		settingsPopup.Open ();
	}
		
	void Start () {
        timer.text = "0:00";
        health.text = "Health: " + playerController.defaultHealth;
        _minute = 0;
        _seconds = 0;
        settingsPopup.Close ();
	}

	public void Resume(){
		settingsPopup.Close ();
	}

	public void Restart(){
        timer.text = "0:00";
        health.text = "Health: " + playerController.defaultHealth;
        _minute = 0;
        _seconds = 0;
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void BackToStart(){
        health.text = "Health: " + playerController.defaultHealth;
        timer.text = "0:00";
        _minute = 0;
        _seconds = 0;
        SceneManager.LoadScene("Start");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HealthPower")
        {
            health.text = "Health: " + playerController.health;
        }
    }

    // Update is called once per frame
    void Update () {
        health.text = "Health: " + playerController.health;
        _seconds += Time.deltaTime;
        timer.text = _minute + ":" + (int)_seconds;
        if (_seconds >= 60)
        {
            _minute++;
            _seconds = 0;
        }
    }
}
