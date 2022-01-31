#version 330

uniform vec2 iResolution;
uniform float uTime;
uniform sampler2D iChannel0;

out vec4 outColor;

void mainImage(out vec4 fragColor, in vec2 fragCoord) 
{
    float bans = 40.0;
    vec2 uv = fragCoord / iResolution.xy;
    vec3 color = mix(vec3(1.0, 0.0, 0.0), vec3(1.0, 165.0 / 255.0, 0.0), uv.y);
    float frequency = texture(iChannel0, vec2(floor(uv.x * bans) / bans, 0.0)).x;
    if(uv.y > frequency) 
    {
        fragColor = vec4(vec3(0.0), 1.0);
    } 
    else 
    {
        float mask = step(0.2, fract(uv.x * bans));
        float r = 1.0 + smoothstep(0.0, frequency, uv.y);
        fragColor = vec4(r * color * mask, 1.0);
    }
}

void main()
{


    mainImage(outColor, gl_FragCoord.xy);
}