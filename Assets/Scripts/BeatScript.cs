using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScript : MonoBehaviour {
    public RuntimeAnimatorController normalBeat;
    public RuntimeAnimatorController poisonBeat;
    public RuntimeAnimatorController ghostBeat;
    // public AnimatorController doubleBeat;

    public BeatManagerScript bms;

    public int beat;
    public BeatTypes type;
    public bool used;

    


    void Start() {
        //setupBeat(0, BeatTypes.Poison);
        bms = GameObject.FindGameObjectsWithTag("BeatManager")[0].GetComponent<BeatManagerScript>();
        used = false;
    }

    void FixedUpdate() {
        switch (type) {
            case BeatTypes.Poison:
                break;

            case BeatTypes.Ghost:
                //Debug.Log(bms.currentBeat);
                if ((bms.currentBeat % 12) + 1 >= beat) {
                    GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
                } else {
                    GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0xFF, 0xFF, 0x30);
                }
                break;

            default:
                break;
        }

        int cb = bms.currentBeat % 12;

        if (cb < 9 && cb != 0 && ((bms.time * bms.beatsPerSecond)%12)-1.5f > beat) {
            if (type == BeatTypes.Poison) return;

            if (!used && gameObject.activeSelf) {
                //Debug.Log("missed beat #" + (beat+1) + ", type " + type + ", cb " + (bms.currentBeat));
                //Debug.Log((bms.time * bms.beatsPerSecond) + ": " + beat);
                used = true;
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().ChangeHP(10);
            }
        }
    }

    public void setupBeat(int beat, BeatTypes type) {
        this.beat = beat;
        this.type = type;

        // setup animator
        switch (type) {
            case BeatTypes.Rest:
                gameObject.SetActive(false);
                break;

            case BeatTypes.Normal:
                GetComponent<Animator>().runtimeAnimatorController = normalBeat;
                break;

            case BeatTypes.Poison:
                GetComponent<Animator>().runtimeAnimatorController = poisonBeat;
                break;

            case BeatTypes.Double:
                // GetComponent<Animator>().runtimeAnimatorController = doubleBeat;
                break;

            case BeatTypes.Ghost:
                GetComponent<Animator>().runtimeAnimatorController = ghostBeat;
                GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0xFF, 0xFF, 0x30);
                break;

            default:
                gameObject.SetActive(false);
                Debug.Log("This beat hasn't been implemented yet!");
                break;
        }

        // move beat to position
        transform.position = new Vector3(-5.5f + (1.6f * (beat % 8)), 0 ,0);
    }

    public void click() {
        used = true;

        //Debug.Log(type);
        if (type != BeatTypes.Rest)
            bms.ParticleEffect(type, transform.position);

        switch (type) {
            case BeatTypes.Rest:
                break;

            case BeatTypes.Normal:
                gameObject.SetActive(false);

                break;

            case BeatTypes.Poison:
                gameObject.SetActive(false);
                Debug.Log("Ow! Poison!");
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().ChangeHP(30);
                break;

            case BeatTypes.Double:
                // GetComponent<Animator>().runtimeAnimatorController = doubleBeat;
                break;

            case BeatTypes.Ghost:
                gameObject.SetActive(false);
                break;

            default:
                Debug.Log("This beat hasn't been implemented yet!");
                break;
        }
    }
}