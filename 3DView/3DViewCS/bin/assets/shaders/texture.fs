//DIFFUSE TEXTURE FRAGMENT SHADER

in vec3 Vertex;
in vec3 Normal;
in vec2 UV;
in vec3 ViewPosition;


uniform int pick;

uniform vec3 diffuseColor;
uniform sampler2D diffuseTexture;
uniform sampler2D specularTexture;
uniform samplerCube cubemapTexture;

out vec4 fragment_color;

float refl(vec3 pLightDir, float pShineness) {
	float sDOTn = max(dot(Normal, -pLightDir), 0.0f);
	
	if (sDOTn > 0.0f) {
		vec3 viewDirection = normalize(Vertex - ViewPosition);
		vec3 reflectedRayDirection = normalize(viewDirection + pLightDir);
		return pow(max(dot(-reflectedRayDirection, Normal), 0.0f), pShineness);
	}
	return 0.0f;
}

vec3 lightRay;

vec3 textureColor;
vec3 specularColor;
vec3 cubemapColor;

vec3 diffuse;

float shineness = 2.0f;
vec3 specular;
float amount;

void main( void ) {
	if (pick == 1) {
		fragment_color = vec4(diffuseColor, 1.0f);
	}
	if (pick == 0) {
		lightRay = reflect(normalize(Vertex - ViewPosition), Normal);

		textureColor = texture(diffuseTexture, UV).rgb;
		specularColor = texture(specularTexture, UV).rgb;
		cubemapColor = texture(cubemapTexture, lightRay).rgb;


		diffuse = diffuseColor * textureColor;

		amount = refl(-lightRay, shineness);
		specular = (cubemapColor * specularColor * vec3(amount));
	

		fragment_color = vec4(diffuse + specular, 1.0f);
	}
}
