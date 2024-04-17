// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Coastline_simple"
{
	Properties
	{
		_Transparency("Transparency", Range( 0 , 1)) = 0
		_Color("Color ", Color) = (1,1,1,0)
		_MainTex("MainTex", 2D) = "white" {}
		_NormalTex("NormalTex", 2D) = "bump" {}
		_NormalScale("NormalScale", Range( 0 , 1)) = 1
		_FoamColor("FoamColor", Color) = (0,0,0,0)
		_FoamTex("FoamTex", 2D) = "white" {}
		_Coastlinemask("Coastline mask", 2D) = "white" {}
		[NoScaleOffset]_Cubemap("Cubemap", CUBE) = "white" {}
		_Fresnel("Fresnel", Float) = 0
		_WaterSpeed("WaterSpeed", Float) = 0
		_WaveSpeed("WaveSpeed", Float) = 0
		_WaveAlpha("WaveAlpha", Float) = 4
		_FoamParams("_FoamParams", Vector) = (3.45,-0.75,1.15,33)
		_WaterDir("WaterDir", Vector) = (0,0,0,0)
		_Specular("Specular", Float) = 0
		_Gloss("Gloss", Float) = 0
		[Gamma]_LightColor("LightColor", Color) = (0,0,0,0)
		_LightDir("LightDir", Vector) = (0,0,0,0)
		[NoScaleOffset]_WaveTex("WaveTex", 2D) = "white" {}
		_WaveMask("WaveMask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldRefl;
			INTERNAL_DATA
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _FoamColor;
		uniform float4 _FoamParams;
		uniform sampler2D _Coastlinemask;
		uniform float4 _Coastlinemask_ST;
		uniform sampler2D _FoamTex;
		uniform float4 _FoamTex_ST;
		uniform sampler2D _NormalTex;
		uniform float4 _NormalTex_ST;
		uniform float _WaterSpeed;
		uniform float2 _WaterDir;
		uniform half _NormalScale;
		uniform samplerCUBE _Cubemap;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform half _Fresnel;
		uniform sampler2D _WaveTex;
		uniform float _WaveSpeed;
		uniform float _WaveAlpha;
		uniform sampler2D _WaveMask;
		uniform float3 _LightDir;
		uniform float _Specular;
		uniform float _Gloss;
		uniform float4 _LightColor;
		uniform float _Transparency;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_Coastlinemask = i.uv_texcoord * _Coastlinemask_ST.xy + _Coastlinemask_ST.zw;
			float4 tex2DNode302 = tex2D( _Coastlinemask, uv_Coastlinemask );
			float mask_r319 = tex2DNode302.r;
			float2 uv_FoamTex = i.uv_texcoord * _FoamTex_ST.xy + _FoamTex_ST.zw;
			float2 uv_NormalTex = i.uv_texcoord * _NormalTex_ST.xy + _NormalTex_ST.zw;
			float mulTime73 = _Time.y * _WaterSpeed;
			float2 temp_output_297_0 = ( mulTime73 * _WaterDir );
			float2 appendResult89 = (float2(temp_output_297_0));
			float2 appendResult102 = (float2(( uv_NormalTex.y + temp_output_297_0 ).x , uv_NormalTex.x));
			half3 worldNormal191 = ( ( UnpackScaleNormal( tex2D( _NormalTex, ( uv_NormalTex + appendResult89 ) ), _NormalScale ) + UnpackScaleNormal( tex2D( _NormalTex, appendResult102 ), _NormalScale ) ) / 2.0 );
			float4 _WaveParams = float4(0.2,0,0,0.01);
			float4 tex2DNode167 = tex2D( _FoamTex, ( float3( uv_FoamTex ,  0.0 ) + ( worldNormal191 * _WaveParams.w ) ).xy );
			float foam_g174 = tex2DNode167.g;
			float mask_b340 = tex2DNode302.b;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float temp_output_362_0 = ( 1.0 - mask_r319 );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float dotResult28 = dot( ase_worldViewDir , (WorldNormalVector( i , worldNormal191 )) );
			float4 lerpResult34 = lerp( texCUBE( _Cubemap, WorldReflectionVector( i , worldNormal191 ) ) , ( _Color * tex2D( _MainTex, ( -uv_MainTex + temp_output_362_0 ) ) ) , saturate( pow( dotResult28 , _Fresnel ) ));
			float4 baseCol259 = lerpResult34;
			float foam_r168 = tex2DNode167.r;
			float mulTime295 = _Time.y * _WaveSpeed;
			float foam_b263 = tex2DNode167.b;
			float temp_output_124_0 = ( mulTime295 + ( foam_b263 * _WaveParams.z ) );
			float mask_g331 = tex2DNode302.g;
			float temp_output_118_0 = ( _WaveParams.y + mask_g331 );
			float2 appendResult138 = (float2(( ( cos( temp_output_124_0 ) * _WaveParams.x ) + temp_output_118_0 ) , 0.0));
			float2 appendResult126 = (float2(( ( sin( temp_output_124_0 ) * _WaveParams.x ) + temp_output_118_0 ) , 0.0));
			float2 appendResult142 = (float2(mask_g331 , 1.0));
			float3 normalizeResult187 = normalize( _LightDir );
			float3 normalizeResult186 = normalize( ( ase_worldViewDir - normalizeResult187 ) );
			float dotResult189 = dot( (WorldNormalVector( i , worldNormal191 )) , normalizeResult186 );
			o.Emission = ( ( ( _FoamColor * ( _FoamParams.w * ( ( 1.0 - saturate( ( ( _FoamParams.x - mask_r319 ) / _FoamParams.x ) ) ) * ( 1.0 - saturate( ( ( mask_r319 - _FoamParams.y ) / _FoamParams.z ) ) ) ) * foam_g174 * mask_b340 ) ) + ( baseCol259 + ( _FoamColor * ( foam_r168 * ( tex2D( _WaveTex, appendResult138 ).r + tex2D( _WaveTex, appendResult126 ).r ) * _WaveAlpha * tex2D( _WaveMask, appendResult142 ).r * mask_g331 ) ) ) ) + ( ( pow( max( 0.0 , dotResult189 ) , ( 128.0 * _Specular ) ) * _Gloss ) * _LightColor ) ).rgb;
			float opacity260 = ( temp_output_362_0 * _Transparency );
			o.Alpha = opacity260;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.CommentaryNode;298;1985.096,1360.776;Inherit;False;1316.636;668.035;Comment;7;200;162;270;158;268;266;262;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;274;-513.889,1277.315;Inherit;False;2439.438;901.3826;海浪参数(X海浪范围 Y海浪偏移 Z海浪扰动 W泡沫扰动)  WaveSpeed(海浪速度)  Coastline mask(R海岸线 G浪花遮罩 B岸边泡沫遮罩);27;119;294;295;265;171;170;154;142;139;116;127;126;138;120;137;136;124;264;133;118;125;121;302;319;324;331;340;WaveParams;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;273;-509.6458,2240.843;Inherit;False;2050.584;411.4309;岸边泡沫参数(X淡入 Y淡出 Z宽度 W透明度);14;150;143;172;160;159;153;147;152;151;146;145;144;320;363;FoamParams;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;203;-530.1968,715.8018;Inherit;False;1355.842;438.2009;泡沫贴图(R海浪泡沫 G海岸泡沫 B海浪扰动);8;254;167;177;176;166;174;168;263;Foam;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;202;-498.3429,2784.216;Inherit;False;2334.188;761.9067;HighLight;16;192;181;198;196;195;199;193;194;188;189;256;186;185;184;187;182;HighLight;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;114;-484.8163,-179.0032;Inherit;False;843.8986;271.3557;Reflection;3;251;21;56;Reflection;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;113;-512.5827,156.731;Inherit;False;968.4155;464.976;Fresnel;7;253;252;29;31;33;30;28;Fresnel;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;112;-2611.192,471.7441;Inherit;False;2036.904;674.3565;WaterSpeed(海水速度)  WaterDir(XY海水方向);16;104;296;297;73;79;38;224;223;221;37;191;222;99;102;89;84;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;111;-503.6456,-729.0441;Inherit;False;1053.221;489.5679;BaceColor;11;164;260;330;329;1;308;316;315;163;361;362;BaceColor;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;-30.83745,920.1469;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;84;-2381.146,534.7441;Inherit;False;0;37;2;3;2;SAMPLER2D;;False;0;FLOAT2;40,40;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;89;-1881.719,733.8278;Inherit;False;FLOAT2;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;177;136.4845,865.8002;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;102;-1882.087,890.3325;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;99;-1738.281,539.3317;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;222;-1141.164,755.0203;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldReflectionVector;56;-222.2554,-103.5298;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;251;-435.0237,-110.326;Inherit;False;191;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;34;804.4974,-117.5346;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;37;-1471.607,589.3401;Inherit;True;Property;_NormalTex;NormalTex;3;0;Create;True;0;0;0;False;0;False;-1;None;1937de641fa261b45a9a030e14479551;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;221;-1460.282,900.1071;Inherit;True;Property;_NormalTex1;NormalTex;3;0;Create;True;0;0;0;False;0;False;-1;None;1937de641fa261b45a9a030e14479551;True;0;True;bump;Auto;True;Instance;37;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;33;311.5175,254.1305;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;30;147.817,255.4308;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;28;-30.88291,228.231;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;29;-283.9337,215.3796;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;253;-304.9181,401.3409;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;223;-977.8391,757.9849;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-38.98302,343.531;Half;False;Property;_Fresnel;Fresnel;9;0;Create;True;0;0;0;False;0;False;0;0.65;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;224;-1137.486,876.1933;Inherit;False;Constant;_Float1;Float 1;14;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1765.186,813.4375;Half;False;Property;_NormalScale;NormalScale;4;0;Create;True;0;0;0;False;0;False;1;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;182;-437.5094,2917.539;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalizeNode;187;-281.4091,3114.24;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;184;-106.6095,3023.54;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;186;45.77546,3022.42;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;256;26.82186,2839.043;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;189;245.7868,2894.216;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;188;406.7861,2870.919;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;194;583.5477,2965.836;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;193;615.7701,3241.262;Float;False;Property;_Gloss;Gloss;16;0;Create;True;0;0;0;False;0;False;0;1.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;199;862.6044,3084.574;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;195;1197.802,3138.587;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;176;-111.5157,769.8018;Inherit;False;0;167;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;144;24.83154,2301.058;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;145;197.1804,2303.781;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;146;347.2153,2310.843;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;151;198.4151,2501.544;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;152;355.4159,2525.544;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;147;532.7157,2290.843;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;153;526.6156,2505.442;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;264;-172.4835,1602.677;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;124;-13.6895,1535.483;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;138;678.4155,1555.242;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;127;857.0258,1739.278;Inherit;True;Property;_WaveTex1;WaveTex;19;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;116;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;196;402.2087,3085.652;Inherit;False;2;2;0;FLOAT;128;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;198;215.838,3141.584;Float;False;Property;_Specular;Specular;15;0;Create;True;0;0;0;False;0;False;0;6.63;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;181;843.5973,3335.766;Inherit;False;Property;_LightColor;LightColor;17;1;[Gamma];Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;295;-251.9269,1514.866;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;294;-424.0397,1509.613;Float;False;Property;_WaveSpeed;WaveSpeed;11;0;Create;True;0;0;0;False;0;False;0;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;104;-2033.776,884.1587;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;299;2615.382,2944.206;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;192;-176.2099,2837.344;Inherit;False;191;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;259;975.8769,-121.2814;Inherit;False;baseCol;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;168;605.9642,810.889;Inherit;False;foam_r;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;174;596.4246,908.3478;Inherit;False;foam_g;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;263;611.3469,991.8348;Inherit;False;foam_b;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;191;-818.092,774.7839;Half;False;worldNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;254;-331.9016,922.8441;Inherit;False;191;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector2Node;296;-2405.951,844.5667;Inherit;False;Property;_WaterDir;WaterDir;14;0;Create;True;0;0;0;False;0;False;0,0;-0.23,0.23;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;73;-2410.329,731.958;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;297;-2223.951,794.5668;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;143;-459.6458,2347.687;Inherit;False;Property;_FoamParams;_FoamParams;13;0;Create;True;0;0;0;False;0;False;3.45,-0.75,1.15,33;16.26,-1.1,1.99,106.28;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;301;3569.395,1730.196;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Custom/Coastline_simple;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.GetLocalVarNode;262;2042.979,1681.86;Inherit;True;259;baseCol;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;266;2027.646,1458.335;Inherit;False;Property;_FoamColor;FoamColor;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.5566038,0.5566038,0.5566038,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;268;2337.105,1855.655;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;158;2541.429,1735.054;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;270;2734.619,1640.807;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;162;2909.128,1713.758;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;200;3107.459,1725.129;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;261;3349.945,1923.931;Inherit;False;260;opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;150;38.41612,2500.042;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;320;-389.4744,2538.635;Inherit;False;319;mask_r;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;21;21.01867,-130.2935;Inherit;True;Property;_Cubemap;Cubemap;8;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;a9f053d430424adb925523ba78342596;True;0;False;white;Auto;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;1;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosOpNode;133;153.3614,1448.39;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;252;-492.9356,396.198;Inherit;False;191;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-2585.442,724.704;Float;False;Property;_WaterSpeed;WaterSpeed;10;0;Create;True;0;0;0;False;0;False;0;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;163;-447.8115,-341.1893;Float;False;Property;_Transparency;Transparency;0;0;Create;True;0;0;0;False;0;False;0;0.883;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;315;-453.4645,-608.5186;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NegateNode;316;-233.5645,-589.8187;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;308;-82.49638,-515.3882;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;330;378.1959,-584.8984;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;164;-97.53558,-364.5239;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;329;80.49614,-703.1981;Inherit;False;Property;_Color;Color ;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;51.75894,-527.4975;Inherit;True;Property;_MainTex;MainTex;2;0;Create;True;0;0;0;False;0;False;-1;9e5a72ba0ce0a7e49b83f6ac7dfb52f9;9e5a72ba0ce0a7e49b83f6ac7dfb52f9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;185;-495.7094,3109.04;Inherit;False;Property;_LightDir;LightDir;18;0;Create;True;0;0;0;False;0;False;0,0,0;-28.49,-23.9,-1.22;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;302;-461.7151,1771.6;Inherit;True;Property;_Coastlinemask;Coastline mask;7;0;Create;True;0;0;0;False;0;False;-1;abc146a77d7711b469ebe0be5bd1d685;abc146a77d7711b469ebe0be5bd1d685;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;360;2268.26,2308.649;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;139;868.3216,1953.914;Inherit;True;Property;_WaveMask;WaveMask;20;0;Create;True;0;0;0;False;0;False;-1;c55de03bb9de790488438e9ccfe206cf;c55de03bb9de790488438e9ccfe206cf;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;142;562.9714,2012.445;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;126;653.1963,1762.775;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;137;477.0256,1552.696;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;116;892.3931,1505.08;Inherit;True;Property;_WaveTex;WaveTex;19;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;1999f7251537230498563d6a5cd5071d;1999f7251537230498563d6a5cd5071d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;118;257.3903,1764.6;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;34.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;313.3626,1479.484;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;299.7654,1607.906;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;120;478.6223,1734.521;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;154;1214.409,1653.942;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;1646.946,1792.53;Inherit;True;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;170;1363.074,1615.438;Inherit;False;168;foam_r;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;324;1334.319,1809.842;Inherit;False;Property;_WaveAlpha;WaveAlpha;12;0;Create;True;0;0;0;False;0;False;4;0.48;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;121;128.7664,1618.313;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;265;-438.8835,1604.051;Inherit;False;263;foam_b;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;167;282.3839,863.6072;Inherit;True;Property;_FoamTex;FoamTex;6;0;Create;True;1;;0;0;False;0;False;-1;17cd16fec4e38674489b6f75001e1219;17cd16fec4e38674489b6f75001e1219;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;260;96.27163,-325.992;Inherit;False;opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;331;-124.5871,1851.361;Inherit;False;mask_g;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;340;-126.3026,1949.578;Inherit;False;mask_b;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;361;-435.3107,-465.7474;Inherit;False;319;mask_r;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;362;-241.3107,-466.7474;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;160;1066.98,2349.179;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;159;765.9131,2317.264;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;172;748.9632,2458.422;Inherit;False;174;foam_g;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;363;752.0709,2549.841;Inherit;False;340;mask_b;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;319;-113.3413,1752.104;Inherit;False;mask_r;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;119;-463.889,1327.315;Inherit;False;Constant;_WaveParams;_WaveParams;15;0;Create;True;0;0;0;False;0;False;0.2,0,0,0.01;0.2,0,0,0.01;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;166;0;254;0
WireConnection;166;1;119;4
WireConnection;89;0;297;0
WireConnection;177;0;176;0
WireConnection;177;1;166;0
WireConnection;102;0;104;0
WireConnection;102;1;84;1
WireConnection;99;0;84;0
WireConnection;99;1;89;0
WireConnection;222;0;37;0
WireConnection;222;1;221;0
WireConnection;56;0;251;0
WireConnection;34;0;21;0
WireConnection;34;1;330;0
WireConnection;34;2;33;0
WireConnection;37;1;99;0
WireConnection;37;5;38;0
WireConnection;221;1;102;0
WireConnection;221;5;38;0
WireConnection;33;0;30;0
WireConnection;30;0;28;0
WireConnection;30;1;31;0
WireConnection;28;0;29;0
WireConnection;28;1;253;0
WireConnection;253;0;252;0
WireConnection;223;0;222;0
WireConnection;223;1;224;0
WireConnection;187;0;185;0
WireConnection;184;0;182;0
WireConnection;184;1;187;0
WireConnection;186;0;184;0
WireConnection;256;0;192;0
WireConnection;189;0;256;0
WireConnection;189;1;186;0
WireConnection;188;1;189;0
WireConnection;194;0;188;0
WireConnection;194;1;196;0
WireConnection;199;0;194;0
WireConnection;199;1;193;0
WireConnection;195;0;199;0
WireConnection;195;1;181;0
WireConnection;144;0;143;1
WireConnection;144;1;320;0
WireConnection;145;0;144;0
WireConnection;145;1;143;1
WireConnection;146;0;145;0
WireConnection;151;0;150;0
WireConnection;151;1;143;3
WireConnection;152;0;151;0
WireConnection;147;1;146;0
WireConnection;153;1;152;0
WireConnection;264;0;265;0
WireConnection;264;1;119;3
WireConnection;124;0;295;0
WireConnection;124;1;264;0
WireConnection;138;0;137;0
WireConnection;127;1;126;0
WireConnection;196;1;198;0
WireConnection;295;0;294;0
WireConnection;104;0;84;2
WireConnection;104;1;297;0
WireConnection;299;0;195;0
WireConnection;259;0;34;0
WireConnection;168;0;167;1
WireConnection;174;0;167;2
WireConnection;263;0;167;3
WireConnection;191;0;223;0
WireConnection;73;0;79;0
WireConnection;297;0;73;0
WireConnection;297;1;296;0
WireConnection;301;2;200;0
WireConnection;301;9;261;0
WireConnection;268;0;266;0
WireConnection;268;1;171;0
WireConnection;158;0;262;0
WireConnection;158;1;268;0
WireConnection;270;0;266;0
WireConnection;270;1;360;0
WireConnection;162;0;270;0
WireConnection;162;1;158;0
WireConnection;200;0;162;0
WireConnection;200;1;299;0
WireConnection;150;0;320;0
WireConnection;150;1;143;2
WireConnection;21;1;56;0
WireConnection;133;0;124;0
WireConnection;316;0;315;0
WireConnection;308;0;316;0
WireConnection;308;1;362;0
WireConnection;330;0;329;0
WireConnection;330;1;1;0
WireConnection;164;0;362;0
WireConnection;164;1;163;0
WireConnection;1;1;308;0
WireConnection;360;0;160;0
WireConnection;139;1;142;0
WireConnection;142;0;331;0
WireConnection;126;0;120;0
WireConnection;137;0;136;0
WireConnection;137;1;118;0
WireConnection;116;1;138;0
WireConnection;118;0;119;2
WireConnection;118;1;331;0
WireConnection;136;0;133;0
WireConnection;136;1;119;1
WireConnection;125;0;121;0
WireConnection;125;1;119;1
WireConnection;120;0;125;0
WireConnection;120;1;118;0
WireConnection;154;0;116;1
WireConnection;154;1;127;1
WireConnection;171;0;170;0
WireConnection;171;1;154;0
WireConnection;171;2;324;0
WireConnection;171;3;139;1
WireConnection;171;4;331;0
WireConnection;121;0;124;0
WireConnection;167;1;177;0
WireConnection;260;0;164;0
WireConnection;331;0;302;2
WireConnection;340;0;302;3
WireConnection;362;1;361;0
WireConnection;160;0;143;4
WireConnection;160;1;159;0
WireConnection;160;2;172;0
WireConnection;160;3;363;0
WireConnection;159;0;147;0
WireConnection;159;1;153;0
WireConnection;319;0;302;1
ASEEND*/
//CHKSM=DCAC636F6AEC0BB18D019A9806C00B453E9C52EA