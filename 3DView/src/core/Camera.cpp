#include "core/Camera.h"

#include <iostream>

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
