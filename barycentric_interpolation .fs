
#iUniform float gradient = 0.0 in { 0.0, 1.0 }
#iUniform vec2 ta = vec2(-1.0, -1.0) in { -1.0, 1.0 }
#iUniform vec2 tb = vec2(1.0, -1.0) in { -1.0, 1.0 }
#iUniform vec2 tc = vec2(0.0, 1.0) in { -1.0, 1.0 }

struct Triangle
{
    vec2 a;
    vec2 b;
    vec2 c;
};

vec3 barycentric(Triangle triangle, vec2 p)
{
    vec3 a = vec3(triangle.b.x - triangle.a.x, triangle.c.x - triangle.a.x, triangle.a.x - p.x);
    vec3 b = vec3(triangle.b.y - triangle.a.y, triangle.c.y - triangle.a.y, triangle.a.y - p.y);
    vec3 w = cross(a, b);
    w = w / w.z;
    return vec3(1.0 - w.x - w.y, w.x, w.y);
}

float is_inside(vec3 w)
{
    if (w.x >= 0.0 && w.x <= 1.0 && w.y >= 0.0 && w.y <= 1.0 && w.z >= 0.0 && w.z <= 1.0)
    {
        return 1.0;
    }
    else
    {
        return 0.0;
    }
}

float triangle_wave(float x)
{
    return abs(fract(x / 2.0)  - 0.5) * 2.0;
}

void main()
{
    const vec3 red_color = vec3(1.0, 0.0, 0.0);
    const vec3 green_color = vec3(0.0, 1.0, 0.0);
    const vec3 blue_color = vec3(0.0, 0.0, 1.0);

    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = uv * 2.0 - 1.0;
    uv.x =  uv.x * iResolution.x / iResolution.y;

    Triangle t;
    t.a = ta;
    t.b = tb;
    t.c = tc;
    
    float p = triangle_wave(iGlobalTime);

    vec3 a_color = mix(red_color, green_color, p);
    vec3 b_color = mix(green_color, blue_color, p);
    vec3 c_color = mix(blue_color, red_color, p);

    a_color = mix(red_color, a_color, gradient);
    b_color = mix(green_color, b_color, gradient);
    c_color = mix(blue_color, c_color, gradient);

    vec3 w = barycentric(t, uv);
    float mask = is_inside(w);

    vec3 color = a_color * w.x + b_color * w.y + c_color * w.z;
    color = color * mask;

    gl_FragColor = vec4(color,1.0);
}