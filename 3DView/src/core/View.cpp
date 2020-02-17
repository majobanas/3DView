#include "core/View.h"

#include <iostream>

View::View(int pWidth, int pHeight, float pFov, int pUpdatesPerSecond, int pRendersPerSecond)
{
	std::cout << "----Creating View----" << std::endl;
	_width = pWidth; 
	_height = pHeight;
	_timePerUpdate = 1.0f / pUpdatesPerSecond;
	_timePerRender = 1.0f / pRendersPerSecond;
	_timePerFrame = glm::min(_timePerUpdate, _timePerRender);
	_timeSinceLastUpdate = _timePerFrame;
	_timeSinceLastRender = _timePerFrame;
	_timeSinceLastFrame = _timePerFrame;

	_initializeRenderWindow();
	_printContextInfo();
	_initializeGLEW();

	_initializeCamera(pFov);
	_initializeSpace();
}

View::~View()
{
	std::cout << "Deleting View" << std::endl;
	delete _renderWindow;
	_renderWindow = NULL;
	delete _camera;
	_camera = NULL;
	delete _space;
	_space = NULL;
}

void View::run()
{
	while (_renderWindow->isOpen()) {

		_timeSinceLastFrame += _frameClock.restart().asSeconds();
		if (_timeSinceLastFrame >= _timePerFrame) {
			_timeSinceLastFrame = glm::mod(_timeSinceLastFrame, _timePerFrame);
			_processInput();
			_framesPerSecond += 1;

			_timeSinceLastUpdate += _updateClock.restart().asSeconds();
			if (_timeSinceLastUpdate >= _timePerUpdate) {
				_timeSinceLastUpdate -= _timePerUpdate;
				_processUpdate(_timePerUpdate);
				_updatesPerSecond += 1;
			}

			_timeSinceLastRender += _renderClock.restart().asSeconds();
			if (_timeSinceLastRender >= _timePerRender) {
				_timeSinceLastRender -= _timePerRender;
				_processRender();
				_rendersPerSecond += 1;
			}
		}
	}
}

void View::_initializeRenderWindow()
{
	std::cout << "----Initializing render window----" << std::endl;
	_renderWindow = new sf::RenderWindow(sf::VideoMode(_width, _height), "3DView", sf::Style::Default);// , sf::ContextSettings(24, 8, 0, 3, 3));
}

void View::_printContextInfo()
{
	std::cout << "----Printing context info----" << std::endl;
	//std::cout << "----------------------------------" << std::endl << std::endl;
	//print some debug stats for whoever cares
	const GLubyte* vendor = glGetString(GL_VENDOR);
	const GLubyte* renderer = glGetString(GL_RENDERER);
	const GLubyte* version = glGetString(GL_VERSION);
	const GLubyte* glslVersion = glGetString(GL_SHADING_LANGUAGE_VERSION);
	//nice consistency here in the way OpenGl retrieves values
	GLint major, minor;
	glGetIntegerv(GL_MAJOR_VERSION, &major);
	Config::VALUE["gl_major"] = std::to_string(major);
	std::cout << Config::VALUE["gl_major"] << std::endl;
	glGetIntegerv(GL_MINOR_VERSION, &minor);
	Config::VALUE["gl_minor"] = std::to_string(minor) + "0";
	std::cout << Config::VALUE["gl_minor"] << std::endl;

	printf("GL Vendor : %s\n", vendor);
	printf("GL Renderer : %s\n", renderer);
	printf("GL Version (string) : %s\n", version);
	printf("GL Version (integer) : %d.%d\n", major, minor);
	printf("GLSL Version : %s\n", glslVersion);
	//std::cout << "----------------------------------" << std::endl << std::endl;
}

void View::_initializeGLEW()
{
	std::cout << "----Initializing GLEW----" << std::endl;
	//initialize the opengl extension wrangler
	GLint glewStatus = glewInit();
	std::cout << "Initialized GLEW, status (1 == OK, 0 == FAILED):" << (glewStatus == GLEW_OK) << std::endl;
}

void View::_initializeCamera(float pFov)
{
	_camera = new Camera(glm::vec3(0, 0, 10), pFov, _width / _height, 0.001f, 1000.0f);
}

void View::_initializeSpace()
{
	_space = new Space();
}

void View::_processInput()
{
	while (_renderWindow->pollEvent(_windowEvent)) {
		switch (_windowEvent.type) {
		case sf::Event::Closed:
			_renderWindow->close();
			std::cout << "Render window closed" << std::endl;
			break;
		case sf::Event::Resized:
			glViewport(0, 0, _windowEvent.size.width, _windowEvent.size.height);
			break;
		}
	}
}

void View::_processUpdate(float pElapsedTime)
{
	_camera->update(pElapsedTime);
	_space->update(pElapsedTime);
}

void View::_processRender()
{
	_renderWindow->clear();
	_space->render(_camera->getViewProjection());
	glFinish();
	_renderWindow->display();
}
