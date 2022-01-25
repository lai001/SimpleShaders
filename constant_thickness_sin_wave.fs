#iUniform float w = 20.0 in { 20.0, 100.0 }
#iUniform float linew = 0.1 in { 0.1, 0.5 }

float f(float x1, float y1, float x)
{
    return y1 * cos(x) - sin(x) * cos(x) - x + x1;
}

float df(float x1, float y1, float x)
{
    return -y1 * sin(x) - cos(x) * cos(x) + sin(x) * sin(x) - 1.0;
}

void main()
{
    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = uv * w - w / 2.0;
    uv.x =  uv.x * iResolution.x / iResolution.y;

    float x1 = uv.x;
    float y1 = uv.y;

    float xk = x1;
    for (int i = 0; i < 5; i++)
    {
        xk = xk - f(x1, y1, xk) / df(x1, y1, xk);
    }

    float d = distance(vec2(xk, sin(xk)), uv);
    gl_FragColor = vec4(vec3(step(linew, d)), 1.0);
}