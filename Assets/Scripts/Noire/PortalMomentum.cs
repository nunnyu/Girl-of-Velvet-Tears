using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMomentum : MonoBehaviour {
    [SerializeField] private MovementPlayer movementScript;
    [SerializeField] private GameObject portalY;
    [SerializeField] private GameObject portalZ;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float portalPowerScalingX;
    [SerializeField] private float portalPowerScalingY;
    public bool canDestroy = true;
    public Animator animator;
    private float powerX;
    private float powerY;

    // ok this is lazy as heck but this method is also the same timer that prevents the destruction of portals mid teleport
    private void handlePlayerTPAnimation() {
        animator.SetBool("Teleported", false);
        canDestroy = false;
    }

    void Update() {
        // animation stuff
        if (portalY.GetComponent<Portal>().playerIsTeleporting() || portalZ.GetComponent<Portal>().playerIsTeleporting()) {
            animator.SetBool("Teleported", true);
            Invoke("handlePlayerTPAnimation", .25f);
            canDestroy = true;
        }
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isTeleporting = portalY.GetComponent<Portal>().playerIsTeleporting() || portalZ.GetComponent<Portal>().playerIsTeleporting();
        var yVel = rb.velocity.y;
        var xVel = rb.velocity.x;
        // print("jumping: " + movementScript.IsJumping /*+ "  y velocity: " + yVel*/);
        powerX = -1f * yVel * portalPowerScalingX;
        powerY = -1f * yVel * portalPowerScalingY;
        if (isTeleporting) {
            if (moveInput > 0) {
                addForce("right");
                if (!movementScript.IsJumping) {
                    addForce("up");
                }
            } else if (moveInput < 0) {
                addForce("left");
                if (!movementScript.IsJumping) {
                    addForce("up");
                }
            }
        }
    }

    void addForce(string direction) {
        if (direction.Equals("up"))
            rb.AddForce(powerY * Vector2.up, ForceMode2D.Force);
        if (direction.Equals("down"))
            rb.AddForce(powerY * Vector2.down, ForceMode2D.Force);
        if (direction.Equals("left"))
            rb.AddForce(powerX * Vector2.left, ForceMode2D.Force);
        if (direction.Equals("right"))
            rb.AddForce(powerX * Vector2.right, ForceMode2D.Force);
    }
}
