#pragma once

#include "core/material/AbstractMaterial.h"

class ColorMaterial : public AbstractMaterial {
public:
	ColorMaterial(glm::vec3 pColor, float pPickIdentifierColor);
	~ColorMaterial() override;

	void Test() override;

	void renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pView, glm::mat4* pProjection) override;
	void render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pView, glm::mat4* pProjection) override;

protected:

private:
	void _lazyInitializeShader(std::string pShader);

	glm::vec3 _color;

	GLint _uVPMatrix;
	GLint _uMMatrix;
	GLint _uDiffuseColor;

	GLint _aVertex;
	GLint _aNormal;
	GLint _aUV;

};
