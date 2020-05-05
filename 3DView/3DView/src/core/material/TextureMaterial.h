#pragma once

#include "core/material/AbstractMaterial.h"
#include "core/Texture.h"

class TextureMaterial : public AbstractMaterial {
public:
	TextureMaterial(Texture* pTexture, float pPickIdentifierColor, Texture* pSpecular = NULL, Texture* pCubemap = NULL, glm::vec3 pColor = glm::vec3(1.0f));
	~TextureMaterial() override;

	void Test() override;

	void renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) override;
	void render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) override;

protected:

private:
	void _lazyInitializeShader(std::string pShader);

	glm::vec3 _color;
	Texture* _diffuseTexture = NULL;
	Texture* _specularTexture = NULL;
	Texture* _cubemapTexture = NULL;

	GLint _uPick;

	GLint _uVPMatrix;
	GLint _uVMatrix;
	GLint _uMMatrix;

	GLint _uDiffuseColor;
	GLint _uDiffuseTexture;
	GLint _uSpecularTexture;
	GLint _uCubemapTexture;

	GLint _aVertex;
	GLint _aNormal;
	GLint _aUV;
};
