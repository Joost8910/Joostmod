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

float4 ElevatorShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    //float wave = pow(max(sin(frameY * 8 + uWorldPosition.y / 20), 0), 3);
    float wave = pow(max(sin(frameY * 8 + uTime * 5), 0), 3);
    color.rgb *= wave * uColor + sampleColor.rgb;
    color.a *= sampleColor.a;
    return color;
}
float4 OutlineShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float3x4 rowB;
    float3x4 rowM;
    float3x4 rowT;
    //rowM._21_22_23_24 = tex2D(uImage0, coords);
    
    rowM[1] = tex2D(uImage0, coords);

    //float4 color5 = tex2D(uImage0, coords);
    float2 pixCoords = coords * uImageSize0 - uSourceRect.xy;
    float s = 2;
    float2 pCoords = pixCoords;
    pCoords.x -= s;
    pCoords.y += s;
    float2 coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color1 = tex2D(uImage0, coordsFromPix);
    rowB[0] = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.y += s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color2 = tex2D(uImage0, coordsFromPix);
    rowB[1] = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x += s;
    pCoords.y += s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color3 = tex2D(uImage0, coordsFromPix);
    rowB[2] = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    rowM[0] = tex2D(uImage0, coordsFromPix);
    //float4 color4 = tex2D(uImage0, coordsFromPix);
    
    pCoords = pixCoords;
    pCoords.x += s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color6 = tex2D(uImage0, coordsFromPix);
    rowM[2] = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x -= s;
    pCoords.y -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color7 = tex2D(uImage0, coordsFromPix);
    rowT[0] = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.y -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color8 = tex2D(uImage0, coordsFromPix);
    rowT[1] = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x += s;
    pCoords.y -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    //float4 color9 = tex2D(uImage0, coordsFromPix);
    rowT[2] = tex2D(uImage0, coordsFromPix);
    
    /*
    float4 colorx1 = (color1 - color3 + (color4 * 2) - (color6 * 2) + color7 - color9);
    float4 colorx2 = (color3 - color1 + (color6 * 2) - (color4 * 2) + color9 - color7);
    float4 colory1 = (color7 - color1 + (color8 * 2) - (color2 * 2) + color9 - color3);
    float4 colory2 = (color1 - color7 + (color2 * 2) - (color8 * 2) + color3 - color9);
    float4 colorx = max(colorx1, colorx2);
    float4 colory = max(colory1, colory2);
    float4 color = max(colorx, colory);
    if (color.a == 0)
        color = color5;
    else
        color.rgb = uColor;
    */
    float4 cA = rowB[1] + rowM[0] + rowM[2] + rowT[1];
    float4 cM = rowB[1] * rowM[0] * rowM[2] * rowT[1];
    float4 color = rowM[1];
    float luminosity = (color.r + color.g + color.b) / 3;
    color *= sampleColor;
    if (cA.a != 0 && rowM[1][3] == 0)
    {
        color.rgb = uColor.rgb;
    }
    if (cM.a == 0 && rowM[1][3] != 0)
    {
        color.rgb = luminosity * uColor.rgb;
        color *= sampleColor;
    }
    return color;
}
float4 BlurShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color5 = tex2D(uImage0, coords);
    float2 pixCoords = coords * uImageSize0 - uSourceRect.xy;
    
    float s = 2;
    float2 pCoords = pixCoords;
    pCoords.x -= s;
    pCoords.y += s;
    float2 coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color1 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.y += s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color2 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x += s;
    pCoords.y += s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color3 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color4 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x += s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color6 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x -= s;
    pCoords.y -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color7 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.y -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color8 = tex2D(uImage0, coordsFromPix);
    pCoords = pixCoords;
    pCoords.x += s;
    pCoords.y -= s;
    coordsFromPix = (pCoords + uSourceRect.xy) / uImageSize0;
    float4 color9 = tex2D(uImage0, coordsFromPix);
    

    float4 color = ((color1 + color3 + color7 + color9) + ((color2 + color4 + color6 + color8) * 2) + (color5 * 4)) / 16;
    return color * sampleColor;
}
float4 GhostShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    color.rgb *= uColor;
    color.rgb *= ((color.rgb + color.rgb * sampleColor.rgb) / 2) / color.rgb;
    color.a *= sampleColor.a * 0.1f;
    return color;
}
float4 GlowShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    /*
    float frameX = (coords.x * uImageSize0.x - uSourceRect.x) / uSourceRect.w;
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float time = sin(uTime) * 0.5f + 0.5f;
    float wave = 1 - frac(frameY + uTime);
    color.rgb *= wave;
    */
    color.rgb *= uColor;
    color.rgb *= 1.3f;
    color.a *= sampleColor.a;
    return color;
}

technique Technique1
{
    pass BlurShaderPass
    {
        PixelShader = compile ps_2_0 BlurShaderFunction();
    }
    pass OutlineShaderPass
    {
        PixelShader = compile ps_2_0 OutlineShaderFunction();
    }
    pass ElevatorShaderPass
    {
        PixelShader = compile ps_2_0 ElevatorShaderFunction();
    }
    pass GhostShaderPass
    {
        PixelShader = compile ps_2_0 GhostShaderFunction();
    }
    pass GlowShaderPass
    {
        PixelShader = compile ps_2_0 GlowShaderFunction();
    }
}