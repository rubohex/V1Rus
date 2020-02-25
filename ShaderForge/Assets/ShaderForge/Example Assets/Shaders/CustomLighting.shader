// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:0,x:35814,y:32000,varname:node_0,prsc:2|normal-83-RGB,custl-5653-OUT,olwid-5312-OUT,olcol-6015-RGB;n:type:ShaderForge.SFN_LightAttenuation,id:37,x:33302,y:31448,varname:node_37,prsc:2;n:type:ShaderForge.SFN_Dot,id:40,x:30119,y:32896,varname:node_40,prsc:2,dt:1|A-42-OUT,B-41-OUT;n:type:ShaderForge.SFN_NormalVector,id:41,x:29910,y:32990,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:42,x:29910,y:32869,varname:node_42,prsc:2;n:type:ShaderForge.SFN_Dot,id:52,x:30119,y:33069,varname:node_52,prsc:2,dt:1|A-41-OUT,B-62-OUT;n:type:ShaderForge.SFN_Add,id:55,x:33622,y:32642,varname:node_55,prsc:2|A-84-OUT,B-4124-OUT;n:type:ShaderForge.SFN_Power,id:58,x:30271,y:33207,cmnt:Specular Light,varname:node_58,prsc:2|VAL-52-OUT,EXP-244-OUT;n:type:ShaderForge.SFN_HalfVector,id:62,x:29910,y:33129,varname:node_62,prsc:2;n:type:ShaderForge.SFN_LightColor,id:63,x:34297,y:32316,varname:node_63,prsc:2;n:type:ShaderForge.SFN_Color,id:80,x:32775,y:31774,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6544118,c2:0.8426978,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:82,x:32507,y:31522,ptovrint:False,ptlb:Albedo+AO,ptin:_AlbedoAO,varname:_Diffuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8993b617f08498f43adcbd90697f1c5d,ntxv:0,isnm:False|UVIN-272-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:83,x:35626,y:32025,ptovrint:False,ptlb:Normals,ptin:_Normals,varname:_Normals,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6dfb00dbee6bc044a8a3bb22e56e064,ntxv:3,isnm:True|UVIN-272-UVOUT;n:type:ShaderForge.SFN_Multiply,id:84,x:33204,y:32120,cmnt:Diffuse Light,varname:node_84,prsc:2|A-82-RGB,B-80-RGB,C-2337-OUT;n:type:ShaderForge.SFN_AmbientLight,id:187,x:33014,y:32667,varname:node_187,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:216,x:30321,y:33069,ptovrint:False,ptlb:Cel Shading Bands,ptin:_CelShadingBands,varname:_Bands,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Slider,id:239,x:29172,y:33237,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2940849,max:1;n:type:ShaderForge.SFN_Add,id:240,x:29910,y:33286,varname:node_240,prsc:2|A-242-OUT,B-241-OUT;n:type:ShaderForge.SFN_Vector1,id:241,x:29742,y:33374,varname:node_241,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:242,x:29742,y:33224,varname:node_242,prsc:2|A-239-OUT,B-243-OUT;n:type:ShaderForge.SFN_Vector1,id:243,x:29329,y:33307,varname:node_243,prsc:2,v1:10;n:type:ShaderForge.SFN_Exp,id:244,x:30081,y:33286,varname:node_244,prsc:2,et:1|IN-240-OUT;n:type:ShaderForge.SFN_Posterize,id:264,x:30464,y:32649,varname:node_264,prsc:2|IN-40-OUT,STPS-216-OUT;n:type:ShaderForge.SFN_TexCoord,id:272,x:32059,y:31228,varname:node_272,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:6015,x:35613,y:32721,ptovrint:False,ptlb:Outline color,ptin:_Outlinecolor,varname:node_6015,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1698113,c2:0.1698113,c3:0.1698113,c4:1;n:type:ShaderForge.SFN_SceneDepth,id:6915,x:30126,y:31398,varname:node_6915,prsc:2;n:type:ShaderForge.SFN_Blend,id:7576,x:31106,y:31394,varname:node_7576,prsc:2,blmd:1,clmp:True|SRC-9921-RGB,DST-1148-OUT;n:type:ShaderForge.SFN_Color,id:9921,x:30685,y:31224,ptovrint:False,ptlb:Color Distancia Camara,ptin:_ColorDistanciaCamara,varname:node_9921,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5566038,c2:0.5566038,c3:0.5566038,c4:1;n:type:ShaderForge.SFN_Multiply,id:1148,x:30905,y:31428,varname:node_1148,prsc:2|A-9249-OUT,B-4707-OUT;n:type:ShaderForge.SFN_Tex2d,id:3145,x:31330,y:31932,ptovrint:False,ptlb:textura para la sombra,ptin:_texturaparalasombra,varname:node_3145,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fef47ba7891e8224d91a4dad8e08bc11,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:4707,x:30555,y:31572,ptovrint:False,ptlb:Distancia degradado,ptin:_Distanciadegradado,varname:node_4707,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Clamp,id:9249,x:30490,y:31355,varname:node_9249,prsc:2|IN-6915-OUT,MIN-8742-OUT,MAX-4093-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4093,x:30266,y:31477,ptovrint:False,ptlb:Maximo Degradado Distancia,ptin:_MaximoDegradadoDistancia,varname:node_4093,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:100;n:type:ShaderForge.SFN_ValueProperty,id:8742,x:30269,y:31223,ptovrint:False,ptlb:Minimo Degrafado Distancia,ptin:_MinimoDegrafadoDistancia,varname:node_8742,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5312,x:35613,y:32566,ptovrint:False,ptlb:outline with,ptin:_outlinewith,varname:node_5312,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Blend,id:5988,x:32806,y:32146,varname:node_5988,prsc:2,blmd:5,clmp:True|SRC-3177-OUT,DST-6976-OUT;n:type:ShaderForge.SFN_Slider,id:5475,x:32993,y:32828,ptovrint:False,ptlb:Ambient light,ptin:_Ambientlight,varname:_Distanciadegradado_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5643086,max:1;n:type:ShaderForge.SFN_Multiply,id:4124,x:33370,y:32691,varname:node_4124,prsc:2|A-187-RGB,B-5475-OUT;n:type:ShaderForge.SFN_Slider,id:3092,x:33325,y:31789,ptovrint:False,ptlb:Light Atten,ptin:_LightAtten,varname:_Ambientlight_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5643086,max:1;n:type:ShaderForge.SFN_Tex2d,id:7871,x:31441,y:32143,ptovrint:False,ptlb:textura para la sombra de las normales,ptin:_texturaparalasombradelasnormales,varname:_texturaparalasombra_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:52faf797b08ecc24081738537b00a003,ntxv:1,isnm:False|UVIN-4931-OUT;n:type:ShaderForge.SFN_Blend,id:776,x:31995,y:32193,varname:node_776,prsc:2,blmd:5,clmp:True|SRC-7871-RGB,DST-7576-OUT;n:type:ShaderForge.SFN_Blend,id:3177,x:32344,y:32088,varname:node_3177,prsc:2,blmd:10,clmp:True|SRC-776-OUT,DST-3145-RGB;n:type:ShaderForge.SFN_Posterize,id:5124,x:30683,y:33134,varname:node_5124,prsc:2|IN-58-OUT,STPS-216-OUT;n:type:ShaderForge.SFN_Divide,id:4931,x:31221,y:32226,varname:node_4931,prsc:2|A-4412-UVOUT,B-52-OUT;n:type:ShaderForge.SFN_Slider,id:7975,x:32562,y:33714,ptovrint:False,ptlb:rimlight size,ptin:_rimlightsize,varname:node_7975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:5;n:type:ShaderForge.SFN_Fresnel,id:4968,x:33022,y:33628,varname:node_4968,prsc:2|EXP-7975-OUT;n:type:ShaderForge.SFN_Posterize,id:5827,x:33249,y:33576,varname:node_5827,prsc:2|IN-4968-OUT,STPS-6390-OUT;n:type:ShaderForge.SFN_Color,id:5153,x:32811,y:33462,ptovrint:False,ptlb:rimlight color,ptin:_rimlightcolor,varname:node_5153,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6946412,c2:0.3551086,c3:0.7924528,c4:1;n:type:ShaderForge.SFN_Blend,id:6662,x:33423,y:33397,varname:node_6662,prsc:2,blmd:0,clmp:True|SRC-5153-RGB,DST-5827-OUT;n:type:ShaderForge.SFN_Slider,id:6390,x:32901,y:33846,ptovrint:False,ptlb:rimlight hardness,ptin:_rimlighthardness,varname:_rimlightsize_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:100;n:type:ShaderForge.SFN_Add,id:302,x:34130,y:32925,varname:node_302,prsc:2|A-55-OUT,B-6662-OUT;n:type:ShaderForge.SFN_Posterize,id:8750,x:33953,y:31768,varname:node_8750,prsc:2|IN-1194-OUT,STPS-8157-OUT;n:type:ShaderForge.SFN_Tex2d,id:3245,x:32533,y:30598,ptovrint:False,ptlb:Curvature,ptin:_Curvature,varname:node_3245,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:5653,x:35626,y:32211,varname:node_5653,prsc:2,blmd:1,clmp:True|SRC-3787-OUT,DST-1965-OUT;n:type:ShaderForge.SFN_Step,id:4792,x:32765,y:30432,varname:node_4792,prsc:2|A-3626-OUT,B-3245-RGB;n:type:ShaderForge.SFN_ValueProperty,id:3626,x:32516,y:30393,ptovrint:False,ptlb:Intensidad Curvature,ptin:_IntensidadCurvature,varname:node_3626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:1592,x:32358,y:30936,ptovrint:False,ptlb:Ambient Occlusion,ptin:_AmbientOcclusion,varname:node_1592,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:2395,x:32817,y:30799,varname:node_2395,prsc:2|A-8104-OUT,B-82-A;n:type:ShaderForge.SFN_ValueProperty,id:8104,x:32472,y:30805,ptovrint:False,ptlb:Intensidad AO,ptin:_IntensidadAO,varname:node_8104,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Blend,id:1965,x:32992,y:30530,varname:node_1965,prsc:2,blmd:1,clmp:True|SRC-4792-OUT,DST-2395-OUT;n:type:ShaderForge.SFN_Blend,id:1194,x:33718,y:31662,varname:node_1194,prsc:2,blmd:1,clmp:True|SRC-7407-OUT,DST-3092-OUT;n:type:ShaderForge.SFN_Clamp,id:7407,x:33482,y:31615,varname:node_7407,prsc:2|IN-37-OUT,MIN-1725-OUT,MAX-9166-OUT;n:type:ShaderForge.SFN_Vector1,id:1725,x:33300,y:31602,varname:node_1725,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Vector1,id:9166,x:33316,y:31674,varname:node_9166,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:5843,x:34269,y:32039,varname:node_5843,prsc:2|A-82-RGB,B-8750-OUT,T-3909-OUT;n:type:ShaderForge.SFN_Slider,id:3909,x:33677,y:32009,ptovrint:False,ptlb:Intensidad Sombra,ptin:_IntensidadSombra,varname:node_3909,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:8157,x:33553,y:31924,ptovrint:False,ptlb:Franjas Sombra,ptin:_FranjasSombra,varname:node_8157,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Blend,id:1171,x:34858,y:32029,varname:node_1171,prsc:2,blmd:5,clmp:True|SRC-7335-OUT,DST-258-RGB;n:type:ShaderForge.SFN_Tex2d,id:258,x:34114,y:32144,ptovrint:False,ptlb:Textura sombra proyectada,ptin:_Texturasombraproyectada,varname:node_258,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Divide,id:1978,x:33755,y:32173,varname:node_1978,prsc:2|A-272-UVOUT,B-62-OUT;n:type:ShaderForge.SFN_TexCoord,id:4412,x:30977,y:32119,varname:node_4412,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Fresnel,id:6830,x:33640,y:30924,varname:node_6830,prsc:2|EXP-26-OUT;n:type:ShaderForge.SFN_Posterize,id:966,x:33931,y:31032,varname:node_966,prsc:2|IN-6830-OUT,STPS-6869-OUT;n:type:ShaderForge.SFN_Color,id:8711,x:33674,y:31180,ptovrint:False,ptlb:rimlight color_sombra,ptin:_rimlightcolor_sombra,varname:_rimlightcolor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Blend,id:8949,x:34123,y:31402,varname:node_8949,prsc:2,blmd:0,clmp:True|SRC-8711-RGB,DST-966-OUT;n:type:ShaderForge.SFN_Slider,id:6869,x:33197,y:31130,ptovrint:False,ptlb:rimlight hardness_sombra,ptin:_rimlighthardness_sombra,varname:_rimlighthardness_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:100;n:type:ShaderForge.SFN_Slider,id:26,x:33121,y:30910,ptovrint:False,ptlb:rimlight size sombra,ptin:_rimlightsizesombra,varname:node_26,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:5;n:type:ShaderForge.SFN_Blend,id:7335,x:34621,y:31569,varname:node_7335,prsc:2,blmd:5,clmp:True|SRC-8949-OUT,DST-5843-OUT;n:type:ShaderForge.SFN_Blend,id:8453,x:34705,y:32653,varname:node_8453,prsc:2,blmd:5,clmp:True|SRC-8949-OUT,DST-302-OUT;n:type:ShaderForge.SFN_Blend,id:4991,x:34938,y:32334,varname:node_4991,prsc:2,blmd:19,clmp:True|SRC-5843-OUT,DST-8453-OUT;n:type:ShaderForge.SFN_Blend,id:4179,x:35097,y:32865,varname:node_4179,prsc:2,blmd:5,clmp:True|SRC-4991-OUT,DST-302-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2358,x:30917,y:32981,ptovrint:False,ptlb:Valor Franja 1,ptin:_ValorFranja1,varname:node_2358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4112,x:31173,y:33078,ptovrint:False,ptlb:Valor Franja 2,ptin:_ValorFranja2,varname:node_4112,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:4259,x:30939,y:33321,ptovrint:False,ptlb:Franja1,ptin:_Franja1,varname:node_4259,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:3282,x:31209,y:32935,varname:node_3282,prsc:2|A-6976-OUT,B-2358-OUT,GT-6861-OUT,EQ-4259-RGB,LT-4259-RGB;n:type:ShaderForge.SFN_Vector1,id:6861,x:30917,y:33039,varname:node_6861,prsc:2,v1:1;n:type:ShaderForge.SFN_If,id:9648,x:31448,y:32993,varname:node_9648,prsc:2|A-6976-OUT,B-4112-OUT,GT-6861-OUT,EQ-171-RGB,LT-171-RGB;n:type:ShaderForge.SFN_Tex2d,id:171,x:31206,y:33373,ptovrint:False,ptlb:Franja2,ptin:_Franja2,varname:node_171,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:6536,x:31651,y:32825,varname:node_6536,prsc:2,blmd:0,clmp:True|SRC-3282-OUT,DST-9648-OUT;n:type:ShaderForge.SFN_Blend,id:2337,x:32641,y:32385,varname:node_2337,prsc:2,blmd:5,clmp:True|SRC-6976-OUT,DST-1520-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3661,x:31459,y:33142,ptovrint:False,ptlb:Valor Franja 3,ptin:_ValorFranja3,varname:node_3661,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_If,id:5804,x:31695,y:33057,varname:node_5804,prsc:2|A-6976-OUT,B-3661-OUT,GT-6861-OUT,EQ-2495-RGB,LT-2495-RGB;n:type:ShaderForge.SFN_Tex2d,id:2495,x:31459,y:33246,ptovrint:False,ptlb:Franja 3,ptin:_Franja3,varname:node_2495,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:4099,x:31940,y:32803,varname:node_4099,prsc:2,blmd:0,clmp:True|SRC-6536-OUT,DST-5804-OUT;n:type:ShaderForge.SFN_If,id:4616,x:31985,y:33088,varname:node_4616,prsc:2|A-6976-OUT,B-5151-OUT,GT-6861-OUT,EQ-6471-RGB,LT-6471-RGB;n:type:ShaderForge.SFN_ValueProperty,id:5151,x:31733,y:33209,ptovrint:False,ptlb:Valor Franja 4,ptin:_ValorFranja4,varname:node_5151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:6471,x:31714,y:33282,ptovrint:False,ptlb:Franja 4,ptin:_Franja4,varname:node_6471,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:1520,x:32165,y:32794,varname:node_1520,prsc:2,blmd:0,clmp:True|SRC-4099-OUT,DST-4616-OUT;n:type:ShaderForge.SFN_Divide,id:1639,x:30671,y:33504,varname:node_1639,prsc:2|A-4412-UVOUT,B-62-OUT;n:type:ShaderForge.SFN_ViewVector,id:5788,x:30490,y:33567,varname:node_5788,prsc:2;n:type:ShaderForge.SFN_Subtract,id:6976,x:30636,y:32545,varname:node_6976,prsc:2|A-264-OUT,B-8396-OUT;n:type:ShaderForge.SFN_Slider,id:8396,x:30163,y:32495,ptovrint:False,ptlb:valor sombra,ptin:_valorsombra,varname:node_8396,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:5682,x:32277,y:32529,ptovrint:False,ptlb:limite sombra,ptin:_limitesombra,varname:node_5682,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Divide,id:5150,x:34687,y:32302,varname:node_5150,prsc:2|A-63-RGB,B-1546-OUT;n:type:ShaderForge.SFN_Vector1,id:4583,x:34305,y:32421,varname:node_4583,prsc:2,v1:1.5;n:type:ShaderForge.SFN_ValueProperty,id:1546,x:34305,y:32509,ptovrint:False,ptlb:intensidad luz,ptin:_intensidadluz,varname:node_1546,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_Blend,id:4899,x:35187,y:31945,varname:node_4899,prsc:2,blmd:5,clmp:True|SRC-1171-OUT,DST-2097-RGB;n:type:ShaderForge.SFN_Color,id:2097,x:35000,y:31735,ptovrint:False,ptlb:color sombra,ptin:_colorsombra,varname:node_2097,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Blend,id:2414,x:35365,y:31945,varname:node_2414,prsc:2,blmd:0,clmp:True|SRC-4899-OUT,DST-4179-OUT;n:type:ShaderForge.SFN_Blend,id:3787,x:35410,y:32114,varname:node_3787,prsc:2,blmd:1,clmp:True|SRC-2414-OUT,DST-5150-OUT;n:type:ShaderForge.SFN_Multiply,id:6829,x:34468,y:32186,varname:node_6829,prsc:2|A-5843-OUT,B-63-RGB;proporder:80-82-83-216-239-9921-4707-4093-8742-6015-5312-5475-3092-3145-7871-7975-5153-6390-3245-3626-1592-8104-3909-8157-258-8711-6869-26-2358-4259-4112-171-3661-2495-5151-6471-8396-5682-1546-2097;pass:END;sub:END;*/

