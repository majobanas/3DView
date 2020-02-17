#pragma once

#include <vector>
#include "core/Object.h"
#include "core/Config.h"

class Space {
public:
	Space();
	~Space();

	void update(float pElapsedTime);

	void render(glm::mat4* pViewProjection);

private:
	std::vector<Object*> _objects;
};