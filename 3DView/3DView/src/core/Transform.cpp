#include "core/Transform.h"

// -------------------------------------------- PUBLIC --------------------------------------------
Transform::Transform(glm::vec3 pPosition)
{
	_transform = new glm::mat4(glm::translate(pPosition));
}

Transform::~Transform()
{
	delete _transform;
	_transform = NULL;
}

glm::mat4* Transform::getTransform()
{
	return _transform;
}

glm::mat4 Transform::getInverse()
{
	return glm::inverse(*_transform);
}

glm::vec3 Transform::getPosition()
{
	return (*_transform)[3];
}

void Transform::setPosition(glm::vec3 pPosition)
{
	(*_transform)[3] = glm::vec4(pPosition, 1.0f);
}

void Transform::scale(glm::vec3 pScale)
{
	*_transform = glm::scale(*_transform, pScale);
}
