#include "core/Sprite.h"

Sprite::Sprite(std::string pFileName)
{
	_texture = Texture::load(pFileName);
	
	_transform = new Transform(glm::vec3(1.0f));
	_transform->scale(glm::vec3(_texture->getWidth(), _texture->getHeight(), 0.0f));
	_transform->scale(glm::vec3(0.5f, 0.5f, 0.0f));


	_vertices = {
		{0.0f, 1.0f, 0.0f},
		{1.0f, 0.0f, 0.0f},
		{0.0f, 0.0f, 0.0f},

		{0.0f, 1.0f, 0.0f},
		{1.0f, 1.0f, 0.0f},
		{1.0f, 0.0f, 0.0f}
	};
	_uvs = {
		{0.0f, 1.0f},
		{1.0f, 0.0f},
		{0.0f, 0.0f},

		{0.0f, 1.0f},
		{1.0f, 1.0f},
		{1.0f, 0.0f}
	};
	_indices = {
		0, 1, 2, 3, 4, 5
	};

	_lazyInitializeShader(Config::s["sprite_shader_vertex"]);

	_buffer();
}

Sprite::~Sprite()
{
	//delete _texture;
	_texture = NULL;
	delete _transform;
	_transform = NULL;
	// Space.cpp deletes all shaders
	_shader = NULL;
}

void Sprite::setPosition(int pX, int pY)
{
	_transform->setPosition(glm::vec3(pX, pY, 0.0f));
}

void Sprite::setScale(int pX, int pY)
{
	_transform->scale(glm::vec3(_texture->getWidth() * pX, _texture->getHeight() * pY, 0.0f));
}

int Sprite::getWidth()
{
	return _texture->getWidth();
}

int Sprite::getHeight()
{
	return _texture->getHeight();
}

int Sprite::getScaledWidth()
{
	return glm::length(glm::vec3((*_transform->getTransform())[0]));
}

int Sprite::getScaledHeight()
{
	return glm::length(glm::vec3((*_transform->getTransform())[1]));;
}

bool Sprite::contains(glm::ivec2 pMousePosition)
{
	glm::vec3 position = _transform->getPosition();
	return	position.x < pMousePosition.x && pMousePosition.x < position.x + getWidth() &&
			position.y < pMousePosition.y && pMousePosition.y < position.y + getHeight();
}

void Sprite::render(glm::mat4* pProjection)
{
	_shader->use();

	//setup texture slot 0
	glActiveTexture(GL_TEXTURE0);
	//bind the texture to the current active slot
	glBindTexture(GL_TEXTURE_2D, _texture->getTextureID());
	//tell the shader the texture slot for the diffuse texture is slot 0
	glUniform1i(_uDiffuseTexture, 0);

	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*_transform->getTransform()));
	glUniformMatrix4fv(_uPMatrix, 1, GL_FALSE, glm::value_ptr(*pProjection));

	_streamToOpenGL(_aVertex, -1, _aUV);
}



void Sprite::_lazyInitializeShader(std::string pShader)
{
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];
	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["sprite_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["sprite_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}

	_uMMatrix = _shader->getUniformLocation("mMatrix");
	_uPMatrix = _shader->getUniformLocation("pMatrix");
	_uDiffuseTexture = _shader->getUniformLocation("diffuseTexture");

	_aVertex = _shader->getAttribLocation("vertex");
	_aUV = _shader->getAttribLocation("uv");
}

void Sprite::_buffer()
{

	glGenBuffers(1, &_vertexBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferId);
	//std::cout << "V: " << std::to_string(_vertices.size()) << std::endl;
	glBufferData(GL_ARRAY_BUFFER, _vertices.size() * sizeof(glm::vec3), &_vertices[0], GL_STATIC_DRAW);

	glGenBuffers(1, &_uvBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, _uvBufferId);
	//std::cout << "UV: " << std::to_string(_uvs.size()) << std::endl;
	glBufferData(GL_ARRAY_BUFFER, _uvs.size() * sizeof(glm::vec2), &_uvs[0], GL_STATIC_DRAW);

	glGenBuffers(1, &_indexBufferId);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _indexBufferId);
	//std::cout << "I: " << std::to_string(_indices.size()) << std::endl;
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indices.size() * sizeof(unsigned int), &_indices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
}

Sprite::Sprite()
{
}

void Sprite::_streamToOpenGL(GLint pVerticesAttrib, GLint pNormalsAttrib, GLint pUVsAttrib) {
	if (pVerticesAttrib != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferId);
		glEnableVertexAttribArray(pVerticesAttrib);
		glVertexAttribPointer(pVerticesAttrib, 3, GL_FLOAT, GL_FALSE, 0, 0);
	}

	/*if (pNormalsAttrib != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, _normalBufferId);
		glEnableVertexAttribArray(pNormalsAttrib);
		glVertexAttribPointer(pNormalsAttrib, 3, GL_FLOAT, GL_TRUE, 0, 0);
	}*/

	if (pUVsAttrib != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, _uvBufferId);
		glEnableVertexAttribArray(pUVsAttrib);
		glVertexAttribPointer(pUVsAttrib, 2, GL_FLOAT, GL_FALSE, 0, 0);
	}

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _indexBufferId);

	glDrawElements(GL_TRIANGLES, _indices.size(), GL_UNSIGNED_INT, (GLvoid*)0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	if (pUVsAttrib != -1) glDisableVertexAttribArray(pUVsAttrib);
	if (pNormalsAttrib != -1) glDisableVertexAttribArray(pNormalsAttrib);
	if (pVerticesAttrib != -1) glDisableVertexAttribArray(pVerticesAttrib);
}
