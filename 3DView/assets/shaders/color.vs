//DIFFUSE COLOR VERTEX SHADER

#version 460// for glsl version (12 is for older versions , say opengl 2.1

in vec3 vertex;
in vec3 normal;
in vec2 uv;
out vec2 UV;
out float z;

uniform	mat4 mMatrix;
uniform mat4 vpMatrix;

void main( void ){
		UV = uv;
    	gl_Position = vpMatrix * mMatrix * vec4(vertex, 1.f);
		z = gl_Position.z;
}
