#pragma once

#include <vector>
#include "core/Object.h"

class Space {
public:
	Space();
	~Space();

	void update(float pElapsedTime);

	void render(glm::mat4* pViewProjection);

private:
	std::vector<Object*> _objects;
};