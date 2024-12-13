using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;
    // [SerializeField] private float playerMomentumTime;
    private bool playerTeleporting;
    private bool toggleSpin;
    private Quaternion defaultRotation;

    void Start() {
        playerTeleporting = false;
        defaultRotation = transform.rotation;
    }
    
    void Update() {
        // print(playerTeleporting);
        if (toggleSpin) {
            transform.Rotate(1.5f, 0, 0);
        }
    }

    public void resetRotation() {
        transform.rotation = defaultRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.6f) {
                if (player.GetComponent<PlacePortal>().portalAvailable()) {
                    player.transform.position = target.transform.position;
                    ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
        		    audiomanager.Play("teleport");
                    playerTeleporting = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerTeleportingOff();
        }
    }

    public void playerTeleportingOff() {
        playerTeleporting = false;
    }

    public bool playerIsTeleporting() {
        return playerTeleporting;
    }

    public void closeAnimation(bool tf) {
        toggleSpin = tf;
    }
}