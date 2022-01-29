
#iUniform float speed = 0.05 in { 0.05, 10.0 }
#iUniform float factor = 0.0 in { 0.0, 1.0 }

float rand(float n)
{
    return fract(sin(n) * 43758.5453123);
}

float noise(float p)
{
    float fl = floor(p);
    float fc = fract(p);
    return mix(rand(fl), rand(fl + 1.0), fc);
}

vec2 randomPoint(vec2 i)
{
    return vec2(noise(i.x), noise(cos(i.x)));
}

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
    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = uv * 2.0 - 1.0;
    uv.x *= iResolution.x / iResolution.y;

    float t = iGlobalTime + 150.0;
    float vd = 1000000.0;
    float cellindex = 0.0;
    const float num = 50.0;

    for(float i = 0.0; i < num; i++)
    {
        vec2 point = randomPoint(vec2(i));
        vec2 p = vec2(sin(point* t * speed));
        float d = distance(uv, p);
        if(d < vd)
        {
            cellindex = i;
        }
        vd = min(vd, d);
    }

    vec3 cell_color = HSVtoRGB(vec3(cellindex / num, 1.0, 1.0));
    gl_FragColor = vec4(mix(cell_color, vec3(vd), factor), 1.0);
}