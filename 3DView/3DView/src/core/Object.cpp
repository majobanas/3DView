#include "core/Object.h"
#include "core/Space.h"

class Space;

Object::Object(float pScale, std::string pModel, AbstractMaterial* pMaterial)
	:	_transform(new Transform(glm::vec3(0.0f))),
		_mesh(Mesh::load(pModel)),
		_material(pMaterial)
{
	_transform->scale(glm::vec3(pScale));
}

Object::~Object()
{
}


Transform* Object::getTransform()
{
	return _transform;
}

Mesh* Object::getMesh()
{
	return _mesh;
}

AbstractMaterial* Object::getMaterial()
{
	return _material;
}