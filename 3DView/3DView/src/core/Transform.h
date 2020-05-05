#pragma once

#include "glm/glm.hpp"
#include "glm/gtx/transform.hpp"
#include "glm/gtx/string_cast.hpp"

class Transform {
public:
	Transform(glm::vec3 pPosition);
	~Transform();

	glm::mat4* getTransform();
	glm::mat4 getInverse();

	glm::vec3 getPosition();
	void setPosition(glm::vec3 pPosition);

	void scale(glm::vec3 pScale);

private:
	glm::mat4* _transform = NULL;
};
