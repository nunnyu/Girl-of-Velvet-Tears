using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {        
    // smooth camera control
    public Transform player;
    public Camera orthoCamera;
    public float ydisplacement; // y displacement for the offset
    public float damping = 0.25f;
    private Vector3 velocity = Vector3.zero;

    // zooming in and out
    public float zoomRate = 0.04f;
    public float verticalDisplacementRate = 0.3f;
    public float maxSize = 7;
    public float minSize = 3;

    void Update() {
        Vector3 offset = new Vector3(0f, ydisplacement, -10f);
        // zooming in and out
        float size = orthoCamera.orthographicSize;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && size < maxSize) {
            orthoCamera.orthographicSize += zoomRate;
            ydisplacement+= verticalDisplacementRate;
        } else if (Input.GetAxis("Mouse ScrollWheel") > 0f && size > minSize) {
            orthoCamera.orthographicSize -= zoomRate;
            ydisplacement -= verticalDisplacementRate;
        }

        // smooth camera control
        Vector3 movePosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
