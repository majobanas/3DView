#include "core/Object.h"

#include <iostream>
#include "cfg.h"

Object::Object(glm::vec3 pPosition)
{
	std::cout << "----Creating Object----" << std::endl;
	_transform = new glm::mat4(glm::translate(pPosition));
	_mesh = Mesh::load(cfg::MODELS + "cube_flat.obj");
	_material = new Material(glm::vec3(1, 1, 1));
}

Object::~Object()
{
	std::cout << "Deleting Object" << std::endl;
	delete _transform;
	_transform = NULL;
	delete _mesh;
	_mesh = NULL;
	delete _material;
	_material = NULL;
}

glm::mat4* Object::getTransform()
{
	return _transform;
}

Mesh* Object::getMesh()
{
	return _mesh;
}

Material* Object::getMaterial()
{
	return _material;
}
