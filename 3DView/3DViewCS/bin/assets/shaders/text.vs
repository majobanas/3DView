//TEXT VERTEX SHADER



in vec4 vertex;

out vec2 TexCoords;

uniform mat4 pMatrix;
uniform mat4 vMatrix;
uniform mat4 cMatrix;
uniform mat4 mMatrix;
mat4 tMatrix = mat4(1.0f);

float xOffset;
uniform float xStep;

mat4 lookAt;

void main()
{
    xOffset = (length(vec3(mMatrix[1])) / 2.0f) + 1;
    tMatrix[3] = mMatrix[3];

    lookAt[0] = cMatrix[0];
    lookAt[1] = cMatrix[1];
    lookAt[2] = vMatrix[2];
    lookAt[3] = vec4(vec3(0.0f), 1.0f);
    gl_Position = pMatrix * vMatrix * tMatrix * lookAt * vec4(vertex.x + xStep + xOffset, vertex.y , 0.0f, 1.0f);
    TexCoords = vertex.zw;
}  