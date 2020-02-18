//DIFFUSE COLOR FRAGMENT SHADER

uniform vec3 diffuseColor;
out vec4 fragment_color;

void main( void ) {
	fragment_color = vec4 (diffuseColor,1.0f);
}
