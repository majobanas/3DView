#include "core/material/TextureMaterial.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
TextureMaterial::TextureMaterial(Texture* pTexture, float pPickIdentifierColor, Texture* pSpecular, Texture* pCubemap, glm::vec3 pColor)
	: AbstractMaterial(glm::vec3(pPickIdentifierColor))
{
	_color = pColor;
	_diffuseTexture = pTexture;
	_specularTexture = pSpecular;
	_cubemapTexture = pCubemap;

	_lazyInitializeShader(Config::s["texture_shader_vertex"]);
}

TextureMaterial::~TextureMaterial()
{
	// Space.cpp deletes all textures
	_diffuseTexture = NULL;
}

void TextureMaterial::Test()
{
	Debug::log("Color " + glm::to_string(_color));
	Debug::log("PickIdentifierColor " + glm::to_string(_pickIdentifierColor));
}

void TextureMaterial::renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection)
{
	_shader->use();

	glUniform1i(_uPick, 1);
	glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(_pickIdentifierColor));
	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pTransform));
	glUniformMatrix4fv(_uVMatrix, 1, GL_FALSE, glm::value_ptr(*pModel));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr(*pProjection * glm::inverse(*pModel)));

	pMesh->streamToOpenGL(_aVertex, _aNormal, _aUV);
}

void TextureMaterial::render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) {
	_shader->use();

	glUniform1i(_uPick, 0);

	
	glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(_color));


	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, _diffuseTexture->getTextureID());
	glUniform1i(_uDiffuseTexture, 0);

	if (_specularTexture != NULL) {
		glActiveTexture(GL_TEXTURE1);
		glBindTexture(GL_TEXTURE_2D, _specularTexture->getTextureID());
		glUniform1i(_uSpecularTexture, 1);
	}

	if (_cubemapTexture != NULL) {
		glActiveTexture(GL_TEXTURE2);
		glBindTexture(GL_TEXTURE_CUBE_MAP, _cubemapTexture->getTextureID());
		glUniform1i(_uCubemapTexture, 2);
	}


	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pTransform));
	glUniformMatrix4fv(_uVMatrix, 1, GL_FALSE, glm::value_ptr(*pModel));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr(*pProjection * glm::inverse(*pModel)));

	pMesh->streamToOpenGL(_aVertex, _aNormal, _aUV);
}
void TextureMaterial::setSkyboxCubemap(Texture* pCubemap)
{
	_cubemapTexture = pCubemap;
}
// -------------------------------------------- PRIVATE --------------------------------------------
void TextureMaterial::_lazyInitializeShader(std::string pShader) {
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];
	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["texture_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["texture_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}
	_uPick = _shader->getUniformLocation("pick");

	_uMMatrix = _shader->getUniformLocation("mMatrix");
	_uVMatrix = _shader->getUniformLocation("vMatrix");
	_uVPMatrix = _shader->getUniformLocation("vpMatrix");
	_uDiffuseColor = _shader->getUniformLocation("diffuseColor");
	_uDiffuseTexture = _shader->getUniformLocation("diffuseTexture");
	_uSpecularTexture = _shader->getUniformLocation("specularTexture");
	_uCubemapTexture = _shader->getUniformLocation("cubemapTexture");

	_aVertex = _shader->getAttribLocation("vertex");
	_aNormal = _shader->getAttribLocation("normal");
	_aUV = _shader->getAttribLocation("uv");
}
