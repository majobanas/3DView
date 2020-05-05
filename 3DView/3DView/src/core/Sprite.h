#pragma once

#include "core/Config.h"

#include "glm/glm.hpp"
#include "glm/gtc/type_ptr.hpp"
#include "glm/gtx/string_cast.hpp"

#include "core/ShaderProgram.h"
#include "core/Transform.h"
#include "core/Texture.h"
#include "core/Mesh.h"

class Sprite {

public:
	Sprite();
	Sprite(std::string pFileName);
	~Sprite();

	void setPosition(int pX, int pY);
	void setScale(int pX, int pY);

	int getWidth();
	int getHeight();

	int getScaledWidth();
	int getScaledHeight();

	bool contains(glm::ivec2 pMousePosition);

	void render(glm::mat4* pProjection);

private:

	GLuint _uMMatrix;
	GLuint _uPMatrix;
	GLuint _uDiffuseTexture;

	GLuint _aVertex;
	GLuint _aUV;

	ShaderProgram* _shader = NULL;

	void _lazyInitializeShader(std::string pShader);

	Transform* _transform = NULL;

	Texture* _texture = NULL;

	void _buffer();

	GLuint _vertexBufferId;
	GLuint _uvBufferId;
	GLuint _indexBufferId;

	std::vector<glm::vec3> _vertices;
	std::vector<glm::vec2> _uvs;
	std::vector<unsigned> _indices;

	void _streamToOpenGL(GLint pVerticesAttrib, GLint pNormalsAttrib, GLint pUVsAttrib);

};
