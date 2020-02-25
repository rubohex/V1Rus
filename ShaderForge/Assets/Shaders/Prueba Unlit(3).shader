// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|normal-6049-RGB,custl-9771-OUT,olwid-7711-OUT,olcol-9061-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7711,x:32535,y:33308,ptovrint:False,ptlb:Outline Width,ptin:_OutlineWidth,varname:node_7711,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:7718,x:31391,y:32593,ptovrint:False,ptlb:Ambient Occlusion,ptin:_AmbientOcclusion,varname:node_7718,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:7943,x:31620,y:32629,varname:node_7943,prsc:2|A-2982-OUT,B-7718-RGB;n:type:ShaderForge.SFN_ValueProperty,id:2982,x:31374,y:32286,ptovrint:False,ptlb:AO intensity,ptin:_AOintensity,varname:node_2982,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.6;n:type:ShaderForge.SFN_Tex2d,id:2862,x:32297,y:32718,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_2862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6049,x:31374,y:32401,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6049,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:9001,x:30974,y:33171,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_9001,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3464,x:32035,y:32844,varname:node_3464,prsc:2|A-7943-OUT,B-6066-OUT;n:type:ShaderForge.SFN_LightVector,id:2823,x:31095,y:33356,varname:node_2823,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:2656,x:31074,y:33497,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:3345,x:31352,y:33424,varname:node_3345,prsc:2,dt:0|A-2823-OUT,B-2656-OUT;n:type:ShaderForge.SFN_Max,id:6066,x:31757,y:33279,varname:node_6066,prsc:2|A-9935-OUT,B-8794-OUT;n:type:ShaderForge.SFN_Posterize,id:8794,x:31538,y:33512,varname:node_8794,prsc:2|IN-3345-OUT,STPS-5383-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5383,x:31092,y:33706,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:node_5383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Fresnel,id:4949,x:32294,y:32323,varname:node_4949,prsc:2|NRM-6049-RGB;n:type:ShaderForge.SFN_Clamp,id:9935,x:31406,y:33152,varname:node_9935,prsc:2|IN-9001-RGB,MIN-1152-OUT,MAX-8629-OUT;n:type:ShaderForge.SFN_Vector1,id:1152,x:31215,y:33064,varname:node_1152,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:8629,x:31212,y:33117,varname:node_8629,prsc:2,v1:1;n:type:ShaderForge.SFN_LightColor,id:5325,x:31424,y:33003,varname:node_5325,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:1842,x:30544,y:34090,varname:node_1842,prsc:2;n:type:ShaderForge.SFN_Color,id:1228,x:30835,y:34494,ptovrint:False,ptlb:Depth Color,ptin:_DepthColor,varname:node_1228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Blend,id:9061,x:31189,y:34392,varname:node_9061,prsc:2,blmd:1,clmp:True|SRC-1228-RGB,DST-7238-OUT;n:type:ShaderForge.SFN_Multiply,id:7238,x:30940,y:34242,varname:node_7238,prsc:2|A-2435-OUT,B-871-OUT;n:type:ShaderForge.SFN_Slider,id:871,x:30498,y:34332,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_871,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7521368,max:1;n:type:ShaderForge.SFN_Clamp,id:2435,x:30763,y:34155,varname:node_2435,prsc:2|IN-1842-OUT,MIN-8228-OUT,MAX-4288-OUT;n:type:ShaderForge.SFN_Vector1,id:4288,x:30519,y:34241,varname:node_4288,prsc:2,v1:100;n:type:ShaderForge.SFN_ValueProperty,id:8228,x:30564,y:34029,ptovrint:False,ptlb:DepthMinColor,ptin:_DepthMinColor,varname:node_8228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Blend,id:5028,x:32332,y:32969,varname:node_5028,prsc:2,blmd:5,clmp:True|SRC-3464-OUT,DST-9061-OUT;n:type:ShaderForge.SFN_Blend,id:9771,x:32519,y:32890,varname:node_9771,prsc:2,blmd:0,clmp:True|SRC-2862-RGB,DST-5028-OUT;proporder:7711-7718-2982-2862-6049-9001-5383-1228-871-8228;pass:END;sub:END;*/

Shader "Shader Forge/Prueba Unlit" {
    Properties {
        _OutlineWidth ("Outline Width", Float ) = 0.5
        _AmbientOcclusion ("Ambient Occlusion", 2D) = "white" {}
        _AOintensity ("AO intensity", Float ) = 0.6
        _Color ("Color", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Noise ("Noise", 2D) = "white" {}
        _Bands ("Bands", Float ) = 4
        _DepthColor ("Depth Color", Color) = (0.5,0.5,0.5,1)
        _Depth ("Depth", Range(0, 1)) = 0.7521368
        _DepthMinColor ("DepthMinColor", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _OutlineWidth;
            uniform float4 _DepthColor;
            uniform float _Depth;
            uniform float _DepthMinColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlineWidth,1) );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 node_9061 = saturate((_DepthColor.rgb*(clamp(max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs)) - _ProjectionParams.g),_DepthMinColor,100.0)*_Depth)));
                return fixed4(node_9061,0);
            }
            ENDCG
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
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _AmbientOcclusion; uniform float4 _AmbientOcclusion_ST;
            uniform float _AOintensity;
            uniform sampler2D _Color; uniform float4 _Color_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Bands;
            uniform float4 _DepthColor;
            uniform float _Depth;
            uniform float _DepthMinColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float4 _Color_var = tex2D(_Color,TRANSFORM_TEX(i.uv0, _Color));
                float4 _AmbientOcclusion_var = tex2D(_AmbientOcclusion,TRANSFORM_TEX(i.uv0, _AmbientOcclusion));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float3 node_9061 = saturate((_DepthColor.rgb*(clamp(max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs)) - _ProjectionParams.g),_DepthMinColor,100.0)*_Depth)));
                float3 finalColor = saturate(min(_Color_var.rgb,saturate(max((step(_AOintensity,_AmbientOcclusion_var.rgb)*max(clamp(_Noise_var.rgb,0.6,1.0),floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1))),node_9061))));
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
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _AmbientOcclusion; uniform float4 _AmbientOcclusion_ST;
            uniform float _AOintensity;
            uniform sampler2D _Color; uniform float4 _Color_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Bands;
            uniform float4 _DepthColor;
            uniform float _Depth;
            uniform float _DepthMinColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float4 _Color_var = tex2D(_Color,TRANSFORM_TEX(i.uv0, _Color));
                float4 _AmbientOcclusion_var = tex2D(_AmbientOcclusion,TRANSFORM_TEX(i.uv0, _AmbientOcclusion));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float3 node_9061 = saturate((_DepthColor.rgb*(clamp(max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs)) - _ProjectionParams.g),_DepthMinColor,100.0)*_Depth)));
                float3 finalColor = saturate(min(_Color_var.rgb,saturate(max((step(_AOintensity,_AmbientOcclusion_var.rgb)*max(clamp(_Noise_var.rgb,0.6,1.0),floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1))),node_9061))));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
