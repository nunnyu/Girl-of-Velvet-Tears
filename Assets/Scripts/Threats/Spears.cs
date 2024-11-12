using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Spears : MonoBehaviour {
    [SerializeField] private Transform spearTransform;
    [SerializeField] private Transform sensorTransform;
    
    public float moveDistance = 3.0f; // Distance to move
    public float moveDuration = 1.0f; // Duration of the move

    Vector3 originalPosition;
    bool moving;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !moving) {
            MoveSpearUp();
        }
    }

    void Start() {
        moving = false;
        originalPosition = spearTransform.position;
    }

    public void MoveSpearUp() {
        StartCoroutine(MoveUpCoroutine());
    }

    private IEnumerator MoveUpCoroutine() {
        Vector3 startPosition = originalPosition;
        Vector3 endPosition = startPosition + new Vector3(0, moveDistance, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            moving = true;
            spearTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spearTransform.position = endPosition;

        // Move down
        elapsedTime = 0f;
        while (elapsedTime < moveDuration * 10)
        {
            
            spearTransform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime / moveDuration / 10);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moving = false;
        spearTransform.position = startPosition;

    }
}