#include "core/material/AbstractMaterial.h"

AbstractMaterial::AbstractMaterial(glm::vec3 pPickIdentifierColor)
{
	_pickIdentifierColor = glm::vec3(pPickIdentifierColor);
}

AbstractMaterial::~AbstractMaterial()
{
	// Space.cpp deletes all shaders
	_shader = NULL;
}

void AbstractMaterial::setPickIdentifier(float pPickIdentifier)
{
	_pickIdentifierColor = glm::vec3(pPickIdentifier);
}

