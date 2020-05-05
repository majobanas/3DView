
in vec3 vertex;

uniform mat4 vpMatrix;
uniform mat4 mMatrix;

out vec3 reflectedDirection;

void main()
{
	reflectedDirection = vertex;

	gl_Position = vpMatrix * mMatrix * vec4(vertex, 1.0f);
}