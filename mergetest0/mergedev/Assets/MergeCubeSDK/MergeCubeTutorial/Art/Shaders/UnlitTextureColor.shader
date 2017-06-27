Shader "Custom/Unlit Texture Color" {
     Properties {
         _Color ("Main Color", Color) = (1,1,1,1)
         _MainTex ("Base (RGB)", 2D) = "white" {}
     }
     Category {
        SubShader {
             Tags { "Queue" = "Geometry" }
             Pass {
                 SetTexture [_MainTex] {
                     constantColor [_Color]
                     Combine texture * constant DOUBLE
                 }
             }
         }
     }
 }