using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {
    // [SerializeField] private Transform transform;
    [SerializeField] private float speed;   // Speed of the swinging motion
    [SerializeField] private float angle;  // Maximum swing angle

    private float startTime;

    void Start() {
        startTime = Time.time;
    }

    void Update() {
        // rotation angel 
        float angleOffset = Mathf.Sin((Time.time - startTime) * speed) * angle;
        transform.localRotation = Quaternion.Euler(0, 0, angleOffset);
    }
}