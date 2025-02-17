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
    private bool isTeleporting;
    private float moveInput;

    private void handlePlayerTPAnimation() {
        animator.SetBool("Teleported", false);
        canDestroy = false;
    }

    void Update() {
        // Handle animation
        isTeleporting = portalY.GetComponent<Portal>().playerIsTeleporting() || portalZ.GetComponent<Portal>().playerIsTeleporting();
        moveInput = Input.GetAxisRaw("Horizontal");

        if (isTeleporting) {
            animator.SetBool("Teleported", true);
            Invoke("handlePlayerTPAnimation", .25f);
            canDestroy = true;
        }
    }

    void FixedUpdate() { 
        if (isTeleporting) {
            float yVel = rb.velocity.y;
            powerX = -1f * yVel * portalPowerScalingX;
            powerY = -1f * yVel * portalPowerScalingY;

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
        Vector2 force = Vector2.zero;
        if (direction.Equals("up")) force = powerY * Vector2.up;
        if (direction.Equals("down")) force = powerY * Vector2.down;
        if (direction.Equals("left")) force = powerX * Vector2.left;
        if (direction.Equals("right")) force = powerX * Vector2.right;

        rb.AddForce(force * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
