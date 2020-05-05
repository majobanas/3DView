//TEXT FRAGMENT SHADER



in vec2 TexCoords;

out vec4 fragment_color;

uniform sampler2D text;
uniform vec3 textColor;

void main()
{   
    //float alpha = texture(text, TexCoords).r;
    //if (alpha == 1.0f) {
        fragment_color = vec4(textColor, texture(text, TexCoords).r);
    //} else discard;
}  