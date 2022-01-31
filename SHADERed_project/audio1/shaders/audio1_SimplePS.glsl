#version 330

uniform vec2 iResolution;
uniform float uTime;
uniform sampler2D iChannel0;

out vec4 outColor;

const float pi = 3.14159;
const float pi_2 = 6.28318;

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = (fragCoord.xy / iResolution.xy);
    uv = uv * 2.0 - 1.0;
    uv.x =  uv.x * iResolution.x / iResolution.y;
    
    vec2 pc = vec2(length(uv), atan(uv.y, uv.x) / pi_2 + 0.5);
    float bans = 20.0;

    float frequency = texture(iChannel0, vec2(floor(pc.x * bans) / bans, 0.0)).x;

    fragColor = vec4(vec3(frequency), 1.0);
}

void main()
{


    mainImage(outColor, gl_FragCoord.xy);
}