#iChannel0 "file://images/cat.jpg"

float remap(float v, float a1, float a2, float b1, float b2)
{
    return (v - a1) / (a2 - a1) * (b2 - b1) + b1;
}

void main() 
{
    const float animation_duration = 1.0;
    const float n = 3.0;
    vec2 uv = gl_FragCoord.xy / iResolution.xy;
    float _x = floor(fract(iGlobalTime / animation_duration * n)  * n) / n;
    float _y = 1.0 - floor(fract(iGlobalTime / animation_duration) * n) / n;
    _x = remap(uv.x, 0.0, 1.0, _x, _x + 1.0 / n);
    _y = remap(uv.y, 0.0, 1.0, _y - 1.0 / n, _y);
    gl_FragColor = texture(iChannel0, vec2(_x, _y));
}