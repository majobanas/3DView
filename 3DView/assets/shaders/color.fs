//DIFFUSE COLOR FRAGMENT SHADER

#version 450 // for glsl version (12 is for older versions , say opengl 2.1
in vec2 UV;
in float z;

uniform vec3 diffuseColor;
out vec4 fragment_color;

float near = 0.1f; 
float far  = 200.0f; 
  
vec4 LinearizeDepth(float depth) {
	return vec4(vec3((2.0f * near * far) / (far + near - ((depth / far) * 2.0f - 1.0f) * (far - near))) * diffuseColor, 1.0f);
}

void main( void ) {
	fragment_color = vec4 (diffuseColor,1.0f);
	//fragment_color = LinearizeDepth(z);
}
