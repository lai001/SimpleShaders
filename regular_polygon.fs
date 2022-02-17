#iUniform float n = 3.0 in { 3.0, 10.0 } step 1.0

const float pi = 3.14159;
const float pi_2 = 6.28318;

vec2 rotate(float radian, vec2 p)
{
    mat2 m;
    m[1] = vec2(-sin(radian), cos(radian));
    m[0] = vec2(cos(radian), sin(radian));
    return m * p; 
}

void main()
{
    vec2 uv = (gl_FragCoord.xy / iResolution.xy);
    uv = uv * 2.0 - 1.0;
    uv.x *= iResolution.x / iResolution.y;

    vec2 center = vec2(0.0, 0.0);
    vec2 pc = vec2(length(uv), atan(uv.y, uv.x) / pi_2 + 0.5);

    float max_length_center_to_side = 0.5;

    vec2 center_to_side = vec2(-max_length_center_to_side, center.y);
    center_to_side = rotate(pi_2 / n * floor(pc.y * n), center_to_side);

    vec2 next_center_to_side = center_to_side;
    next_center_to_side = rotate(pi_2 / n, next_center_to_side);

    float theta = fract(pc.y*n) * pi_2 / n ;
    float alpha = (pi - pi_2 / n) / 2.0;
    float beta = pi - theta - alpha;

    float ac_length = length(center_to_side) / sin(beta) * sin(theta);
    
    float ratio = ac_length / length(next_center_to_side - center_to_side);

    vec2 ac = (next_center_to_side - center_to_side) * ratio;
    vec2 oc = center_to_side + ac;

    vec3 color = vec3(smoothstep(length(oc), length(oc) - 0.005, distance(uv, center)));

    gl_FragColor = vec4(color, 1.0);
}