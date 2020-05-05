#pragma once

#include <vector>

#include "core/Transform.h"
#include "core/Mesh.h"

#include "core/material/AbstractMaterial.h"
#include "core/material/ColorMaterial.h"
#include "core/material/TextureMaterial.h"

#include "core/material/TextMaterial.h"
#include "core/material/SkyboxMaterial.h"

class Object {
public:
	Object(float pScale, std::string pModel, AbstractMaterial* pMaterial);
	virtual ~Object();

	Transform* getTransform();
	Mesh* getMesh();
	AbstractMaterial* getMaterial();
protected:
	Transform* _transform = NULL;
	Mesh* _mesh = NULL;
	AbstractMaterial* _material = NULL;


private:

};