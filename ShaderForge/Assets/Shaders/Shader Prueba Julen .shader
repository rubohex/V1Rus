// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|normal-4614-RGB,custl-580-OUT,olwid-6030-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3906,x:31592,y:32719,ptovrint:False,ptlb:AO Intensity,ptin:_AOIntensity,varname:node_3906,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:4614,x:32316,y:32404,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_4614,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:6228,x:31592,y:32854,ptovrint:False,ptlb:Ambient_Occlusion,ptin:_Ambient_Occlusion,varname:node_6228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1153,x:32270,y:32631,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1153,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:5271,x:31829,y:32731,varname:node_5271,prsc:2|A-3906-OUT,B-6228-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4560,x:31150,y:33687,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:node_4560,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Tex2d,id:7015,x:31145,y:33037,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_7015,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:08e1767559b394948a6d3265e079e52a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Clamp,id:5700,x:31484,y:33182,varname:node_5700,prsc:2|IN-7015-RGB,MIN-1378-OUT,MAX-8961-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1378,x:31145,y:33233,ptovrint:False,ptlb:Noise_min,ptin:_Noise_min,varname:node_1378,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.6;n:type:ShaderForge.SFN_ValueProperty,id:8961,x:31145,y:33327,ptovrint:False,ptlb:Noise_max,ptin:_Noise_max,varname:node_8961,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_LightVector,id:8185,x:30933,y:33385,varname:node_8185,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:7736,x:30939,y:33518,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:2260,x:31150,y:33460,varname:node_2260,prsc:2,dt:0|A-8185-OUT,B-7736-OUT;n:type:ShaderForge.SFN_Posterize,id:2940,x:31420,y:33526,varname:node_2940,prsc:2|IN-2260-OUT,STPS-4560-OUT;n:type:ShaderForge.SFN_Multiply,id:9485,x:32088,y:32827,varname:node_9485,prsc:2|A-5271-OUT,B-2142-OUT;n:type:ShaderForge.SFN_Blend,id:8812,x:32334,y:32910,varname:node_8812,prsc:2,blmd:6,clmp:True|SRC-9485-OUT,DST-8496-OUT;n:type:ShaderForge.SFN_Slider,id:9121,x:30729,y:34402,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_9121,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:3090,x:30886,y:34168,ptovrint:False,ptlb:DepthMinColor,ptin:_DepthMinColor,varname:node_3090,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:7011,x:30886,y:34279,ptovrint:False,ptlb:DepthMaxColor,ptin:_DepthMaxColor,varname:node_7011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:1529,x:31273,y:33942,ptovrint:False,ptlb:Depth_Color,ptin:_Depth_Color,varname:node_1529,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_SceneDepth,id:5547,x:30900,y:34021,varname:node_5547,prsc:2;n:type:ShaderForge.SFN_Clamp,id:4726,x:31108,y:34134,varname:node_4726,prsc:2|IN-5547-OUT,MIN-3090-OUT,MAX-7011-OUT;n:type:ShaderForge.SFN_Multiply,id:9375,x:31265,y:34226,varname:node_9375,prsc:2|A-4726-OUT,B-9121-OUT;n:type:ShaderForge.SFN_Blend,id:8496,x:31531,y:34052,varname:node_8496,prsc:2,blmd:1,clmp:True|SRC-1529-RGB,DST-9375-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6030,x:32535,y:33152,ptovrint:False,ptlb:Outline_Width,ptin:_Outline_Width,varname:node_6030,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Blend,id:2142,x:31728,y:33192,varname:node_2142,prsc:2,blmd:6,clmp:True|SRC-5700-OUT,DST-2940-OUT;n:type:ShaderForge.SFN_Blend,id:580,x:32526,y:32813,varname:node_580,prsc:2,blmd:1,clmp:True|SRC-1153-RGB,DST-8812-OUT;proporder:3906-6228-4614-1153-4560-7015-1378-8961-9121-3090-7011-1529-6030;pass:END;sub:END;*/

Shader "Shader Forge/Shader Prueba Julen " {
    Properties {
        _AOIntensity ("AO Intensity", Float ) = 0.5
        _Ambient_Occlusion ("Ambient_Occlusion", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Color ("Color", 2D) = "white" {}
        _Bands ("Bands", Float ) = 4
        _Noise ("Noise", 2D) = "white" {}
        _Noise_min ("Noise_min", Float ) = 0.6
        _Noise_max ("Noise_max", Float ) = 1
        _Depth ("Depth", Range(0, 1)) = 0
        _DepthMinColor ("DepthMinColor", Float ) = 0
        _DepthMaxColor ("DepthMaxColor", Float ) = 0
        _Depth_Color ("Depth_Color", Color) = (0.5,0.5,0.5,1)
        _Outline_Width ("Outline_Width", Float ) = 0
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
            uniform float _Outline_Width;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_Outline_Width,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(float3(0,0,0),0);
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
            uniform float _AOIntensity;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Ambient_Occlusion; uniform float4 _Ambient_Occlusion_ST;
            uniform sampler2D _Color; uniform float4 _Color_ST;
            uniform float _Bands;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Noise_min;
            uniform float _Noise_max;
            uniform float _Depth;
            uniform float _DepthMinColor;
            uniform float _DepthMaxColor;
            uniform float4 _Depth_Color;
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
                float4 _Ambient_Occlusion_var = tex2D(_Ambient_Occlusion,TRANSFORM_TEX(i.uv0, _Ambient_Occlusion));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float3 node_5700 = clamp(_Noise_var.rgb,_Noise_min,_Noise_max);
                float node_2940 = floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1);
                float3 node_9485 = (step(_AOIntensity,_Ambient_Occlusion_var.rgb)*saturate((1.0-(1.0-node_5700)*(1.0-node_2940))));
                float3 node_8496 = saturate((_Depth_Color.rgb*(clamp(max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs)) - _ProjectionParams.g),_DepthMinColor,_DepthMaxColor)*_Depth)));
                float3 node_8812 = saturate((1.0-(1.0-node_9485)*(1.0-node_8496)));
                float3 finalColor = saturate((_Color_var.rgb*node_8812));
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
            uniform float _AOIntensity;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Ambient_Occlusion; uniform float4 _Ambient_Occlusion_ST;
            uniform sampler2D _Color; uniform float4 _Color_ST;
            uniform float _Bands;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Noise_min;
            uniform float _Noise_max;
            uniform float _Depth;
            uniform float _DepthMinColor;
            uniform float _DepthMaxColor;
            uniform float4 _Depth_Color;
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
                float4 _Ambient_Occlusion_var = tex2D(_Ambient_Occlusion,TRANSFORM_TEX(i.uv0, _Ambient_Occlusion));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float3 node_5700 = clamp(_Noise_var.rgb,_Noise_min,_Noise_max);
                float node_2940 = floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1);
                float3 node_9485 = (step(_AOIntensity,_Ambient_Occlusion_var.rgb)*saturate((1.0-(1.0-node_5700)*(1.0-node_2940))));
                float3 node_8496 = saturate((_Depth_Color.rgb*(clamp(max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs)) - _ProjectionParams.g),_DepthMinColor,_DepthMaxColor)*_Depth)));
                float3 node_8812 = saturate((1.0-(1.0-node_9485)*(1.0-node_8496)));
                float3 finalColor = saturate((_Color_var.rgb*node_8812));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
