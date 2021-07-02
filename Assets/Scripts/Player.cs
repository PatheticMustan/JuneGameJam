using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int hp;
    public int maxHP;

    public HPBar hpBar;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public bool ChangeHP(int damage) {
        hp -= damage;

        hpBar.ChangeHP(damage);

        if (hp <= 0) {
            SceneManager.LoadScene("Loss");
            return false;
        }

        return true;
    }
}