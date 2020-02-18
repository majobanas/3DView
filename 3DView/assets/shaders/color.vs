//DIFFUSE COLOR VERTEX SHADER

in vec3 vertex;
in vec3 normal;
in vec2 uv;

uniform	mat4 mMatrix;
uniform mat4 vpMatrix;

void main( void ){
    	gl_Position = vpMatrix * mMatrix * vec4(vertex, 1.f);
}
