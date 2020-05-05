
in vec3 reflectedDirection;

uniform samplerCube cubemapTexture;

out vec4 fragmentColor;

void main()
{
	fragmentColor = texture(cubemapTexture, reflectedDirection);
}