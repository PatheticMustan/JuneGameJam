using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public RectTransform hpBarObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // x = 0 maxHP
    // x = -4 0 HP
    public void UpdateHPBar (int currentHP, int maxHP)
    {
        float percent = (maxHP - currentHP) / maxHP;

        //hpBarObject
    }
}
