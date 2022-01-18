#iChannel0 "file://images/demo2.jpg"
#iUniform float n = 1.0 in { 1.0, 10.0 }
#iUniform float x = 0.5 in { 0.0, 1.0 }
#iUniform float y = 0.5 in { 0.0, 1.0 }
#iUniform float _x = 0.5 in { 0.0, 1.0 }
#iUniform float _y = 0.5 in { 0.0, 1.0 }

void main()
{
    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    vec2 center = vec2(x, y);
    vec2 offset = vec2(_x, _y);
    vec4 color = texture(iChannel0, vec2(fract(uv.x * n) - center.x * n + offset.x, fract(uv.y * n) - center.y * n + offset.y));
    gl_FragColor = mix(vec4(1.0, 0.0, 0.0, 1.0), color, step(0.01, distance(center, uv)));
}