// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33311,y:32774,varname:node_3138,prsc:2|custl-8382-OUT;n:type:ShaderForge.SFN_Tex2d,id:490,x:31508,y:33203,ptovrint:False,ptlb:Trama,ptin:_Trama,varname:node_490,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:697cd8aee53a5f546b7ee542a088a53b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:8382,x:32949,y:32895,varname:node_8382,prsc:2|A-1174-OUT,B-983-OUT;n:type:ShaderForge.SFN_Slider,id:3833,x:31640,y:32721,ptovrint:False,ptlb:Valor Trama,ptin:_ValorTrama,varname:node_3833,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:1.5,max:1.5;n:type:ShaderForge.SFN_LightVector,id:815,x:30485,y:32834,varname:node_815,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:469,x:30485,y:33031,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:5336,x:30741,y:32916,varname:node_5336,prsc:2,dt:0|A-815-OUT,B-469-OUT;n:type:ShaderForge.SFN_Posterize,id:182,x:30987,y:32916,varname:node_182,prsc:2|IN-5336-OUT,STPS-8962-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8962,x:30760,y:33183,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:node_8962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Clamp01,id:983,x:31803,y:33189,varname:node_983,prsc:2|IN-490-RGB;n:type:ShaderForge.SFN_Blend,id:7550,x:31680,y:32921,varname:node_7550,prsc:2,blmd:18,clmp:True|SRC-182-OUT,DST-349-OUT;n:type:ShaderForge.SFN_Vector1,id:349,x:31477,y:33016,varname:node_349,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:1174,x:32038,y:32769,varname:node_1174,prsc:2|A-3833-OUT,B-7550-OUT;proporder:490-3833-8962;pass:END;sub:END;*/

Shader "Shader Forge/Prueba trama" {
    Properties {
        _Trama ("Trama", 2D) = "white" {}
        _ValorTrama ("Valor Trama", Range(-0.5, 1.5)) = 1.5
        _Bands ("Bands", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Trama; uniform float4 _Trama_ST;
            uniform float _ValorTrama;
            uniform float _Bands;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float4 _Trama_var = tex2D(_Trama,TRANSFORM_TEX(i.uv0, _Trama));
                float3 finalColor = step((_ValorTrama+saturate((0.5 - 2.0*(floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1)-0.5)*(1.0-0.5)))),saturate(_Trama_var.rgb));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Trama; uniform float4 _Trama_ST;
            uniform float _ValorTrama;
            uniform float _Bands;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float4 _Trama_var = tex2D(_Trama,TRANSFORM_TEX(i.uv0, _Trama));
                float3 finalColor = step((_ValorTrama+saturate((0.5 - 2.0*(floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1)-0.5)*(1.0-0.5)))),saturate(_Trama_var.rgb));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
