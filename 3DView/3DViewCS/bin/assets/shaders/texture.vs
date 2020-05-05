//DIFFUSE TEXTURE VERTEX SHADER


in vec3 vertex;
in vec3 normal;
in vec2 uv;

uniform	mat4 mMatrix;
uniform mat4 vMatrix;
uniform	mat4 vpMatrix;

mat4 lookAt;
vec3 direction;
vec3 up = vec3(1.0f);
vec3 right;

out vec3 Vertex;
out vec3 Normal;
out vec2 UV;

out vec3 ViewPosition;

vec4 lookDirectly() {
    direction = normalize(vec3(vMatrix[3]) - vec3(mMatrix[3]));
    right = normalize(cross(vec3(vMatrix[1]), direction));
    up = cross(direction, right);
    lookAt[0] = vec4(right, 0.0f)* vec4(vec3(length(vec3(mMatrix[0]))), 0.0f);
    lookAt[1] = vec4(up, 0.0f)* vec4(vec3(length(vec3(mMatrix[1]))), 0.0f);
    lookAt[2] = vec4(direction, 0.0f)* vec4(vec3(length(vec3(mMatrix[2]))), 0.0f);
    lookAt[3] = mMatrix[3] * vec4(1.0f);
    Vertex = vec3(lookAt * vec4(vertex, 1.0f));
    Normal = normalize(vec3(lookAt * vec4(normal, 0.0f)));
    return vpMatrix * lookAt * vec4(vertex, 1.0f);
}

vec4 justFaceMe() {
    lookAt[0] = vMatrix[0] * vec4(vec3(length(vec3(mMatrix[0]))), 0.0f);
    lookAt[1] = vMatrix[1] * vec4(vec3(length(vec3(mMatrix[1]))), 0.0f);
    lookAt[2] = vMatrix[2] * vec4(vec3(length(vec3(mMatrix[2]))), 0.0f);
    lookAt[3] = mMatrix[3] * vec4(1.0f);
    Vertex = vec3(lookAt * vec4(vertex, 1.0f));
    Normal = normalize(vec3(lookAt * vec4(normal, 0.0f)));
    return vpMatrix * lookAt * vec4(vertex, 1.0f);
}

vec4 basic() {
    Vertex = vec3(mMatrix * vec4(vertex, 1.0f));
    Normal = normalize(vec3(mMatrix * vec4(normal, 0.0f)));
    return vpMatrix * mMatrix * vec4(vertex, 1.0f);
}


void main( void ){
    UV = uv;
    ViewPosition = vec3(vMatrix[3]);
    // Varianta 1
    //gl_Position = lookDirectly();
    // Varianta 2
    gl_Position = justFaceMe();
    // Varianta 3
    //gl_Position = basic();
}
