struct Rect
{
  float x;
  float y;
  float width;
  float height;
};

vec3 quadrilateral(vec2 uv, Rect rect, vec3 house_color, vec3 background_color)
{
  float a = 1.0-step(rect.x, uv.x);
  float b = step(rect.x + rect.width, uv.x);
  float c = step(rect.y + rect.height, uv.y);
  float d = 1.0 - step(rect.y, uv.y);
  return mix(house_color, background_color, max(0.0, a+b+c+d));
}

void main()
{
  vec2 uv = (gl_FragCoord.xy / iResolution.xy);
  vec3 plane = vec3(step(0.2, uv.y));
  vec3 color = quadrilateral(uv, Rect(0.2, 0.15, 0.2, 0.2), vec3(1.0, 0.0, 0.0), plane);
  gl_FragColor = vec4(color, 1.0);
}