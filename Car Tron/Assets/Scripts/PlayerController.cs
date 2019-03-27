using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    private const string VERTICAL_AXIS_KEY = "Vertical";
    private const string HORIZONTAL_AXIS_KEY = "Horizontal";

    private Transform transform_;

    private Rigidbody rig;

    public float defaultSpeed = 7;

    public float speed = 7;

    public float defaultHealth = 100;

    public float health = 100;
    private void Awake()
    {
        transform_ = transform;
        rig = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Border")
        {
            health -= 10;
            Debug.Log("Health: " + health);
        }
    }

    private void FixedUpdate()
    {
        float hInputAxis = Input.GetAxis(HORIZONTAL_AXIS_KEY);
        float vInputAxis = Input.GetAxis(VERTICAL_AXIS_KEY);

        Vector3 movement = 
            new Vector3(hInputAxis * speed, 0, vInputAxis * speed);

        Vector3 nextPosition = transform.position + movement * Time.deltaTime;

        rig.velocity = movement;

        
    }
}