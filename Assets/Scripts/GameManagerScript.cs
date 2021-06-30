using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public int score = 0;

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) click();
    }

    void click() { 
    }

    void startLevel(int level) {
        score = 0;
    }
}
