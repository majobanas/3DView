#pragma once

#include "core/Config.h"
#include <boost/algorithm/string.hpp>

#include "core/ShaderProgram.h"
#include <glm/gtc/type_ptr.hpp>

#include <map>
#include "core/MObject.h"

class SelectionMaterial
{
public:
    SelectionMaterial(glm::vec3 pColor);
	~SelectionMaterial();

    bool renderSelection = false;
	void render(Camera* pCamera);

    void onMouseDown(glm::ivec2 pMousePosition);
    void onMouseMove(glm::ivec2 pMousePosition);
    void onMouseRelease();
    
    void onResize(int pWidth, int pHeight);

    int startX();
    int startY();
    int sizeX();
    int sizeY();

private:

    ShaderProgram* _shader = NULL;
    void _lazyInitializeShader(std::string pShader);

    int _width;
    int _height;

    GLuint _uWidth;
    GLuint _uHeight;

    GLuint _uDiffuseColor;
    GLuint _aVertex;

    glm::vec3 _color;

    GLuint _vertexBufferID = -1;
    GLuint _indexBufferID = -1;
    std::vector<glm::vec3> _vertices;
    std::vector<unsigned> _indices;

    void _buffer(GLuint& pVertexBufferID, GLuint& pIndexBufferID, std::vector<glm::vec3>& pVertices, std::vector<unsigned int>& pIndices, int pGL_DRAW_TYPE);
    
    void _streamToOpenGL(GLint pVBID, GLint pIBID, int pIndicesSize);
};