using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class BeatManagerScript : MonoBehaviour {
    public int currentBeat;
    public float time;

    public bool playing;

    public float beatLength;

    public BeatTypes[] beatData;

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
        //new Song("Test Song", 120, "xxxbxxxb", audioclip)
    };

    void Start() {

    }

    void FixedUpdate() {
        if (playing) {
            time += Time.deltaTime;
            currentBeat = (int)(time * beatLength);
        }
    }

    public void startLevel(int level, int beat) {
        beat = 0;
        GetComponent<AudioSource>().clip = levels[level].audio;
        GetComponent<AudioSource>().Play();

        string cleanedBeats = Regex.Replace(levels[level].beatData.ToLower(), @"[^xbpdg]", "");

        for (int i = 0; i < cleanedBeats.Length; i++) {
            switch (cleanedBeats[i]) {
                case 'x':
                    beatData[i] = BeatTypes.Rest;
                    break;
                case 'b':
                    beatData[i] = BeatTypes.Normal;
                    break;
                case 'p':
                    beatData[i] = BeatTypes.Poison;
                    break;
                case 'd':
                    beatData[i] = BeatTypes.Double;
                    break;
                case 'g':
                    beatData[i] = BeatTypes.Ghost;
                    break;
                default:
                    Debug.LogError("Invalid note! AHHHHH!");
                    break;
            }
        }

        beatLength = (float)(levels[level].bpm) / 60;
        time = 0;

        playing = true;
    }
}

[System.Serializable]
public struct Song {
    public string songName;
    public int bpm;
    public string beatData;
    public AudioClip audio;

    public Song(string songName, int bpm, string beatData, AudioClip audio) {
        this.songName = songName;
        this.bpm = bpm;
        this.beatData = beatData;
        this.audio = audio;
    }
}

[System.Serializable]
public enum BeatTypes { 
    Rest,
    Normal,
    Poison,
    Double,
    Ghost
}