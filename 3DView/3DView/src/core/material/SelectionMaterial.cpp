#include "core/material/SelectionMaterial.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
SelectionMaterial::SelectionMaterial(glm::vec3 pColor) {
	_color = pColor;

	_vertices.push_back(glm::vec3(0.0f));
	_vertices.push_back(glm::vec3(0.0f));

	_vertices.push_back(glm::vec3(0.0f));
	_vertices.push_back(glm::vec3(0.0f));

	_vertices.push_back(glm::vec3(0.0f));
	_vertices.push_back(glm::vec3(0.0f));

	_vertices.push_back(glm::vec3(0.0f));
	_vertices.push_back(glm::vec3(0.0f));

	for (int i = 0; i < _vertices.size(); i++)
		_indices.push_back(i);


	_lazyInitializeShader(Config::s["selection_shader_vertex"]);
	//_bufferPositive();
	_buffer(_vertexBufferID, _indexBufferID, _vertices, _indices, GL_STATIC_DRAW);
}

SelectionMaterial::~SelectionMaterial()
{
	// Space.cpp deletes all ShaderPrograms (delete _shader;)
	_shader = NULL;

	glDeleteBuffers(1, &_vertexBufferID);
	glDeleteBuffers(1, &_indexBufferID);
}


// -------------------------------------------- PRIVATE --------------------------------------------
void SelectionMaterial::_lazyInitializeShader(std::string pShader) {
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];
	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["selection_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["selection_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}

	_uWidth = _shader->getUniformLocation("width");
	_uHeight = _shader->getUniformLocation("height");

	_uDiffuseColor = _shader->getUniformLocation("diffuseColor");

	_aVertex = _shader->getAttribLocation("vertex");

}
void SelectionMaterial::render(Camera* pCamera) {
	if (renderSelection) {
		_shader->use();

		glUniform1i(_uWidth, _width);
		glUniform1i(_uHeight, _height);


		glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(_color));

		_streamToOpenGL(_vertexBufferID, _indexBufferID, _indices.size());
	}
}

void SelectionMaterial::onMouseDown(glm::ivec2 pMousePosition)
{
	renderSelection = true;
	//Left
	_vertices.at(0).x = pMousePosition.x;
	_vertices.at(0).y = pMousePosition.y;
	_vertices.at(1).x = pMousePosition.x;
	_vertices.at(1).y = pMousePosition.y;
	//Top
	_vertices.at(2).x = pMousePosition.x;
	_vertices.at(2).y = pMousePosition.y;
	_vertices.at(3).x = pMousePosition.x;
	_vertices.at(3).y = pMousePosition.y;
	//Right
	_vertices.at(4).x = pMousePosition.x;
	_vertices.at(4).y = pMousePosition.y;
	_vertices.at(5).x = pMousePosition.x;
	_vertices.at(5).y = pMousePosition.y;
	//Bottom
	_vertices.at(6).x = pMousePosition.x;
	_vertices.at(6).y = pMousePosition.y;
	_vertices.at(7).x = pMousePosition.x;
	_vertices.at(7).y = pMousePosition.y;

	_buffer(_vertexBufferID, _indexBufferID, _vertices, _indices, GL_DYNAMIC_DRAW);

}

void SelectionMaterial::onMouseMove(glm::ivec2 pMousePosition)
{
	if (renderSelection) {
		//Left
		_vertices.at(1).y = pMousePosition.y;
		//Top
		_vertices.at(3).x = pMousePosition.x;
		//Right
		_vertices.at(4).x = pMousePosition.x;
		_vertices.at(5).x = pMousePosition.x;
		_vertices.at(5).y = pMousePosition.y;
		//Bottom
		_vertices.at(6).y = pMousePosition.y;
		_vertices.at(7).x = pMousePosition.x;
		_vertices.at(7).y = pMousePosition.y;

		_buffer(_vertexBufferID, _indexBufferID, _vertices, _indices, GL_DYNAMIC_DRAW);
	}
}

void SelectionMaterial::onMouseRelease()
{
	renderSelection = false;
}

void SelectionMaterial::onResize(int pWidth, int pHeight)
{
	_width = pWidth;
	_height = pHeight;
}

int SelectionMaterial::startX()
{
	return glm::min(_vertices.at(0).x, _vertices.at(7).x);
}

int SelectionMaterial::startY()
{
	return glm::min(_vertices.at(0).y, _vertices.at(7).y);
}

int SelectionMaterial::sizeX()
{
	return glm::max(1, (int)glm::round(glm::abs(_vertices.at(0).x - _vertices.at(7).x)));
}

int SelectionMaterial::sizeY()
{
	return glm::max(1, (int)glm::round(glm::abs(_vertices.at(0).y - _vertices.at(7).y)));
}


void SelectionMaterial::_buffer(GLuint& pVertexBufferID, GLuint& pIndexBufferID, std::vector<glm::vec3>& pVertices, std::vector<unsigned int>& pIndices, int pGL_DRAW_TYPE)
{
	if (pVertexBufferID == -1)
		glGenBuffers(1, &pVertexBufferID);
	glBindBuffer(GL_ARRAY_BUFFER, pVertexBufferID);
	glBufferData(GL_ARRAY_BUFFER, pVertices.size() * sizeof(glm::vec3), &pVertices[0], pGL_DRAW_TYPE);


	if (pIndexBufferID == -1)
		glGenBuffers(1, &pIndexBufferID);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, pIndexBufferID);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, pIndices.size() * sizeof(unsigned int), &pIndices[0], pGL_DRAW_TYPE);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
}

void SelectionMaterial::_streamToOpenGL(GLint pVBID, GLint pIBID, int pIndicesSize)
{
	if (_aVertex != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, pVBID);
		glEnableVertexAttribArray(_aVertex);
		glVertexAttribPointer(_aVertex, 3, GL_FLOAT, GL_FALSE, 0, 0);
	}
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, pIBID);
	glDrawElements(GL_LINES, pIndicesSize, GL_UNSIGNED_INT, (GLvoid*)0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	if (_aVertex != -1) glDisableVertexAttribArray(_aVertex);
}