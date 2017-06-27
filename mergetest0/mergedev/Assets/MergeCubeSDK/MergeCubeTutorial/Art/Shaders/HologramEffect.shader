// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33210,y:32513,varname:node_3138,prsc:2|emission-9934-OUT,alpha-9881-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31948,y:32462,ptovrint:False,ptlb:color,ptin:_color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:456,x:31779,y:32279,varname:node_456,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:2181,x:31948,y:32279,ptovrint:False,ptlb:mainTexture,ptin:_mainTexture,varname:node_2181,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aeb2e665d0ef148f2961ed047ddc8d97,ntxv:0,isnm:False|UVIN-456-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1277,x:32160,y:32349,varname:node_1277,prsc:2|A-2181-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Tex2d,id:9728,x:32092,y:32732,ptovrint:False,ptlb:scanlineTexture,ptin:_scanlineTexture,varname:node_9728,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2184-OUT;n:type:ShaderForge.SFN_Blend,id:7522,x:32426,y:32434,varname:node_7522,prsc:2,blmd:10,clmp:False|SRC-9728-RGB,DST-1277-OUT;n:type:ShaderForge.SFN_Slider,id:793,x:31947,y:32920,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:node_793,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:9881,x:32379,y:32899,varname:node_9881,prsc:2|A-9728-A,B-793-OUT,C-8216-A;n:type:ShaderForge.SFN_Lerp,id:9934,x:32785,y:32408,varname:node_9934,prsc:2|A-1277-OUT,B-7522-OUT,T-274-OUT;n:type:ShaderForge.SFN_Slider,id:274,x:32362,y:32628,ptovrint:False,ptlb:overlayBlend,ptin:_overlayBlend,varname:node_274,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:8216,x:32072,y:33044,ptovrint:False,ptlb:opacityTexture,ptin:_opacityTexture,varname:node_8216,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2184-OUT;n:type:ShaderForge.SFN_NormalVector,id:3385,x:31131,y:32864,prsc:2,pt:False;n:type:ShaderForge.SFN_Transform,id:2466,x:31295,y:32864,varname:node_2466,prsc:2,tffrom:0,tfto:1|IN-3385-OUT;n:type:ShaderForge.SFN_ComponentMask,id:358,x:31458,y:32864,varname:node_358,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2466-XYZ;n:type:ShaderForge.SFN_RemapRange,id:2184,x:31622,y:32864,cmnt:MatcapUvs world to local,varname:node_2184,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-358-OUT;proporder:7241-2181-9728-793-274-8216;pass:END;sub:END;*/

Shader "Shader Forge/HologramEffect" {
    Properties {
        _color ("color", Color) = (1,1,1,1)
        _mainTexture ("mainTexture", 2D) = "white" {}
        _scanlineTexture ("scanlineTexture", 2D) = "white" {}
        _opacity ("opacity", Range(0, 1)) = 1
        _overlayBlend ("overlayBlend", Range(0, 1)) = 1
        _opacityTexture ("opacityTexture", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _color;
            uniform sampler2D _mainTexture; uniform float4 _mainTexture_ST;
            uniform sampler2D _scanlineTexture; uniform float4 _scanlineTexture_ST;
            uniform float _opacity;
            uniform float _overlayBlend;
            uniform sampler2D _opacityTexture; uniform float4 _opacityTexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _mainTexture_var = tex2D(_mainTexture,TRANSFORM_TEX(i.uv1, _mainTexture));
                float3 node_1277 = (_mainTexture_var.rgb*_color.rgb);
                float2 node_2184 = (mul( unity_WorldToObject, float4(i.normalDir,0) ).xyz.rgb.rg*0.5+0.5); // MatcapUvs world to local
                float4 _scanlineTexture_var = tex2D(_scanlineTexture,TRANSFORM_TEX(node_2184, _scanlineTexture));
                float3 emissive = lerp(node_1277,( node_1277 > 0.5 ? (1.0-(1.0-2.0*(node_1277-0.5))*(1.0-_scanlineTexture_var.rgb)) : (2.0*node_1277*_scanlineTexture_var.rgb) ),_overlayBlend);
                float3 finalColor = emissive;
                float4 _opacityTexture_var = tex2D(_opacityTexture,TRANSFORM_TEX(node_2184, _opacityTexture));
                return fixed4(finalColor,(_scanlineTexture_var.a*_opacity*_opacityTexture_var.a));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
