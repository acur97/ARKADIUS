Shader "Unlit/SkyBlend"
{
    Properties
    {
        _Cube("cube map", Cube) = "" {}
        _Cube2("cube map 2", Cube) = "" {}
		_Blend ("Blend value", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            uniform samplerCUBE _Cube;
            uniform samplerCUBE _Cube2;  
	    	half _Blend;
 
            struct vertexInput
            {
                float4 vertex : POSITION;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float3 texDir : TEXCOORD0;
            };
 
            vertexOutput vert(vertexInput input)
            {
                vertexOutput output;
 
                output.texDir = input.vertex;
                output.pos = UnityObjectToClipPos(input.vertex);
                return output;
            }
 
            float4 frag(vertexOutput input) : COLOR
            {
                fixed4 t1 = texCUBE(_Cube, input.texDir);
                fixed4 t2 = texCUBE(_Cube2, input.texDir);
                fixed4 c = lerp (t1, t2, _Blend);
                return c;
            }
            ENDCG
        }
    }
}