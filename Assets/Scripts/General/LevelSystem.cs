using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    bool gameOver = false;
    public float restartDelay = .5f;
    // public GameOver gameOverScript;
    public float levelChangeDelay = 0;
    // public Transform light;
    public int currentLevel = 1;
    private Boolean active; // for audio, so the ding only plays once 
    public Boolean isGravestone; 
    private Boolean gravestoneActive;
    public Boolean isMenu;

    void Start() {
        HideAllChildren();
        active = true;
        gravestoneActive = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isMenu) {
            NextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (active && !isGravestone) {
                ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
                audiomanager.Play("ding");
                active = false;
                ShowAllChildren();
            }
            
            if (gravestoneActive && isGravestone) {
                gravestoneActive = false;
                Destroy(FindObjectOfType<PlayerData>().gameObject);
                ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
                audiomanager.Play("jump");
                audiomanager.Play("death");
                Invoke("SoundQueue", 0.1f);
                Invoke("NextLevel", 7.5f);
            }
        }
    }

    private void SoundQueue() {
        ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
        ShowAllChildren();
        audiomanager.Play("wind");
        audiomanager.Stop("background1");
    }

    // Function to hide all child objects
    public void HideAllChildren()
    {
        // Iterate through each child of the transform
        foreach (Transform child in transform)
        {
            // Deactivate the child GameObject
            child.gameObject.SetActive(false);
        }
    }

    // Function to show all child objects
    public void ShowAllChildren()
    {
        // Iterate through each child of the transform
        foreach (Transform child in transform)
        {
            // Activate the child GameObject
            child.gameObject.SetActive(true);
        }
    }

    private void NextLevel() {
        currentLevel++;
        SceneManager.LoadScene("Stage" + currentLevel);
        Debug.Log("Stage" + currentLevel);
    }
}