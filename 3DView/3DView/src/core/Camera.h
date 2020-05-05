#pragma once

#include <iostream>

#include "GL/glew.h"
#include "glm/glm.hpp"
#include "glm/gtx/transform.hpp"
#include "glm/gtx/rotate_vector.hpp"

#include "SFML/Window/Mouse.hpp"
#include "SFML/Graphics.hpp"

#include "core/Config.h"
#include "core/Input.h"
#include "core/Transform.h"

class Camera {
public:
	Camera(sf::RenderWindow* pRenderWindow, glm::vec3 pPosition, float pFov, float pNearPlane, float pFarPlane);
	~Camera();

	void update(float pElapsedTime);

	Transform* getTransform();
	glm::mat4* getOrtho();
	glm::mat4* getProjection();
	glm::mat4* getViewProjection();

	void updateResolution();
	float getWidth();
	float getHeight();
	
	void setMouseWheelDelta(float pDelta);
private:
	sf::RenderWindow* _renderWindow = NULL;

	int _height = 0;
	int _width = 0;
	sf::Vector2i _windowCenter;
	sf::Vector2i _previousMousePosition;
	sf::Vector2i _mousePositionDifference;
	glm::vec2 _movementRotation;
	glm::vec3 _movementDirection;
	Transform* _transform = NULL;
	float _fov = 0.0f;
	float _aspectRatio = 0.0f;
	float _nearPlane = 0.0f;
	float _farPlane = 0.0f;
	glm::mat4* _ortho = NULL;
	glm::mat4* _projection = NULL;
	glm::mat4* _viewProjection = NULL;

	void _keepMouseOnSpot();
	void _centralizeMouse();

	glm::vec3 _moveStep;
	glm::vec3 _rotateStep;
	glm::vec3 _xRotationAxis;
	glm::vec3 _yRotationAxis;
	glm::vec3 _zRotationAxis;

	bool _shift = false;
	bool _ctrl = false;
	bool _middle = false;
	bool _scroll = false;

	float _mouseWheelDelta;
	glm::vec3 _rotationPoint = glm::vec3(0.0f);


	Input::Key _middleButton = Input::Key::MiddleMouse;
	Input::Key _shiftKey = Input::Key::LShift;
	Input::Key _ctrlKey = Input::Key::LControl;
};
