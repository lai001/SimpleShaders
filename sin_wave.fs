void main()
{
  vec2 uv = (gl_FragCoord.xy / iResolution.xy);
  float y = sin(2.0 * 3.14 * uv.x) / 4.0 + 0.5;
  float d = distance(uv, vec2(uv.x, y));
  gl_FragColor = vec4(vec3(step(0.01, d)), 1.0);
}