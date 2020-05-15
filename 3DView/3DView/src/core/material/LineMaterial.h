#pragma once

#include "core/Config.h"
#include <boost/algorithm/string.hpp>

#include "core/ShaderProgram.h"
#include <glm/gtc/type_ptr.hpp>

#include <map>
#include "core/MObject.h"

class LineMaterial
{
public:
	LineMaterial(float pPositiveNegativeLength);
	~LineMaterial();



    void addFrom(MObject* pSelectedObject, MObject* pFrom);
    void removeFrom(MObject* pFrom);

    void addTo(MObject* pSelectedObject, MObject* pTo);
    void removeTo(MObject* pTo);

	void render(glm::mat4* pViewProjection);

    void toggleRender(int pType, bool pBool);
    bool help(std::string pTag, int pType);
    void toggleRender(int pType, int pID, bool pBool);
    bool help(std::string pTag, int pType, int pID);

    void clearFromTo();

private:
    std::map<int, bool> _renderStatus;

    class Line {
    public:
        Line();
        Line(glm::vec3 pStart, glm::vec3 pEnd);
        ~Line();

        glm::vec3 start;
        glm::vec3 end;

        bool render;
    private:
    };

    ShaderProgram* _shader = NULL;
    void _lazyInitializeShader(std::string pShader);
    
    GLuint _uVPMatrix;
    GLuint _uDiffuseColor;
    GLuint _aVertex;

    glm::vec3 _positiveColor;
    GLuint _positiveVertexBufferID = -1;
    GLuint _positiveIndexBufferID = -1;
    std::vector<glm::vec3> _positiveVertices;
    std::vector<unsigned> _positiveIndices;

    glm::vec3 _negativeColor;
    GLuint _negativeVertexBufferID = -1;
    GLuint _negativeIndexBufferID = -1;
    std::vector<glm::vec3> _negativeVertices;
    std::vector<unsigned> _negativeIndices;

    bool _addedRemovedFrom;
    std::map<std::string, Line> _from;
    glm::vec3 _fromColor;
    GLuint _fromVertexBufferID = -1;
    GLuint _fromIndexBufferID = -1;
    std::vector<glm::vec3> _fromVertices;
    std::vector<unsigned> _fromIndices;

    bool _addedRemovedTo;
    std::map<std::string, Line> _to;
    glm::vec3 _toColor;
    GLuint _toVertexBufferID = -1;
    GLuint _toIndexBufferID = -1;
    std::vector<glm::vec3> _toVertices;
    std::vector<unsigned> _toIndices;

    void _buffer(GLuint& pVertexBufferID, GLuint& pIndexBufferID, std::vector<glm::vec3>& pVertices, std::vector<unsigned int>& pIndices, int pGL_DRAW_TYPE);
    void _render(glm::vec3& pColor, GLuint& pVertexBufferID, GLuint& pIndexBufferID, std::vector<unsigned int>& pIndices, glm::mat4* pViewProjection);
    
    void _streamToOpenGL(GLint pVBID, GLint pIBID, int pIndicesSize);

    void _clearFrom();
    void _clearTo();

};