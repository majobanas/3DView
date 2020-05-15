#include "SkyboxMaterial.h"

SkyboxMaterial::SkyboxMaterial(Texture* pTexture)
	: AbstractMaterial(glm::vec3(0.0f))
{
	_cubemapTexture = pTexture;
	_lazyInitializeShader(Config::s["skybox_shader_vertex"]);
}

SkyboxMaterial::~SkyboxMaterial()
{
	// Space.cpp deletes all textures
	_cubemapTexture = NULL;
}

void SkyboxMaterial::Test()
{
}

void SkyboxMaterial::renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection)
{
	// nothing
}

void SkyboxMaterial::render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection)
{
	_shader->use();

	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pTransform));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr(*pProjection * glm::inverse(*pModel)));


	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_CUBE_MAP, _cubemapTexture->getTextureID()); 
	glUniform1i(_uCubemapTexture, 0);

	glFrontFace(GL_CW);
	pMesh->streamToOpenGL(_aVertex, -1, -1);
	glFrontFace(GL_CCW);
}

void SkyboxMaterial::setSkyboxCubemap(Texture* pCubemap)
{
	_cubemapTexture = pCubemap;
}

void SkyboxMaterial::_lazyInitializeShader(std::string pShader)
{
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];
	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["skybox_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["skybox_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}
	_aVertex = _shader->getAttribLocation("vertex");

	_uVPMatrix = _shader->getUniformLocation("vpMatrix");
	_uMMatrix = _shader->getUniformLocation("mMatrix");

	_uCubemapTexture = _shader->getUniformLocation("cubemapTexture");
}
