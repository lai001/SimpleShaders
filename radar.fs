#iUniform float speed = 0.1 in { 0.01, 1.0 } 
#iUniform float dr = 0.3 in { 0.1, 0.9 } 
#iUniform float radius = 0.5 in { 0.1, 1.0 } 

const float pi = 3.14159;
const float pi_2 = 6.28318;

float bump(float x, float a, float b, float c, float d)
{
    return smoothstep(a, b, x) * smoothstep(d - c, 0.0, x - c);
}

float line2d(vec2 a, vec2 b, vec2 p, float w) 
{
    vec2 ap = p - a;
    vec2 ab = b - a;
    return 1.0 - smoothstep(0.0, w, length(ap - ab * clamp(dot(ap, ab) / dot(ab, ab), 0.0, 1.0)));
}

vec2 rotate(float radian, vec2 p)
{
    mat2 m;
    m[1] = vec2(-sin(radian), cos(radian));
    m[0] = vec2(cos(radian), sin(radian));
    return m * p; 
}

void main()
{
    vec3 green_color = vec3(0.0, 1.0, 0.0);
    vec2 center = vec2(0.0, 0.0);
    float w = 0.0003;
    float s = 0.005;
    float time = iGlobalTime * speed;

    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = uv * 2.0 - 1.0;
    uv.x =  uv.x * iResolution.x / iResolution.y;

    float offset = distance(uv, center) - radius;
    vec3 circle_color = green_color * bump(offset, -w - s, -w, w, w + s);

    vec2 pc = vec2(length(uv), atan(uv.y, uv.x) / pi_2 + 0.5);
    vec3 scan_color = bump(fract(pc.y - time), 0.0, dr, dr, dr + 0.005) * green_color * 0.3;
    scan_color = (1.0 - step(radius, pc.x)) * scan_color;

    vec2 line_end = vec2(-radius + 0.002, 0.0);
    line_end = rotate((time + dr) * pi_2, line_end);
    vec3 line_color = line2d(center, line_end, uv, 0.006) * green_color;

    gl_FragColor = vec4(scan_color + circle_color + line_color, 1.0);
}