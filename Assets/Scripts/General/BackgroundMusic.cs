using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        ManageAudio audiomanager = FindObjectOfType<ManageAudio>();
        audiomanager.PlayLoop("background1");
    }
}
