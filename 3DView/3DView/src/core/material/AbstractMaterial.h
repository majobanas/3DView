#pragma once

#include "core/Config.h"

#include <gl/glew.h>
#include "glm/glm.hpp"

#include "core/Mesh.h"
#include "core/ShaderProgram.h"

#include "glm/gtc/type_ptr.hpp"
#include "glm/gtx/string_cast.hpp"

class AbstractMaterial {
public:
	AbstractMaterial(glm::vec3 pPickIdentifierColor);
	virtual ~AbstractMaterial() = 0;

	virtual void Test() = 0;

	virtual void renderPickIdentifier(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) = 0;
	virtual void render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pModel, glm::mat4* pProjection) = 0;

	void setPickIdentifier(float pPickIdentifier);

protected:
	ShaderProgram* _shader = NULL;

	glm::vec3 _pickIdentifierColor;

private:

};
