#include "core/Space.h"

#include <iostream>

Space::Space()
{
	std::cout << "----Creating Space----" << std::endl;
	_objects = std::vector<Object*>();

	for (int i = 0; i < 10; i++) {
		_objects.push_back(new Object(glm::vec3(i, i, i)));
	}
}

Space::~Space()
{
	std::cout << "Deleting Space" << std::endl;
	for (std::vector<Object*>::iterator object = _objects.begin(); object != _objects.end(); object++) {
		delete (*object);
		(*object) = NULL;
	}
	_objects.clear();
}

void Space::update(float pElapsedTime)
{
	for (int i = 0; i < _objects.size(); i++) {
		_objects.at(i)->update(pElapsedTime);
	}
}

void Space::render(glm::mat4* pViewProjection)
{
	Object* object = NULL;
	for (int i = 0; i < _objects.size(); i++) {
		object = _objects.at(i);
		object->getMaterial()->render(object->getMesh(), object->getTransform(), pViewProjection);
	}
}
