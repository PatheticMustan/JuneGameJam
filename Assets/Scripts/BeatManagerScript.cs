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
    public GameObject musicCounter;

    public GameObject beatPrefab;

    private int lastUpdatedBeat;
    private GameObject[] beats;

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
        beats = new GameObject[8];
    }

    void FixedUpdate() {
        if (playing) {
            time += Time.deltaTime;
            currentBeat = (int)(time * beatsPerSecond);

            float xVal = -6.3f + (1.6f * (currentBeat % 12));
            if (currentBeat % 12 > 7) {
                if (currentBeat % 12 > 9) xVal = -6.3f;
                else xVal = 6.3f;
            }
            player.transform.position = new Vector3(xVal, 0, 0);

            musicCounter.transform.position = player.transform.position + new Vector3(-0.8f, 0, 0);

            // on the first beat of the song, or the 10th beat of every 12 beats.
            if (currentBeat == 0 || currentBeat % 12 == 9) {
                if (lastUpdatedBeat != currentBeat) {
                    lastUpdatedBeat = currentBeat;

                    

                    for (int i = 0; i < 8; i++) {
                        if (currentBeat != 0) {
                            Destroy(beats[i]);
                        }
                        beats[i] = Instantiate(beatPrefab);

                        // no, you can't just replace the last part with (currentBeat/3).
                        // each song line has 8 beats, but each line has a 4 beat buffer afterwards. Current beat is %12, but we need the real beat index,
                        // so we do some wizard stuff to get it. I swear we've thought it out, we're not just hackjobs!
                        int cbi = currentBeat + i;
                        int realBeatIndex = cbi - (4 * (cbi / 12));
                        BeatTypes currentBeatType;

                        

                        if (realBeatIndex >= beatData.Length) currentBeatType = BeatTypes.Rest;
                        else currentBeatType = beatData[realBeatIndex];

                        //Debug.Log(currentBeatType + ", " + realBeatIndex);

                        beats[i].GetComponent<BeatScript>().setupBeat(i, currentBeatType);
                        beats[i].transform.parent = beatContainer.transform;
                    }
                }
            }

            // last beat, past the rest
            if (currentBeat % 12 == 11) {
                int realBeatIndex = currentBeat - (4 * (currentBeat / 12));
                if (realBeatIndex >= beatData.Length) {
                    playing = false;
                    // end the game
                    for (int i = 0; i < 8; i++) {
                        Destroy(beats[i]);
                    }
                }
            }
        }
    }

    public void startLevel(int level, int beat) {
        beat = 0;
        lastUpdatedBeat = -1;
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