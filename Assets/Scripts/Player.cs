using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        hpBar.UpdateHPBar(hp, maxHP);

        if (hp <= 0) {
            return false;
        }

        return true;
    }
}