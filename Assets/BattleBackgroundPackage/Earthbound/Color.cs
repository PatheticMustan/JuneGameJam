using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    public Gradient colorGradient;

    public float gradientTime;

    private Renderer renderComp;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        renderComp = GetComponent<Renderer>();

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        renderComp.material.color = colorGradient.Evaluate((timer % gradientTime / gradientTime));
    }
}
