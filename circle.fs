#iUniform float v = 0.0 in { -0.3, 0.3 } 

void main()
{
    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = uv * 2.0 - 1.0;
    uv.x =  uv.x * iResolution.x / iResolution.y;
    float d = distance(vec2(0.0, 0.0), uv);
    float r = smoothstep(0.5, 0.5+v, d);
    vec3 color = vec3(r);
    gl_FragColor = vec4(color,1.0);
}