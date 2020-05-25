//SELECTION FRAGMENT SHADER


out vec4 fragment_color;

uniform vec3 diffuseColor;

void main()
{   
    fragment_color = vec4(diffuseColor, 1.0f);
}  