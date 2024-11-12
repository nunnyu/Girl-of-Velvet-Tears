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

    void Start() {
        HideAllChildren();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            ShowAllChildren();
        }
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


    // public void EndGame() {
    //     if (gameOver == false) {
    //         gameOver = true;
    //         // Debug.Log("restart");
    //         // Invoke("Restart", restartDelay);
    //         // gameOverScript.setNoireSpawn(transform.position.x, transform.position.y + .25f); 
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if (other.tag == "Player") {
    //         Debug.Log("next level");
    //         Invoke("nextLevel", levelChangeDelay);
            
    //     }
    // }

    // private void Restart() {
        // gameOverScript.levelStart();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloads the current scene
    // }

    // private void nextLevel() {
    //     currentLevel++;
    //     SceneManager.LoadScene("Stage" + currentLevel);
    // }
}