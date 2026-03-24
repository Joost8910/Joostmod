sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float uIntensity;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float2 uTargetPosition;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;

float4 MeteorShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float3 colorA = float3(1, 0.05, 0.05);
    float3 colorB = float3(1, 0.25, 0.1);
    float3 colorC = float3(1, 1, 0.8);
    
    float frameX = (coords.x * uImageSize0.x - uSourceRect.x) / uSourceRect.z;
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    
    frameX = (1 - frameX);
    float frameXY = (frameX + frameY * 0.707);
    float frameXY2 = abs((frameX * -0.5 + frameY) - 0.2);
    
    float wave = max(sin(frameXY * 20 + uTime * 25), 0);
    float wave2 = pow(max(sin(frameXY2 * -40 + uTime * 25), 0), 2);
    //float core = pow(max(sin(frameY * 8 + uWorldPosition.y / 20), 0), 3);
    float core = frameXY;
    colorA *= pow(core, 2) * 2;
    colorB *= wave * pow(core, 4) * 6;
    colorC *= wave2 * pow(core, 10);
    color.rgb *= colorA * sampleColor.rgb + (colorB+ colorC);
    color.a *= sampleColor.a;
    return color;
}
float4 XTransformShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float2 pixCoords = coords * uImageSize0 - uSourceRect.xy;
    
    float t = uOpacity + 2;
    float offset = (uOpacity * 1.5);
    
    float stretch = t; //stretch size in pixels
    float2 pCoords = pixCoords;
    float shift = (pCoords.x + offset) % stretch;
    pCoords.x += stretch - shift;
    float2 coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color2 = tex2D(uImage1, coordsFromPix);
    
    if (color.a > 0)
    {
        return color2 * sampleColor;
    }
    
    
    //float4 r = float4(shift / stretch, 0, 0, 1);
    return color * sampleColor;
}

technique Technique1
{
    pass JuiceMeteorShaderPass
    {
        PixelShader = compile ps_2_0 MeteorShaderFunction();
    }
    pass XTransformShaderPass
    {
        PixelShader = compile ps_2_0 XTransformShaderFunction();
    }
}