Shader "Shader Forge/Examples/Custom Lighting" {
    Properties {
        _Color ("Color", Color) = (0.6544118,0.8426978,1,1)
        _AlbedoAO ("Albedo+AO", 2D) = "white" {}
        _Normals ("Normals", 2D) = "bump" {}
        _CelShadingBands ("Cel Shading Bands", Float ) = 6
        _Gloss ("Gloss", Range(0, 1)) = 0.2940849
        _ColorDistanciaCamara ("Color Distancia Camara", Color) = (0.5566038,0.5566038,0.5566038,1)
        _Distanciadegradado ("Distancia degradado", Range(0, 1)) = 0
        _MaximoDegradadoDistancia ("Maximo Degradado Distancia", Float ) = 100
        _MinimoDegrafadoDistancia ("Minimo Degrafado Distancia", Float ) = 0
        _Outlinecolor ("Outline color", Color) = (0.1698113,0.1698113,0.1698113,1)
        _outlinewith ("outline with", Float ) = 0
        _Ambientlight ("Ambient light", Range(0, 1)) = 0.5643086
        _LightAtten ("Light Atten", Range(0, 1)) = 0.5643086
        _texturaparalasombra ("textura para la sombra", 2D) = "white" {}
        _texturaparalasombradelasnormales ("textura para la sombra de las normales", 2D) = "gray" {}
        _rimlightsize ("rimlight size", Range(0, 5)) = 4
        _rimlightcolor ("rimlight color", Color) = (0.6946412,0.3551086,0.7924528,1)
        _rimlighthardness ("rimlight hardness", Range(0, 100)) = 2
        _Curvature ("Curvature", 2D) = "white" {}
        _IntensidadCurvature ("Intensidad Curvature", Float ) = 0
        _AmbientOcclusion ("Ambient Occlusion", 2D) = "white" {}
        _IntensidadAO ("Intensidad AO", Float ) = 0
        _IntensidadSombra ("Intensidad Sombra", Range(0, 1)) = 0
        _FranjasSombra ("Franjas Sombra", Float ) = 2
        _Texturasombraproyectada ("Textura sombra proyectada", 2D) = "white" {}
        _rimlightcolor_sombra ("rimlight color_sombra", Color) = (1,1,1,1)
        _rimlighthardness_sombra ("rimlight hardness_sombra", Range(0, 100)) = 2
        _rimlightsizesombra ("rimlight size sombra", Range(0, 5)) = 4
        _ValorFranja1 ("Valor Franja 1", Float ) = 0
        _Franja1 ("Franja1", 2D) = "white" {}
        _ValorFranja2 ("Valor Franja 2", Float ) = 0
        _Franja2 ("Franja2", 2D) = "white" {}
        _ValorFranja3 ("Valor Franja 3", Float ) = 0
        _Franja3 ("Franja 3", 2D) = "white" {}
        _ValorFranja4 ("Valor Franja 4", Float ) = 0
        _Franja4 ("Franja 4", 2D) = "white" {}
        _valorsombra ("valor sombra", Range(0, 1)) = 0
        _limitesombra ("limite sombra", Float ) = 0
        _intensidadluz ("intensidad luz", Float ) = 1.5
        _colorsombra ("color sombra", Color) = (0.5,0.5,0.5,1)
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Outlinecolor;
            uniform float _outlinewith;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_outlinewith,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_Outlinecolor.rgb,0);
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _AlbedoAO; uniform float4 _AlbedoAO_ST;
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform float _CelShadingBands;
            uniform float _Ambientlight;
            uniform float _LightAtten;
            uniform float _rimlightsize;
            uniform float4 _rimlightcolor;
            uniform float _rimlighthardness;
            uniform sampler2D _Curvature; uniform float4 _Curvature_ST;
            uniform float _IntensidadCurvature;
            uniform float _IntensidadAO;
            uniform float _IntensidadSombra;
            uniform float _FranjasSombra;
            uniform sampler2D _Texturasombraproyectada; uniform float4 _Texturasombraproyectada_ST;
            uniform float4 _rimlightcolor_sombra;
            uniform float _rimlighthardness_sombra;
            uniform float _rimlightsizesombra;
            uniform float _ValorFranja1;
            uniform float _ValorFranja2;
            uniform sampler2D _Franja1; uniform float4 _Franja1_ST;
            uniform sampler2D _Franja2; uniform float4 _Franja2_ST;
            uniform float _ValorFranja3;
            uniform sampler2D _Franja3; uniform float4 _Franja3_ST;
            uniform float _ValorFranja4;
            uniform sampler2D _Franja4; uniform float4 _Franja4_ST;
            uniform float _valorsombra;
            uniform float _intensidadluz;
            uniform float4 _colorsombra;
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
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normals_var = UnpackNormal(tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals)));
                float3 normalLocal = _Normals_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 node_8949 = saturate(min(_rimlightcolor_sombra.rgb,floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),_rimlightsizesombra) * _rimlighthardness_sombra) / (_rimlighthardness_sombra - 1)));
                float4 _AlbedoAO_var = tex2D(_AlbedoAO,TRANSFORM_TEX(i.uv0, _AlbedoAO));
                float node_8750 = floor(saturate((clamp(attenuation,0.1,1.0)*_LightAtten)) * _FranjasSombra) / (_FranjasSombra - 1);
                float3 node_5843 = lerp(_AlbedoAO_var.rgb,float3(node_8750,node_8750,node_8750),_IntensidadSombra);
                float4 _Texturasombraproyectada_var = tex2D(_Texturasombraproyectada,TRANSFORM_TEX(i.uv0, _Texturasombraproyectada));
                float node_6976 = (floor(max(0,dot(lightDirection,normalDirection)) * _CelShadingBands) / (_CelShadingBands - 1)-_valorsombra);
                float node_3282_if_leA = step(node_6976,_ValorFranja1);
                float node_3282_if_leB = step(_ValorFranja1,node_6976);
                float4 _Franja1_var = tex2D(_Franja1,TRANSFORM_TEX(i.uv0, _Franja1));
                float node_6861 = 1.0;
                float node_9648_if_leA = step(node_6976,_ValorFranja2);
                float node_9648_if_leB = step(_ValorFranja2,node_6976);
                float4 _Franja2_var = tex2D(_Franja2,TRANSFORM_TEX(i.uv0, _Franja2));
                float node_5804_if_leA = step(node_6976,_ValorFranja3);
                float node_5804_if_leB = step(_ValorFranja3,node_6976);
                float4 _Franja3_var = tex2D(_Franja3,TRANSFORM_TEX(i.uv0, _Franja3));
                float node_4616_if_leA = step(node_6976,_ValorFranja4);
                float node_4616_if_leB = step(_ValorFranja4,node_6976);
                float4 _Franja4_var = tex2D(_Franja4,TRANSFORM_TEX(i.uv0, _Franja4));
                float3 node_302 = (((_AlbedoAO_var.rgb*_Color.rgb*saturate(max(node_6976,saturate(min(saturate(min(saturate(min(lerp((node_3282_if_leA*_Franja1_var.rgb)+(node_3282_if_leB*node_6861),_Franja1_var.rgb,node_3282_if_leA*node_3282_if_leB),lerp((node_9648_if_leA*_Franja2_var.rgb)+(node_9648_if_leB*node_6861),_Franja2_var.rgb,node_9648_if_leA*node_9648_if_leB))),lerp((node_5804_if_leA*_Franja3_var.rgb)+(node_5804_if_leB*node_6861),_Franja3_var.rgb,node_5804_if_leA*node_5804_if_leB))),lerp((node_4616_if_leA*_Franja4_var.rgb)+(node_4616_if_leB*node_6861),_Franja4_var.rgb,node_4616_if_leA*node_4616_if_leB))))))+(UNITY_LIGHTMODEL_AMBIENT.rgb*_Ambientlight))+saturate(min(_rimlightcolor.rgb,floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),_rimlightsize) * _rimlighthardness) / (_rimlighthardness - 1))));
                float4 _Curvature_var = tex2D(_Curvature,TRANSFORM_TEX(i.uv0, _Curvature));
                float3 finalColor = saturate((saturate((saturate(min(saturate(max(saturate(max(saturate(max(node_8949,node_5843)),_Texturasombraproyectada_var.rgb)),_colorsombra.rgb)),saturate(max(saturate((saturate(max(node_8949,node_302))-node_5843)),node_302))))*(_LightColor0.rgb/_intensidadluz)))*saturate((step(_IntensidadCurvature,_Curvature_var.rgb)*step(_IntensidadAO,_AlbedoAO_var.a)))));
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _AlbedoAO; uniform float4 _AlbedoAO_ST;
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform float _CelShadingBands;
            uniform float _Ambientlight;
            uniform float _LightAtten;
            uniform float _rimlightsize;
            uniform float4 _rimlightcolor;
            uniform float _rimlighthardness;
            uniform sampler2D _Curvature; uniform float4 _Curvature_ST;
            uniform float _IntensidadCurvature;
            uniform float _IntensidadAO;
            uniform float _IntensidadSombra;
            uniform float _FranjasSombra;
            uniform sampler2D _Texturasombraproyectada; uniform float4 _Texturasombraproyectada_ST;
            uniform float4 _rimlightcolor_sombra;
            uniform float _rimlighthardness_sombra;
            uniform float _rimlightsizesombra;
            uniform float _ValorFranja1;
            uniform float _ValorFranja2;
            uniform sampler2D _Franja1; uniform float4 _Franja1_ST;
            uniform sampler2D _Franja2; uniform float4 _Franja2_ST;
            uniform float _ValorFranja3;
            uniform sampler2D _Franja3; uniform float4 _Franja3_ST;
            uniform float _ValorFranja4;
            uniform sampler2D _Franja4; uniform float4 _Franja4_ST;
            uniform float _valorsombra;
            uniform float _intensidadluz;
            uniform float4 _colorsombra;
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
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normals_var = UnpackNormal(tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals)));
                float3 normalLocal = _Normals_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 node_8949 = saturate(min(_rimlightcolor_sombra.rgb,floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),_rimlightsizesombra) * _rimlighthardness_sombra) / (_rimlighthardness_sombra - 1)));
                float4 _AlbedoAO_var = tex2D(_AlbedoAO,TRANSFORM_TEX(i.uv0, _AlbedoAO));
                float node_8750 = floor(saturate((clamp(attenuation,0.1,1.0)*_LightAtten)) * _FranjasSombra) / (_FranjasSombra - 1);
                float3 node_5843 = lerp(_AlbedoAO_var.rgb,float3(node_8750,node_8750,node_8750),_IntensidadSombra);
                float4 _Texturasombraproyectada_var = tex2D(_Texturasombraproyectada,TRANSFORM_TEX(i.uv0, _Texturasombraproyectada));
                float node_6976 = (floor(max(0,dot(lightDirection,normalDirection)) * _CelShadingBands) / (_CelShadingBands - 1)-_valorsombra);
                float node_3282_if_leA = step(node_6976,_ValorFranja1);
                float node_3282_if_leB = step(_ValorFranja1,node_6976);
                float4 _Franja1_var = tex2D(_Franja1,TRANSFORM_TEX(i.uv0, _Franja1));
                float node_6861 = 1.0;
                float node_9648_if_leA = step(node_6976,_ValorFranja2);
                float node_9648_if_leB = step(_ValorFranja2,node_6976);
                float4 _Franja2_var = tex2D(_Franja2,TRANSFORM_TEX(i.uv0, _Franja2));
                float node_5804_if_leA = step(node_6976,_ValorFranja3);
                float node_5804_if_leB = step(_ValorFranja3,node_6976);
                float4 _Franja3_var = tex2D(_Franja3,TRANSFORM_TEX(i.uv0, _Franja3));
                float node_4616_if_leA = step(node_6976,_ValorFranja4);
                float node_4616_if_leB = step(_ValorFranja4,node_6976);
                float4 _Franja4_var = tex2D(_Franja4,TRANSFORM_TEX(i.uv0, _Franja4));
                float3 node_302 = (((_AlbedoAO_var.rgb*_Color.rgb*saturate(max(node_6976,saturate(min(saturate(min(saturate(min(lerp((node_3282_if_leA*_Franja1_var.rgb)+(node_3282_if_leB*node_6861),_Franja1_var.rgb,node_3282_if_leA*node_3282_if_leB),lerp((node_9648_if_leA*_Franja2_var.rgb)+(node_9648_if_leB*node_6861),_Franja2_var.rgb,node_9648_if_leA*node_9648_if_leB))),lerp((node_5804_if_leA*_Franja3_var.rgb)+(node_5804_if_leB*node_6861),_Franja3_var.rgb,node_5804_if_leA*node_5804_if_leB))),lerp((node_4616_if_leA*_Franja4_var.rgb)+(node_4616_if_leB*node_6861),_Franja4_var.rgb,node_4616_if_leA*node_4616_if_leB))))))+(UNITY_LIGHTMODEL_AMBIENT.rgb*_Ambientlight))+saturate(min(_rimlightcolor.rgb,floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),_rimlightsize) * _rimlighthardness) / (_rimlighthardness - 1))));
                float4 _Curvature_var = tex2D(_Curvature,TRANSFORM_TEX(i.uv0, _Curvature));
                float3 finalColor = saturate((saturate((saturate(min(saturate(max(saturate(max(saturate(max(node_8949,node_5843)),_Texturasombraproyectada_var.rgb)),_colorsombra.rgb)),saturate(max(saturate((saturate(max(node_8949,node_302))-node_5843)),node_302))))*(_LightColor0.rgb/_intensidadluz)))*saturate((step(_IntensidadCurvature,_Curvature_var.rgb)*step(_IntensidadAO,_AlbedoAO_var.a)))));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
