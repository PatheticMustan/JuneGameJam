Shader "Unlit/Earthbound"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _SecondTex ("Second Texture", 2D) = "white" {}

        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _SecondTex;

            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

            //Layer One
            //Oscillation Data
            float _HorOscXStrOne;
            float _HorOscXFrcOne;
            float _HorOscXSpdOne;

            float _VertOscXStrOne;
            float _VertOscXFrcOne;
            float _VertOscXSpdOne;

            float _HorOscYStrOne;
            float _HorOscYFrcOne;
            float _HorOscYSpdOne;

            float _VertOscYStrOne;
            float _VertOscYFrcOne;
            float _VertOscYSpdOne; 

            //Interleaved Oscillation Data
            bool _AllowInterOscLO;

            float _HorOscXStrOneInt;
            float _HorOscXFrcOneInt;
            float _HorOscXSpdOneInt;

            float _VertOscXStrOneInt;
            float _VertOscXFrcOneInt;
            float _VertOscXSpdOneInt;

            float _HorOscYStrOneInt;
            float _HorOscYFrcOneInt;
            float _HorOscYSpdOneInt;

            float _VertOscYStrOneInt;
            float _VertOscYFrcOneInt;
            float _VertOscYSpdOneInt; 

            //Color Data
            float4 _ColorListOne[64];

            uint _ColorListLengthOne = 0;
            uint _CycleOne = 0;

            //Texture Offset Data
            float2 _TextureOffsetOne;
            float2 _TextureOffsetOneInt;

            //Layer Two
            bool _AllowLayering;

            //Oscillation Data
            float _HorOscXStrTwo;
            float _HorOscXFrcTwo;
            float _HorOscXSpdTwo;

            float _VertOscXStrTwo;
            float _VertOscXFrcTwo;
            float _VertOscXSpdTwo;

            float _HorOscYStrTwo;
            float _HorOscYFrcTwo;
            float _HorOscYSpdTwo;

            float _VertOscYStrTwo;
            float _VertOscYFrcTwo;
            float _VertOscYSpdTwo; 

            //Interleaved Oscillation Data
            bool _AllowInterOscLT;

            float _HorOscXStrTwoInt;
            float _HorOscXFrcTwoInt;
            float _HorOscXSpdTwoInt;

            float _VertOscXStrTwoInt;
            float _VertOscXFrcTwoInt;
            float _VertOscXSpdTwoInt;

            float _HorOscYStrTwoInt;
            float _HorOscYFrcTwoInt;
            float _HorOscYSpdTwoInt;

            float _VertOscYStrTwoInt;
            float _VertOscYFrcTwoInt;
            float _VertOscYSpdTwoInt;
            
            //Color Data
            float4 _ColorListTwo[64];

            uint _ColorListLengthTwo = 0;
            uint _CycleTwo = 0;

            //Texture Offset Data
            float2 _TextureOffsetTwo;
            float2 _TextureOffsetTwoInt;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color;

                /*
                if (abs(IN.texcoord.y * _ScreenParams.y * 128) % 2 > 1)
                {
                color = tex2D(_MainTex, IN.texcoord + float2(1 * (acos(sin((1 - IN.worldPosition.y)/30 + _Time[3]))/ 40) * sin(_Time[2]), 0));
                }
                else
                {
                color = tex2D(_MainTex, IN.texcoord + float2(1 * (acos(cos(IN.worldPosition.y/30 + _Time[3]))/ 40) * sin(_Time[2]), 0));
                }
                */
                
                //_HorOscYFrcOne = 1;
                //_HorOscYSpdOne = 1;

                //If Statement used for Interleaved Oscillation
                if (abs((uint)(IN.texcoord.y * _ScreenParams.y)) % 2 >= 1 &&  _AllowInterOscLO)
                {
                    color = tex2D(_MainTex, IN.texcoord + 
                    float2(
                    ((_HorOscYFrcOneInt == 0 || _HorOscYStrOneInt == 0) ? 0 : (sin((IN.worldPosition.y)/_HorOscYFrcOneInt + _Time[3] * _HorOscYSpdOneInt)/_HorOscYStrOneInt)) +
                    ((_HorOscXFrcOneInt == 0 || _HorOscXStrOneInt == 0) ? 0 : (sin((IN.worldPosition.x)/_HorOscXFrcOneInt + _Time[3] * _HorOscXSpdOneInt)/_HorOscXStrOneInt)) 
                    , 
                    ((_VertOscYFrcOneInt == 0 || _VertOscYStrOneInt == 0) ? 0 : (sin((IN.worldPosition.y)/_VertOscYFrcOneInt + _Time[3] * _VertOscYSpdOneInt)/_VertOscYStrOneInt)) +
                    ((_VertOscXFrcOneInt == 0 || _VertOscXStrOneInt == 0) ? 0 : (sin((IN.worldPosition.x)/_VertOscXFrcOneInt + _Time[3] * _VertOscXSpdOneInt)/_VertOscXStrOneInt)) 
                    ) - _TextureOffsetOneInt * _Time[1]);
                }
                else
                {
                    color = tex2D(_MainTex, IN.texcoord + 
                    float2(
                    ((_HorOscYFrcOne == 0 || _HorOscYStrOne == 0) ? 0 : (sin((IN.worldPosition.y)/_HorOscYFrcOne + _Time[3] * _HorOscYSpdOne)/_HorOscYStrOne)) +
                    ((_HorOscXFrcOne == 0 || _HorOscXStrOne == 0) ? 0 : (sin((IN.worldPosition.x)/_HorOscXFrcOne + _Time[3] * _HorOscXSpdOne)/_HorOscXStrOne)) 
                    , 
                    ((_VertOscYFrcOne == 0 || _VertOscYStrOne == 0) ? 0 : (sin((IN.worldPosition.y)/_VertOscYFrcOne + _Time[3] * _VertOscYSpdOne)/_VertOscYStrOne)) +
                    ((_VertOscXFrcOne == 0 || _VertOscXStrOne == 0) ? 0 : (sin((IN.worldPosition.x)/_VertOscXFrcOne + _Time[3] * _VertOscXSpdOne)/_VertOscXStrOne)) 
                    ) - _TextureOffsetOne * _Time[1]);
                }

                for (uint i = 0, ilen = _ColorListLengthOne; i <= ilen; i++)
                {
                    if (all(_ColorListOne[i].rgb == color.rgb))
                    {
                        color.rgb = _ColorListOne[(i + _CycleOne) % _ColorListLengthOne].rgb;
                        break;
                    }
				}

                /*

                */
                //Layer 2 Check
                if (_AllowLayering)
                {
                    half4 layTwoCol;    

                    if (abs(IN.texcoord.y * _ScreenParams.y) % 2 >= 1 &&  _AllowInterOscLT)
                    {
                        layTwoCol = tex2D(_SecondTex, IN.texcoord + 
                        float2(
                        ((_HorOscYFrcTwoInt == 0 || _HorOscYStrTwoInt == 0) ? 0 : (sin((IN.worldPosition.y)/_HorOscYFrcTwoInt + _Time[3] * _HorOscYSpdTwoInt)/_HorOscYStrTwoInt)) +
                        ((_HorOscXFrcTwoInt == 0 || _HorOscXStrTwoInt == 0) ? 0 : (sin((IN.worldPosition.x)/_HorOscXFrcTwoInt + _Time[3] * _HorOscXSpdTwoInt)/_HorOscXStrTwoInt)) 
                        , 
                        ((_VertOscYFrcTwo == 0 || _VertOscYStrTwoInt == 0) ? 0 : (sin((IN.worldPosition.y)/_VertOscYFrcTwoInt + _Time[3] * _VertOscYSpdTwoInt)/_VertOscYStrTwoInt)) +
                        ((_VertOscXFrcTwo == 0 || _VertOscXStrTwoInt == 0) ? 0 : (sin((IN.worldPosition.x)/_VertOscXFrcTwoInt + _Time[3] * _VertOscXSpdTwoInt)/_VertOscXStrTwoInt)) 
                        ) - _TextureOffsetTwoInt * _Time[1]);
                    }
                    else
                    {
                        layTwoCol = tex2D(_SecondTex, IN.texcoord + 
                        float2(
                        ((_HorOscYFrcTwo == 0 || _HorOscYStrTwo == 0) ? 0 : (sin((IN.worldPosition.y)/_HorOscYFrcTwo + _Time[3] * _HorOscYSpdTwo)/_HorOscYStrTwo)) +
                        ((_HorOscXFrcTwo == 0 || _HorOscXStrTwo == 0) ? 0 : (sin((IN.worldPosition.x)/_HorOscXFrcTwo + _Time[3] * _HorOscXSpdTwo)/_HorOscXStrTwo)) 
                        , 
                        ((_VertOscYFrcTwo == 0 || _VertOscYStrTwo == 0) ? 0 : (sin((IN.worldPosition.y)/_VertOscYFrcTwo + _Time[3] * _VertOscYSpdTwo)/_VertOscYStrTwo)) +
                        ((_VertOscXFrcTwo == 0 || _VertOscXStrTwo == 0) ? 0 : (sin((IN.worldPosition.x)/_VertOscXFrcTwo + _Time[3] * _VertOscXSpdTwo)/_VertOscXStrTwo)) 
                        ) - _TextureOffsetTwo * _Time[1]);
                    }

                    for (uint i = 0, ilen = _ColorListLengthTwo; i <= ilen; i++)
                    {
                        if (all(_ColorListTwo[i].rgb == layTwoCol.rgb))
                        {
                            layTwoCol.rgb = _ColorListTwo[(i + _CycleTwo) % _ColorListLengthTwo].rgb;
                            break;
                        }
				    }

                    color = (layTwoCol + color) / 2;

                    
				}
               
               /*
                half4 layTwoCol;

                if (abs(IN.texcoord.y * _ScreenParams.y * 64) % 2 >= 1)
                {
                layTwoCol = tex2D(_SecondTex, IN.texcoord + float2(sin((IN.worldPosition.y)/30 + _Time[3])/30, sin((IN.worldPosition.y)/30 + _Time[3] )/10) - _TextureOffsetOne * _Time[1]);
                }
                else
                {
                layTwoCol = tex2D(_SecondTex, IN.texcoord + float2(-1 * sin((IN.worldPosition.y)/30 + _Time[3])/30, 1 * sin((IN.worldPosition.y)/30 + _Time[3])/10) - _TextureOffsetOned * _Time[1]);
                }

                for (uint j = 0, jlen = _ColorListLengthOne; j <= ilen;j++)
                {
                    if (all(_ColorListOne[j].rgb == layTwoCol.rgb))
                    {
                        layTwoCol.rgb = _ColorListOne[(j + _CycleOne) % _ColorListLengthOne].rgb;
                        break;
                    }
				}
                */

                
                //*/



                //color.a = ;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
        ENDCG
        }
    }
}