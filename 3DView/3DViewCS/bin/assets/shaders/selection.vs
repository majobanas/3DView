//SELECTION VERTEX SHADER



in vec4 vertex;

uniform int width;
uniform int height;

float x;
float y;

void main()
{
    x = ((vertex.x * 2) - width) / width;
    y = -((vertex.y * 2) - height) / height;

    gl_Position = vec4(x, y , 0.0f, 1.0f);
}  