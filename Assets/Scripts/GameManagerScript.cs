using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public int score = 0;

    [Space]
    [Header("References")]

    public BeatManagerScript bms;

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) click();

        if (Input.GetKeyDown(KeyCode.K)) startLevel(0);
    }

    void click() {
        GetComponent<AudioSource>().Play();
        
    }

    void startLevel(int level) {
        startLevel(level, 0);
    }

    void startLevel(int level, int beat) {
        score = 0;
        bms.startLevel(level, beat);
    }
}
