#pragma once

#include "GL/glew.h"
#include "glm/glm.hpp"
#include "glm/gtx/transform.hpp"

class Camera {
public:
	Camera(glm::vec3 pPosition, float pFov, float pAspectRatio, float pNearPlane, float pFarPlane);
	~Camera();

	glm::mat4* getTransform();
	glm::mat4* getProjection();
	glm::mat4* getViewProjection();

private:
	glm::mat4* _transform = NULL;
	float _fov = 0.0f;
	float _aspectRatio = 0.0f;
	float _nearPlane = 0.0f;
	float _farPlane = 0.0f;
	glm::mat4* _projection = NULL;
	glm::mat4* _viewProjection = NULL;
};
