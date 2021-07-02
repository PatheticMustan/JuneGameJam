using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public int score = 0;

    [Space]
    [Header("References")]

    public BeatManagerScript bms;

    public SpriteRenderer scoreboard;
    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite earlySprite;
    public Sprite lateSprite;

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) click();

        if (Input.GetKeyDown(KeyCode.K)) startLevel(0);

        //Debug.Log(1 - (bms.time % (1 / bms.beatsPerSecond)));
    }

    void click() {
        GetComponent<AudioSource>().Play();

        if (bms.playing) {
            // how good a hit is (lower is better)
            // ranges from -1 to 1
            float clickTiming = 1 - (bms.time % (1f / bms.beatsPerSecond)) * bms.beatsPerSecond;
            // ranges from 0 to 1
            float cta = Mathf.Abs(clickTiming);

            Debug.Log(clickTiming + " " + bms.time + " " + bms.beatsPerSecond + " " + (1f / bms.beatsPerSecond));

            /** Beat Timings
             * 0.5 to 0.8:   too early
             * 0.8 to 0.9:   good
             * 0.9 to 0.1:   perfect
             * 0.1 to 0.2:   good
             * 0.2 to 0.5:   too late
             * no click at all: miss
             **/

            // I was trying to use a switch case but apparently those only accept actual values, not just conditions.
            if (clickTiming >= 0.9) scoreboard.sprite = perfectSprite;
            else if (clickTiming >= 0.75) scoreboard.sprite = goodSprite;
            else if (clickTiming >= 0.5) scoreboard.sprite = lateSprite;
            else if (clickTiming >= 0.25) scoreboard.sprite = earlySprite;
            else if (clickTiming >= 0.1) scoreboard.sprite = goodSprite;
            else Debug.Log("PERFECT!!!!"); 
            //else Debug.Log("How??!?!?! This is not supposed to happen.");
        } else {
            Debug.Log("Not playing, fool!");
        }

        //Debug.Break();
    }

    void startLevel(int level) {
        startLevel(level, 0);
    }

    void startLevel(int level, int beat) {
        score = 0;
        bms.startLevel(level, beat);
    }
}
