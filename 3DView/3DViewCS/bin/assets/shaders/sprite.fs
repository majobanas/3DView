//DIFFUSE SPRITE FRAGMENT SHADER
in vec2 UV;

uniform sampler2D diffuseTexture;

out vec4 fragment_color;

const vec3 red = vec3(1.0f, 0.0f, 0.0f);
vec3 r = vec3(0.0f);
const vec3 blue = vec3(0.0f, 0.0f, 1.0f);
vec3 b = vec3(0.0f);

vec4 textureColor;

void main( void ) {
	textureColor = texture(diffuseTexture, UV);
	if (textureColor.rgb == vec3(0.0f, 0.0f, 0.0f)) {
		r = red * UV.x;
		b = blue * UV.y;
	}
	fragment_color = vec4(textureColor.rgb + r + b, textureColor.a);
}
