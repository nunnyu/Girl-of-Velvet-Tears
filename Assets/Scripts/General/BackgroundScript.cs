using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {
    [SerializeField] Transform cameraPosition;
    void Update() {
        gameObject.transform.position = new Vector2(cameraPosition.position.x, cameraPosition.position.y);
    }
}
