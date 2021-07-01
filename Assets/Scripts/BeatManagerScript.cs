using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class BeatManagerScript : MonoBehaviour {
    public int currentBeat;
    public float time;

    public bool playing;

    public float beatsPerSecond;

    public BeatTypes[] beatData;

    public GameObject player;
    public GameObject beatContainer;

    public GameObject beatPrefab;

    private int lastUpdatedBeat;

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
            currentBeat = (int)(time * beatsPerSecond);
            Vector3 start = new Vector3(-6.3f, 0, 0);
            Vector3 end = new Vector3(6.3f, 0, 0);
            float speed = 1.59f; // 1.59 is the length of one beat cell, reset every 8 beats

            //if (Equals(player.transform.position, end)) {
            //musicCounter.transform.position = start;
            //}

            /*musicCounter.transform.position = Vector3.MoveTowards(
                musicCounter.transform.position,
                end,
                speed*Time.deltaTime* beatsPerSecond
            );*/

            float xVal = -6.3f + (1.6f * (currentBeat % 12));

            if (currentBeat % 12 > 7)
            {
                if (currentBeat % 12 > 9)
                    xVal = -6.3f;
                else
                    xVal = 6.3f;
            }


            player.transform.position = new Vector3(xVal, 0, 0);
        }
    }

    public void startLevel(int level, int beat) {
        beat = 0;
        GetComponent<AudioSource>().clip = levels[level].audio;
        GetComponent<AudioSource>().Play();

        string cleanedBeats = Regex.Replace(levels[level].beatData.ToLower(), @"[^xbpdg]", "");

        beatData = new BeatTypes[cleanedBeats.Length];

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

        // the number of beats in one second
        beatsPerSecond = (float)(levels[level].bpm) / 60;
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