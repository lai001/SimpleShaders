#iUniform float h = 50.0 in { 1.0, 300.0 } 
#iChannel0 "file://images/demo2.jpg"

void main()
{
    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = floor(uv * h) / h;
    vec3 color = texture(iChannel0, uv).rgb;
    gl_FragColor = vec4(color, 1.0);
}