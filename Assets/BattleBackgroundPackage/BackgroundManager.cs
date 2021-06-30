using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [Header("Material and Projector")]
    [SerializeField]
    private Material ebMaterial;
    [SerializeField]
    private Image backgroundDisplay;
    [Space(10)]
    //public Color32[] colorArray;

    //[SerializeField]
    private float timerOne;
    //[SerializeField]
    //private float timerAmountOne;
    //[SerializeField]
    private int counterOne;

    //[SerializeField]
    private float timerTwo;
    //[SerializeField]
    private int counterTwo;

    //private float scrollTimer;

    //[SerializeField]
    //private float scrollSpeed;
    //[SerializeField]
    //private Vector2 scrollOffset;

    //public Background test;
    [Header("Background Info")]
    [SerializeField]
    private int bgID;
    [SerializeField]
    private bool update;

    [SerializeField] 
    private Background[] backgrounds;

    //public Color red;


    // Start is called before the first frame update
    void Start()
    {
        update = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Color Cycle
        timerOne += Time.deltaTime;
        if (timerOne > backgrounds[bgID].TimerTickOne && backgrounds[bgID].ColorArrayOne.Length != 0)
        {
            counterOne = (counterOne + 1) % backgrounds[bgID].ColorArrayOne.Length;
            timerOne %= backgrounds[bgID].TimerTickOne;

            ebMaterial.SetInt("_CycleOne", counterOne);
        }

        timerTwo += Time.deltaTime;
        if (timerTwo > backgrounds[bgID].TimerTickTwo && backgrounds[bgID].ColorArrayTwo.Length != 0)
        {
            counterTwo = (counterTwo + 1) % backgrounds[bgID].ColorArrayTwo.Length;
            timerTwo %= backgrounds[bgID].TimerTickTwo;

            ebMaterial.SetInt("_CycleTwo", counterTwo);
        }

        if (update)
        {
            UpdateBackground();
        }
    }

    public void UpdateBackground()
    {
        try
        {
            timerOne = 0f;
            counterOne = 0;
            timerTwo = 0f;
            counterTwo = 0;

            ebMaterial.SetInt("_CycleOne", 0);
            ebMaterial.SetInt("_CycleTwo", 0);

            if (backgrounds[bgID].ColorArrayOne.Length != 0)
            {
                Vector4[] convertedArray = new Vector4[64];

                for (int i = 0; i < backgrounds[bgID].ColorArrayOne.Length; i++)
                {
                    convertedArray[i] = new Vector4(backgrounds[bgID].ColorArrayOne[i].r / 255f, backgrounds[bgID].ColorArrayOne[i].g / 255f, backgrounds[bgID].ColorArrayOne[i].b / 255f, backgrounds[bgID].ColorArrayOne[i].a / 255f);
                }

                ebMaterial.SetVectorArray("_ColorListOne", convertedArray);

                int colorArrayLength = backgrounds[bgID].ColorArrayOne.Length;

                ebMaterial.SetInt("_ColorListLengthOne", colorArrayLength);
            }

            if (backgrounds[bgID].ColorArrayTwo.Length != 0)
            {
                Vector4[] convertedArray = new Vector4[64];

                for (int i = 0; i < backgrounds[bgID].ColorArrayTwo.Length; i++)
                {
                    convertedArray[i] = new Vector4(backgrounds[bgID].ColorArrayTwo[i].r / 255f, backgrounds[bgID].ColorArrayTwo[i].g / 255f, backgrounds[bgID].ColorArrayTwo[i].b / 255f, backgrounds[bgID].ColorArrayTwo[i].a / 255f);
                }

                ebMaterial.SetVectorArray("_ColorListTwo", convertedArray);

                int colorArrayLength = backgrounds[bgID].ColorArrayTwo.Length;

                ebMaterial.SetInt("_ColorListLengthTwo", colorArrayLength);
            }

            ebMaterial.SetTextureOffset("_MainTex", Vector2.zero);

            if (backgroundDisplay == null)
            {
                backgroundDisplay = GetComponent<Image>();
            }
            backgroundDisplay.sprite = backgrounds[bgID].LayerOneTex;

            ebMaterial.SetTexture("_SecondTex", backgrounds[bgID].LayerTwoTex);
        }
        catch
        {
            Debug.Log("Invalid Background!");
        }

        if (ebMaterial != null)
        {
            //Layer One
            //Setting Layer One Oscillation Data
            ebMaterial.SetFloat("_HorOscXStrOne", backgrounds[bgID].LayerOne.HorOscXStr);
            ebMaterial.SetFloat("_HorOscXFrcOne", backgrounds[bgID].LayerOne.HorOscXFrc);
            ebMaterial.SetFloat("_HorOscXSpdOne", backgrounds[bgID].LayerOne.HorOscXSpd);

            ebMaterial.SetFloat("_VertOscXStrOne", backgrounds[bgID].LayerOne.VertOscXStr);
            ebMaterial.SetFloat("_VertOscXFrcOne", backgrounds[bgID].LayerOne.VertOscXFrc);
            ebMaterial.SetFloat("_VertOscXSpdOne", backgrounds[bgID].LayerOne.VertOscXSpd);

            ebMaterial.SetFloat("_HorOscYStrOne", backgrounds[bgID].LayerOne.HorOscYStr);
            ebMaterial.SetFloat("_HorOscYFrcOne", backgrounds[bgID].LayerOne.HorOscYFrc);
            ebMaterial.SetFloat("_HorOscYSpdOne", backgrounds[bgID].LayerOne.HorOscYSpd);

            ebMaterial.SetFloat("_VertOscYStrOne", backgrounds[bgID].LayerOne.VertOscYStr);
            ebMaterial.SetFloat("_VertOscYFrcOne", backgrounds[bgID].LayerOne.VertOscYFrc);
            ebMaterial.SetFloat("_VertOscYSpdOne", backgrounds[bgID].LayerOne.VertOscYSpd);

            //Check if allow Interleaved Oscillation
            ebMaterial.SetInt("_AllowInterOscLO", backgrounds[bgID].AllowInterOscLO ? 1 : 0);

            //Layer One Interleaved Oscillation Data
            ebMaterial.SetFloat("_HorOscXStrOneInt", backgrounds[bgID].LayerOneInter.HorOscXStr);
            ebMaterial.SetFloat("_HorOscXFrcOneInt", backgrounds[bgID].LayerOneInter.HorOscXFrc);
            ebMaterial.SetFloat("_HorOscXSpdOneInt", backgrounds[bgID].LayerOneInter.HorOscXSpd);

            ebMaterial.SetFloat("_VertOscXStrOneInt", backgrounds[bgID].LayerOneInter.VertOscXStr);
            ebMaterial.SetFloat("_VertOscXFrcOneInt", backgrounds[bgID].LayerOneInter.VertOscXFrc);
            ebMaterial.SetFloat("_VertOscXSpdOneInt", backgrounds[bgID].LayerOneInter.VertOscXSpd);

            ebMaterial.SetFloat("_HorOscYStrOneInt", backgrounds[bgID].LayerOneInter.HorOscYStr);
            ebMaterial.SetFloat("_HorOscYFrcOneInt", backgrounds[bgID].LayerOneInter.HorOscYFrc);
            ebMaterial.SetFloat("_HorOscYSpdOneInt", backgrounds[bgID].LayerOneInter.HorOscYSpd);

            ebMaterial.SetFloat("_VertOscYStrOneInt", backgrounds[bgID].LayerOneInter.VertOscYStr);
            ebMaterial.SetFloat("_VertOscYFrcOneInt", backgrounds[bgID].LayerOneInter.VertOscYFrc);
            ebMaterial.SetFloat("_VertOscYSpdOneInt", backgrounds[bgID].LayerOneInter.VertOscYSpd);

            

            ebMaterial.SetVector("_TextureOffsetOne", backgrounds[bgID].LayerOne.ScrollOffset / 10);
            ebMaterial.SetVector("_TextureOffsetOneInt", backgrounds[bgID].LayerOneInter.ScrollOffset / 10);

            //Layer 2
            ebMaterial.SetInt("_AllowLayering", backgrounds[bgID].AllowLayering ? 1 : 0);

            //Setting Layer Two Oscillation Data
            ebMaterial.SetFloat("_HorOscXStrTwo", backgrounds[bgID].LayerTwo.HorOscXStr);
            ebMaterial.SetFloat("_HorOscXFrcTwo", backgrounds[bgID].LayerTwo.HorOscXFrc);
            ebMaterial.SetFloat("_HorOscXSpdTwo", backgrounds[bgID].LayerTwo.HorOscXSpd);

            ebMaterial.SetFloat("_VertOscXStrTwo", backgrounds[bgID].LayerTwo.VertOscXStr);
            ebMaterial.SetFloat("_VertOscXFrcTwo", backgrounds[bgID].LayerTwo.VertOscXFrc);
            ebMaterial.SetFloat("_VertOscXSpdTwo", backgrounds[bgID].LayerTwo.VertOscXSpd);

            ebMaterial.SetFloat("_HorOscYStrTwo", backgrounds[bgID].LayerTwo.HorOscYStr);
            ebMaterial.SetFloat("_HorOscYFrcTwo", backgrounds[bgID].LayerTwo.HorOscYFrc);
            ebMaterial.SetFloat("_HorOscYSpdTwo", backgrounds[bgID].LayerTwo.HorOscYSpd);

            ebMaterial.SetFloat("_VertOscYStrTwo", backgrounds[bgID].LayerTwo.VertOscYStr);
            ebMaterial.SetFloat("_VertOscYFrcTwo", backgrounds[bgID].LayerTwo.VertOscYFrc);
            ebMaterial.SetFloat("_VertOscYSpdTwo", backgrounds[bgID].LayerTwo.VertOscYSpd);

            //Check if allow Interleaved Oscillation
            ebMaterial.SetInt("_AllowInterOscLT", backgrounds[bgID].AllowInterOscLT ? 1 : 0);

            //Layer Two Interleaved Oscillation Data
            ebMaterial.SetFloat("_HorOscXStrTwoInt", backgrounds[bgID].LayerTwoInter.HorOscXStr);
            ebMaterial.SetFloat("_HorOscXFrcTwoInt", backgrounds[bgID].LayerTwoInter.HorOscXFrc);
            ebMaterial.SetFloat("_HorOscXSpdTwoInt", backgrounds[bgID].LayerTwoInter.HorOscXSpd);

            ebMaterial.SetFloat("_VertOscXStrTwoInt", backgrounds[bgID].LayerTwoInter.VertOscXStr);
            ebMaterial.SetFloat("_VertOscXFrcTwoInt", backgrounds[bgID].LayerTwoInter.VertOscXFrc);
            ebMaterial.SetFloat("_VertOscXSpdTwoInt", backgrounds[bgID].LayerTwoInter.VertOscXSpd);

            ebMaterial.SetFloat("_HorOscYStrTwoInt", backgrounds[bgID].LayerTwoInter.HorOscYStr);
            ebMaterial.SetFloat("_HorOscYFrcTwoInt", backgrounds[bgID].LayerTwoInter.HorOscYFrc);
            ebMaterial.SetFloat("_HorOscYSpdTwoInt", backgrounds[bgID].LayerTwoInter.HorOscYSpd);

            ebMaterial.SetFloat("_VertOscYStrTwoInt", backgrounds[bgID].LayerTwoInter.VertOscYStr);
            ebMaterial.SetFloat("_VertOscYFrcTwoInt", backgrounds[bgID].LayerTwoInter.VertOscYFrc);
            ebMaterial.SetFloat("_VertOscYSpdTwoInt", backgrounds[bgID].LayerTwoInter.VertOscYSpd);

            //Color Cycle
            

            ebMaterial.SetVector("_TextureOffsetTwo", backgrounds[bgID].LayerTwo.ScrollOffset / 10);
            ebMaterial.SetVector("_TextureOffsetTwoInt", backgrounds[bgID].LayerTwoInter.ScrollOffset / 10);

            //scrollTimer += Time.deltaTime;

            //Vector2 offset = scrollOffset * scrollTimer * scrollSpeed;
            //Vector4 offset4 = new Vector4(offset.x, offset.y);

            //ebMaterial.SetVector("_TextureOffset", offset);

            //ebMaterial.SetTextureOffset("_MainTex", offset);
        }
        else
        {
            Debug.Log("Something Went Wrong with the renderer!");
        }

        update = false;
    }
}
