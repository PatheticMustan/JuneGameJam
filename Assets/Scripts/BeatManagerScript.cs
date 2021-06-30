using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManagerScript : MonoBehaviour {
    public int currentBeat;

    /** Beat Notes
     * x - empty space
     * b - normal beats
     * p - poison beat, clicking this beat deals double damage!
     * d - double
     * g - ghost, becomes transparent when the player is two beats away
     **/

    // each phase is 8 beats long, first the flower sets out 8 beats, then the player copies those 8
    // spaces are ignored
    public Song[] levels = new Song[] {
        new Song("Test Song", 120, "bxbxbxbx xbxbxbxb xbxbxbbb xxxbxxxb")
    };

    void Start() {

    }

    void FixedUpdate() {
        
    }
}

[System.Serializable]
public struct Song {
    public string songName;
    public int bpm;
    public string beatData;

    public Song(string songName, int bpm, string beatData) {
        this.songName = songName;
        this.bpm = bpm;
        this.beatData = beatData;
    }
}