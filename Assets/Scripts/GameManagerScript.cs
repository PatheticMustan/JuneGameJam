﻿using System.Collections;
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

        Debug.Log(1 - (bms.time % (1 / bms.beatsPerSecond)));
    }

    void click() {
        GetComponent<AudioSource>().Play();

        if (bms.playing) {
            // how good a hit is (lower is better)
            // ranges from -1 to 1
            float clickTiming = 1 - (bms.time % (1 / bms.beatsPerSecond));
            // ranges from 0 to 1
            float cta = Mathf.Abs(clickTiming);

            Debug.Log(clickTiming);

            /** Beat Timings
             * 1 to 0.5:        too early
             * 0.5 to 0.25:     good
             * 0.25 to -0.25:   perfect
             * -0.25 to -0.5:   good
             * -0.5 to -1:      too late
             * no click at all: miss
             **/

            // I was trying to use a switch case but apparently those only accept actual values, not just conditions.
            if (clickTiming >= 0.5) Debug.Log("Early!");
            else if (clickTiming >= 0.25) Debug.Log("Good.");
            else if (clickTiming >= -0.25) Debug.Log("PERFECT!!!!");
            else if (clickTiming >= -0.5) Debug.Log("Good.");
            else if (clickTiming >= -1) Debug.Log("Late!");
            else Debug.Log("How??!?!?! This is not supposed to happen.");
        } else {
            Debug.Log("Not playing, fool!");
        }
    }

    void startLevel(int level) {
        startLevel(level, 0);
    }

    void startLevel(int level, int beat) {
        score = 0;
        bms.startLevel(level, beat);
    }
}
