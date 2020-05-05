//LINE VERTEX SHADER

in vec3 vertex;

uniform mat4 vpMatrix;


void main( void ){

    	gl_Position = vpMatrix * vec4(vertex, 1.0f);
}
