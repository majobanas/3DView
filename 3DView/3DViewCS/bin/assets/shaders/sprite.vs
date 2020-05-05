//DIFFUSE SPRITE VERTEX SHADER
in vec3 vertex;
in vec2 uv;

uniform	mat4 mMatrix;
uniform	mat4 pMatrix;

out vec2 UV;

void main( void ){
    UV = uv;
    gl_Position = pMatrix * mMatrix * vec4(vertex, 1.0);
}
