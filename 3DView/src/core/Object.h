#pragma once

#include "glm/glm.hpp"
#include "glm/gtx/transform.hpp"
#include "glm/gtx/string_cast.hpp"

#include "core/Mesh.h"
#include "core/Material.h"

class Object {
public:
	Object(glm::vec3 pPosition);
	~Object();

	void update(float pElapsedTime);

	glm::mat4* getTransform();
	Mesh* getMesh();
	Material* getMaterial();
private:
	glm::mat4* _transform = NULL;
	Mesh* _mesh = NULL;
	Material* _material = NULL;
};
