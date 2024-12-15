using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
// using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour {
    public GameObject pact;
    public Animator animator;
    public MovementPlayer playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector2 spawnPoint;
    private Color originalColor;
    private int hit; // threats hit; so if she hits a threat while in the death animation she doesn't die again cuz she's already dead

    private bool moving = true;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Threat" && hit < 2) {
            SlaughterNoire();
        }
        if (other.tag == "Lamp") {
            spawnPoint = other.transform.position;
        }
    }
    void Start() {
        // spawnPoint = new Vector2(-3f, 1.5f);
        hit = 0;
        originalColor = spriteRenderer.color;
        setMoving(true);
    }

    public void setNoireSpawn(float x, float y) {
        spawnPoint = new Vector2(x, y);
    }

    void Update() {
        // print("hit: " + hit);
        if (Input.GetKeyDown("r")) {
            SlaughterNoire();
        }
        // Debug.Log("spawnpoint: " + spawnPoint);
    }

    // ik this is called gameEnd but this is essentially the respawn function
    private void GameEnd() {
        transform.position = spawnPoint;
        hit = 0;
        setMoving(true);
        animator.SetBool("Dead", false);
        spriteRenderer.color = originalColor; 

        // FindObjectOfType<Portal>().playerTeleportingOff();
    }

    private void SlaughterNoire() {
        ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
        audiomanager.Play("death");
        spriteRenderer.color = Color.black;
        setMoving(false);
        hit++;
        animator.SetBool("Dead", true);
        Invoke("GameEnd", .4f);
    }

    // use this in other methods to tell them that Noire shouldn't be moving 
    public void setMoving(bool a) {
        moving = a;
    }

    public bool getMoving() {
        return moving;
    }
}
