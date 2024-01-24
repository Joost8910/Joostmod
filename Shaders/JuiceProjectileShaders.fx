sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float2 uTargetPosition;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;
float4 uShaderSpecificData;

float4 GungnirBeamShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    //float3 color2 = float3(0.22f, 0.45f, 0.82f);
    
    float frameX = (coords.x * uImageSize0.x - uSourceRect.x) / uSourceRect.w;
    float frameXreverse = ((1 - coords.x) * uImageSize0.x - uSourceRect.x) / uSourceRect.w;
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    //float wave = pow(max(sin(frameY * 8 + uWorldPosition.y / 20), 0), 3);
    float wave = pow(max(sin((frameXreverse * frameY) * -12 + uTime * 12), 0), 3);
    
    float core = pow(max(sin((frameX + frameY) * 3.14f * 2.5), 0), 5);
    color.rgb *= (core * uColor + wave * uSecondaryColor) * min(sampleColor.a * 2, 1) + sampleColor.rgb;
    color.a *= sampleColor.a;
    
    
    return color;
}
technique Technique1
{
    pass GungnirBeamShaderPass
    {
        PixelShader = compile ps_2_0 GungnirBeamShaderFunction();
    }
}