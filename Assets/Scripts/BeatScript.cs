using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class BeatScript : MonoBehaviour {
    public AnimatorController normalBeat;
    public AnimatorController poisonBeat;
    public AnimatorController ghostBeat;
    // public AnimatorController doubleBeat;

    void Start() {
        setupBeat(0, BeatTypes.Poison);
    }

    void Update() {

    }

    public void setupBeat(int beat, BeatTypes type) {
        gameObject.SetActive(true);

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
                break;

            default:
                gameObject.SetActive(false);
                Debug.Log("This beat hasn't been implemented yet!");
                break;
        }
    }
}