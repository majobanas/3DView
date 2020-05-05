#include "core/material/ColorMaterial.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
ColorMaterial::ColorMaterial(glm::vec3 pColor, float pPickIdentifierColor)
	: AbstractMaterial(glm::vec3(pPickIdentifierColor))
{
	_color = pColor;
	_lazyInitializeShader(Config::s["color_shader_vertex"]);
}

ColorMaterial::~ColorMaterial()
{
}

void ColorMaterial::Test()
{
	Debug::now("Color " + glm::to_string(_color));
	Debug::now("PickIdentifierColor " + glm::to_string(_pickIdentifierColor));
}

void ColorMaterial::renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection)
{
	_shader->use();

	glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(_pickIdentifierColor));
	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pTransform));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr(*pProjection * glm::inverse(*pModel)));

	pMesh->streamToOpenGL(_aVertex, _aNormal, _aUV);
}

void ColorMaterial::render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection)
{
	_shader->use();
	
	glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(_color));

	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pTransform));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr((*pProjection * glm::inverse(*pModel))));

	pMesh->streamToOpenGL(_aVertex, _aNormal, _aUV);
}
// -------------------------------------------- PRIVATE --------------------------------------------
void ColorMaterial::_lazyInitializeShader(std::string pShader)
{
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];
	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["color_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["color_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}
	_uMMatrix = _shader->getUniformLocation("mMatrix");
	_uVPMatrix = _shader->getUniformLocation("vpMatrix");
	_uDiffuseColor = _shader->getUniformLocation("diffuseColor");

	_aVertex = _shader->getAttribLocation("vertex");
	_aNormal = _shader->getAttribLocation("normal");
	_aUV = _shader->getAttribLocation("uv");
}
