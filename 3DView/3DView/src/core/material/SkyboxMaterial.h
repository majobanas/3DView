#pragma once

#include "core/material/AbstractMaterial.h"
#include "core/Texture.h"

class SkyboxMaterial : public AbstractMaterial {
public:
	SkyboxMaterial(Texture* pTexture);
	~SkyboxMaterial() override;
	
	void Test() override;

	void renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) override;
	void render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) override;

private:
	void _lazyInitializeShader(std::string pShader);

	Texture* _cubemapTexture = NULL;

	// Shader -----------------------
	GLuint _aVertex;

	GLuint _uVPMatrix;
	GLuint _uMMatrix;

	GLuint _uCubemapTexture;
	// ------------------------------

};