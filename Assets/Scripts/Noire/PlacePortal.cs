using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlacePortal : MonoBehaviour
{
    [SerializeField] private Transform portalY;
    [SerializeField] private Transform portalZ;

    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask colliderMask;
    [SerializeField] private LayerMask antiportalMask;
    [SerializeField] private LayerMask portalYMask;
    [SerializeField] private LayerMask portalZMask;
    [SerializeField] private PortalMomentum portalMomentumScript;
    [SerializeField] private MovementPlayer movementPlayerScript;

    // mouse constraints
    [SerializeField] private Transform centerPoint; // center point from which the mouse movement will be constrained
    [SerializeField] private float range;
    [SerializeField] private GameObject pact;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private Portal portalScriptZ;
    [SerializeField] private Portal portalScriptY;

    // portal destruction
    private bool Yvisible = false;
    private bool Zvisible = false;

    private bool YTP;
    private bool ZTP;

    void Start() {
        portalY.localScale = new Vector2(0, 0);
        portalZ.localScale = new Vector2(0, 0);
    }

    private void closePortals() {
        portalY.transform.position = new Vector2(transform.position.x, transform.position.y + .6f);
        portalZ.transform.position = new Vector2(transform.position.x, transform.position.y + .6f);
        Yvisible = false;
        Zvisible = false;
        portalScriptZ.closeAnimation(false);
        portalScriptY.closeAnimation(false);
        
        Invoke("resetRotationBoth", .2f);
    }

    private void resetRotationBoth() {
        portalScriptY.resetRotation();
        portalScriptZ.resetRotation();
    }

    void Update() {
        if (!(portalY.GetComponent<Portal>().playerIsTeleporting() || portalZ.GetComponent<Portal>().playerIsTeleporting() || !(movementPlayerScript.LastOnGroundTime > 0))) {
            if (Input.GetKeyDown("q") || gameOver.getMoving() == false) {
                portalScriptZ.closeAnimation(true);
                portalScriptY.closeAnimation(true);
                Invoke("closePortals", .2f);
            } 
        } else if (!(portalY.GetComponent<Portal>().playerIsTeleporting() || portalZ.GetComponent<Portal>().playerIsTeleporting())) {
            if (gameOver.getMoving() == false) { // i just copy pasted this as an exception cuz if you die the portal should close regardless
                portalScriptZ.closeAnimation(true);  // the ability to move is synchronous with the alive state of Noire 
                portalScriptY.closeAnimation(true);
                Invoke("closePortals", .2f);
            }
        }
        
        if (!Yvisible) {
            portalY.localScale = new Vector2(0, 0);
            YTP = false;
        } else {
            portalY.localScale = new Vector2(1, 1);
            YTP = true;
        }

        if (!Zvisible) {
            portalZ.localScale = new Vector2(0, 0);
            ZTP = false;
            
        } else {
            portalZ.localScale = new Vector2(1, 1);
            ZTP = true;
        }
        
        Vector2 centerPos = transform.position;
        Vector2 realMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 offset = realMousePosition - centerPos;

        // Clamp the offset to stay within the circular range
        offset = Vector2.ClampMagnitude(offset, range);

        // Update the constrained mouse position
        pact.transform.position = centerPos + offset;

        // Restrain the cursor movement within the circular range
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (Clear("y")) {
                Yvisible = true;
                SpawnPortal("y");
                ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
        		audiomanager.Play("portal");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            if (Clear("z")) {
                Zvisible = true;
                SpawnPortal("z");
                ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
        		audiomanager.Play("portal");
            }
        }
    }

    // checks if the area can have a portal (isn't inside a collider)
    private bool Clear(string type) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pact.transform.position, checkRadius, colliderMask);
        Collider2D[] antiportal = Physics2D.OverlapCircleAll(pact.transform.position, checkRadius, antiportalMask);
        Collider2D[] portalY = Physics2D.OverlapCircleAll(pact.transform.position, checkRadius - .1f, portalYMask);
        Collider2D[] portalZ = Physics2D.OverlapCircleAll(pact.transform.position, checkRadius - .1f, portalZMask);
        if (type == "y")
            if (colliders.Length == 0 && portalZ.Length == 0 && antiportal.Length == 0) {
                // if no colliders within the specified radius around the mouse click position
                return true;
            }
        
        if (type == "z")
            if (colliders.Length == 0 && portalY.Length == 0 && antiportal.Length == 0) {
                // if no colliders within the specified radius around the mouse click position
                return true;
            }

        return false;
    }

    // spawns portal at mouse
    private void SpawnPortal(string type) {
        if (type.Equals("y"))
            portalY.position = pact.transform.position;
        if (type.Equals("z"))
            portalZ.position = pact.transform.position;
    }

    // if a portal isn't on the screen, the other portal should do nothing
    public bool portalAvailable() {
        if (YTP && ZTP) {
            return true;
        }
        return false;
    }
}