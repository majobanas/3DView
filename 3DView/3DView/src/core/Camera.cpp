#include "core/Camera.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
Camera::Camera(sf::RenderWindow* pRenderWindow, glm::vec3 pPosition, float pFov, float pNearPlane, float pFarPlane)
{
	_renderWindow = pRenderWindow;
	_movementRotation = glm::vec2(0.0f);
	_transform = new Transform(pPosition);

	_fov = pFov;
	_nearPlane = pNearPlane;
	_farPlane = pFarPlane;

	_ortho = new glm::mat4(1.0f);
	_projection = new glm::mat4(1.0f);
	_viewProjection = new glm::mat4(1.0f);

	updateResolution();
	_windowCenter = sf::Vector2i(_width / 2, _height / 2);
	_centralizeMouse();
	glDepthMask(GL_TRUE);
	glEnable(GL_DEPTH_TEST); //make sure we test the depthbuffer
	glDepthFunc(GL_LESS); // normal depth testing
	glFrontFace(GL_CCW); //tell opengl which vertex winding is considered to be front facing
	glEnable(GL_CULL_FACE); //tell opengl to enable face culling for the back faces
	glCullFace(GL_BACK);
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glLineWidth(2.0f);
}

Camera::~Camera()
{
	_renderWindow = NULL;
	delete _transform;
	_transform = NULL;
	delete _ortho;
	_ortho = NULL;
	delete _projection;
	_projection = NULL;
	delete _viewProjection;
	_viewProjection = NULL;
}

void Camera::update(float pElapsedTime)
{
	_moveStep = glm::vec3(0.0f);
	_rotateStep = glm::vec3(0.0f);


	if (Input::getMousePressed(_middleButton)) {
		_middle = true;
		_mousePositionDifference = sf::Mouse::getPosition(*_renderWindow) - _previousMousePosition;
		_keepMouseOnSpot();
		_mousePositionDifference.x = glm::clamp(_mousePositionDifference.x, -1, 1);
		_mousePositionDifference.y = glm::clamp(_mousePositionDifference.y, -1, 1);
		// SHIFT + MID = ROTACIA okolo zvoleneho objektu a okolo kamerovej osi Y
		float rotSpeed = 100.0f;
		if (Input::getKeyPressed(_shiftKey)) {
			_shift = true;
			_rotateStep.x -= _mousePositionDifference.y * rotSpeed;
			_rotateStep.y -= _mousePositionDifference.x * rotSpeed;
			//std::cout << "SHIFT" << std::endl;
		}
		// CTRL + MID = ROTACIA okolo zvoleneho objektu a okolo zakladnej osi Y
		else if (Input::getKeyPressed(_ctrlKey)) {
			_ctrl = true;
			_rotateStep.x -= _mousePositionDifference.y * rotSpeed;
			_rotateStep.y -= _mousePositionDifference.x * rotSpeed;
			//std::cout << "CTRL" << std::endl;

		}
		// MID = PAN pozdlz X a Y osi kamery
		else {
			float panSpeed = 20.0f;
			_moveStep.x -= _mousePositionDifference.x * panSpeed;
			_moveStep.y += _mousePositionDifference.y * panSpeed;
			//std::cout << "PAN" << std::endl;
		}
	}
	// SCROLL = ZOOM
	if (_mouseWheelDelta != 0.0f) {
		float scrollSpeed = 100.0f;
		_scroll = true;
		_moveStep.z = -_mouseWheelDelta * scrollSpeed;
	}

	if (_middle || _scroll) {
		if (_shift || _ctrl) {
			if (_rotationPoint != Config::rotationPoint) {
				_rotationPoint = Config::lerp(Config::rotationPoint, _rotationPoint, 1.0f - pElapsedTime * 4.0f);
			}
			glm::mat4 axis = *_transform->getTransform();
			if (_shift) {
				_xRotationAxis = glm::vec3(axis[0]);
				_yRotationAxis = glm::vec3(axis[1]);
				_zRotationAxis = glm::vec3(axis[2]);
			}
			if (_ctrl) {
				_xRotationAxis = glm::vec3(axis[0]);
				_yRotationAxis = glm::vec3(0.0f, 1.0f, 0.0f);
				_zRotationAxis = glm::vec3(axis[2]);
			}

			*_transform->getTransform() =
				glm::translate(_rotationPoint) *
				glm::rotate(glm::radians(0.0f), _zRotationAxis) *
				glm::rotate(glm::radians(_rotateStep.y * pElapsedTime), _yRotationAxis) *
				glm::rotate(glm::radians(_rotateStep.x * pElapsedTime), _xRotationAxis) *
				glm::translate(-_rotationPoint) *
				glm::translate(_transform->getPosition() + (_moveStep * pElapsedTime));

			glm::vec3 target = _rotationPoint;
			glm::mat4 mat = *_transform->getTransform();
			mat[2] = glm::vec4(-glm::normalize(target - _transform->getPosition()), 0.0f);
			mat[0] = glm::vec4(glm::normalize(glm::cross(glm::vec3(0.0f, 1.0f, 0.0f), glm::vec3(mat[2]))), 0.0f);
			mat[1] = glm::vec4(glm::cross(glm::vec3(mat[2]), glm::vec3(mat[0])), 0.0f);
			*_transform->getTransform() = mat;
		} else if (_middle || _scroll) {
			glm::vec3 pos = _transform->getPosition();
			*_transform->getTransform() =
				glm::translate(*_transform->getTransform(), (_moveStep * pElapsedTime));
			glm::vec3 change = _transform->getPosition() - pos;
			_rotationPoint += change;
			Config::rotationPoint = _rotationPoint;
		}

		*_viewProjection = *_projection * _transform->getInverse();

		_mousePositionDifference = sf::Vector2i(0, 0);
		_mouseWheelDelta = 0.0f;
		_shift = false;
		_ctrl = false;
		_middle = false;
		_scroll = false;
	}
}

Transform* Camera::getTransform()
{
	return _transform;
}

glm::mat4* Camera::getOrtho()
{
	return _ortho;
}

glm::mat4* Camera::getProjection()
{
	return _projection;
}

glm::mat4* Camera::getViewProjection()
{
	return _viewProjection;
}

void Camera::updateResolution()
{
	_width = _renderWindow->getSize().x;
	_height = _renderWindow->getSize().y;
	glViewport(0, 0, _width, _height);
	_aspectRatio = (float)_width/(float)_height;
	*_ortho = glm::ortho(0.0f, (float)_width, (float)_height, 0.0f, -1.0f, 1.0f);
	*_projection = glm::mat4(glm::perspective(glm::radians(_fov), _aspectRatio, _nearPlane, _farPlane));
	*_viewProjection = glm::mat4(*_projection * _transform->getInverse());
}

float Camera::getWidth()
{
	return _width;
}

float Camera::getHeight()
{
	return _height;
}

void Camera::setMouseWheelDelta(float pDelta)
{
	_mouseWheelDelta = pDelta;
}
// -------------------------------------------- PRIVATE --------------------------------------------
void Camera::_keepMouseOnSpot()
{
	sf::Mouse::setPosition(_previousMousePosition, *_renderWindow);
}

void Camera::_centralizeMouse()
{
	sf::Mouse::setPosition(_windowCenter, *_renderWindow);
	_previousMousePosition = _windowCenter;
}
