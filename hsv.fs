#iUniform float h = 0.0 in { 0.0, 1.0 } 

// https://stackoverflow.com/questions/68901847/opengl-esconvert-rgb-to-hsv-not-hsl
vec3 HSVtoRGB(in vec3 HSV)
{
    float H   = HSV.x;
    float R   = abs(H * 6.0 - 3.0) - 1.0;
    float G   = 2.0 - abs(H * 6.0 - 2.0);
    float B   = 2.0 - abs(H * 6.0 - 4.0);
    vec3  RGB = clamp( vec3(R,G,B), 0.0, 1.0 );
    return ((RGB - 1.0) * HSV.y + 1.0) * HSV.z;
}

void main()
{
    float s = iMouse.x / iResolution.x;
    float v = iMouse.y / iResolution.y;
    vec3 color = HSVtoRGB(vec3(h,s,v));
    gl_FragColor = vec4(color,1.0);
}