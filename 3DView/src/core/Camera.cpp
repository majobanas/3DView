#include "core/Camera.h"

#include <iostream>
#include "core/Input.h"

Camera::Camera(glm::vec3 pPosition, float pFov, float pAspectRatio, float pNearPlane, float pFarPlane)
{
	std::cout << "----Creating Camera----" << std::endl;
	_transform = new glm::mat4(glm::translate(pPosition));
	_fov = pFov;
	_aspectRatio = pAspectRatio;
	_nearPlane = pNearPlane;
	_farPlane = pFarPlane;
	_projection = new glm::mat4(glm::perspective(glm::radians(_fov), _aspectRatio, _nearPlane, _farPlane));
	_viewProjection = new glm::mat4(*_projection * glm::inverse(*_transform));
	
	glEnable(GL_DEPTH_TEST); //make sure we test the depthbuffer
	glFrontFace(GL_CCW); //tell opengl which vertex winding is considered to be front facing
	glEnable(GL_CULL_FACE); //tell opengl to enable face culling for the back faces
	glCullFace(GL_BACK);
	//set the default blend mode aka dark magic:
	//https://www.opengl.org/sdk/docs/man/html/glBlendFunc.xhtml
	//https://www.opengl.org/wiki/Blending
	//http://www.informit.com/articles/article.aspx?p=1616796&seqNum=5
	//http://www.andersriggelsen.dk/glblendfunc.php
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glClearColor((float)0x2d / 0xff, (float)0x6b / 0xff, (float)0xce / 0xff, 1.0f);
}

Camera::~Camera()
{
	std::cout << "Deleting Camera" << std::endl;
	delete _transform;
	_transform = NULL;
	delete _projection;
	_projection = NULL;
	delete _viewProjection;
	_viewProjection = NULL;
}

void Camera::update(float pElapsedTime)
{
	if (Input::getKeyPressed(Input::Key::W) || 
		Input::getKeyPressed(Input::Key::S) || 
		Input::getKeyPressed(Input::Key::A) || 
		Input::getKeyPressed(Input::Key::D) ||
		Input::getKeyPressed(Input::Key::Q) ||
		Input::getKeyPressed(Input::Key::E) ||
		Input::getKeyPressed(Input::Key::Up) ||
		Input::getKeyPressed(Input::Key::Down) ||
		Input::getKeyPressed(Input::Key::Left) ||
		Input::getKeyPressed(Input::Key::Right)) {
		glm::vec3 translation = glm::vec3((*_transform)[3]);
		glm::vec3 direction = glm::normalize(glm::vec3(Input::getKeyPressed(Input::Key::A) ? -1 : (Input::getKeyPressed(Input::Key::D) ? 1 : 0),
													   Input::getKeyPressed(Input::Key::Q) ? -1 : (Input::getKeyPressed(Input::Key::E) ? 1 : 0),
													   Input::getKeyPressed(Input::Key::W) ? -1 : (Input::getKeyPressed(Input::Key::S) ? 1 : 0)));
		
		/*float step = 1.0f * pElapsedTime;
		if (Input::getKeyPressed(Input::Key::W)) {
			translation.z -= step;
		}
		if (Input::getKeyPressed(Input::Key::S)) {
			translation.z += step;
		}
		if (Input::getKeyPressed(Input::Key::A)) {
			translation.x -= step;
		}
		if (Input::getKeyPressed(Input::Key::D)) {
			translation.x += step;
		}*/

		*_transform = glm::rotate(glm::radians(0.0f), glm::vec3(0.0f, 1.0f, 0.0f)) * 
					  glm::rotate(glm::radians(0.0f), glm::vec3(1.0f, 0.0f, 0.0f)) *
					  glm::rotate(glm::radians(0.0f), glm::vec3(0.0f, 0.0f, 1.0f)) *
					  glm::translate(translation + (direction * pElapsedTime * 5.0f));

		*_viewProjection = *_projection * glm::inverse(*_transform);
	}
}

glm::mat4* Camera::getTransform()
{
	return _transform;
}

glm::mat4* Camera::getProjection()
{
	return _projection;
}

glm::mat4* Camera::getViewProjection()
{
	return _viewProjection;
}
