// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|normal-6049-RGB,custl-3464-OUT,olwid-7711-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7711,x:32562,y:33032,ptovrint:False,ptlb:Outline Width,ptin:_OutlineWidth,varname:node_7711,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:7718,x:31943,y:32677,ptovrint:False,ptlb:Ambient Occlusion,ptin:_AmbientOcclusion,varname:node_7718,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7175-UVOUT;n:type:ShaderForge.SFN_Step,id:7943,x:32120,y:32677,varname:node_7943,prsc:2|A-2982-OUT,B-7718-RGB;n:type:ShaderForge.SFN_ValueProperty,id:2982,x:31943,y:32550,ptovrint:False,ptlb:AO intensity,ptin:_AOintensity,varname:node_2982,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:2862,x:31975,y:32891,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_2862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7175-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3092,x:32338,y:32832,varname:node_3092,prsc:2|A-7943-OUT,B-2862-RGB;n:type:ShaderForge.SFN_TexCoord,id:4831,x:33247,y:31922,varname:node_4831,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_TexCoord,id:3001,x:33311,y:31986,varname:node_3001,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_TexCoord,id:7175,x:31656,y:32685,varname:node_7175,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:6049,x:32155,y:32471,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6049,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7175-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9001,x:32122,y:33056,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_9001,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3464,x:32509,y:32876,varname:node_3464,prsc:2|A-3092-OUT,B-6066-OUT;n:type:ShaderForge.SFN_LightVector,id:2823,x:31706,y:33094,varname:node_2823,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:2656,x:31706,y:33249,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:3345,x:31873,y:33190,varname:node_3345,prsc:2,dt:0|A-2823-OUT,B-2656-OUT;n:type:ShaderForge.SFN_Max,id:6066,x:32344,y:33160,varname:node_6066,prsc:2|A-9001-RGB,B-8794-OUT;n:type:ShaderForge.SFN_Posterize,id:8794,x:32088,y:33268,varname:node_8794,prsc:2|IN-3345-OUT,STPS-5383-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5383,x:31830,y:33422,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:node_5383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;proporder:7711-7718-2982-2862-6049-9001-5383;pass:END;sub:END;*/

Shader "Shader Forge/Prueba Unlit" {
    Properties {
        _OutlineWidth ("Outline Width", Float ) = 0.5
        _AmbientOcclusion ("Ambient Occlusion", 2D) = "white" {}
        _AOintensity ("AO intensity", Float ) = 0.5
        _Color ("Color", 2D) = "white" {}
        _Normal ("Normal", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _Bands ("Bands", Float ) = 4
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
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlineWidth,1) );
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
            uniform sampler2D _AmbientOcclusion; uniform float4 _AmbientOcclusion_ST;
            uniform float _AOintensity;
            uniform sampler2D _Color; uniform float4 _Color_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Bands;
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
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float4 _Normal_var = tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float4 _AmbientOcclusion_var = tex2D(_AmbientOcclusion,TRANSFORM_TEX(i.uv0, _AmbientOcclusion));
                float4 _Color_var = tex2D(_Color,TRANSFORM_TEX(i.uv0, _Color));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float3 finalColor = ((step(_AOintensity,_AmbientOcclusion_var.rgb)*_Color_var.rgb)*max(_Noise_var.rgb,floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1)));
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
            uniform sampler2D _AmbientOcclusion; uniform float4 _AmbientOcclusion_ST;
            uniform float _AOintensity;
            uniform sampler2D _Color; uniform float4 _Color_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Bands;
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
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float4 _Normal_var = tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float4 _AmbientOcclusion_var = tex2D(_AmbientOcclusion,TRANSFORM_TEX(i.uv0, _AmbientOcclusion));
                float4 _Color_var = tex2D(_Color,TRANSFORM_TEX(i.uv0, _Color));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float3 finalColor = ((step(_AOintensity,_AmbientOcclusion_var.rgb)*_Color_var.rgb)*max(_Noise_var.rgb,floor(dot(lightDirection,i.normalDir) * _Bands) / (_Bands - 1)));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
