#include "core/View.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
View::View(HWND__* pHandle, float pFov, int pUpdatesPerSecond, int pRendersPerSecond)
{
	_timePerUpdate = 1.0f / pUpdatesPerSecond;
	_timePerRender = 1.0f / pRendersPerSecond;
	_timePerInput = glm::min(_timePerUpdate, _timePerRender);
	_timeSinceLastUpdate = _timePerInput;
	_timeSinceLastRender = _timePerInput;
	_timeSinceLastInput = _timePerInput;

	_initializeRenderWindow(pHandle);
	//_printContextInfo();
	_initializeGLEW();

	_initializeCamera(pFov);
	_initializeSpace();

	_initializeFrameBuffer();
}

View::~View()
{
	delete _renderWindow;
	_renderWindow = NULL;
	delete _camera;
	_camera = NULL;
	delete space;
	space = NULL;

	glDeleteFramebuffers(1, &_frameBufferId);
	glDeleteRenderbuffers(1, &_renderBufferId);

}

bool View::timeToInput()
{
	_timeSinceLastInput += _inputClock.restart().asSeconds();
	if (_timeSinceLastInput >= _timePerInput) {
		_timeSinceLastInput -= _timePerInput;
		_inputsPerSecond += 1;
		return true;
	}
	else return false;
}

bool View::timeToUpdate()
{
	_timeSinceLastUpdate += _updateClock.restart().asSeconds();
	if (_timeSinceLastUpdate >= _timePerUpdate) {
		_timeSinceLastUpdate -= _timePerUpdate;
		_updatesPerSecond += 1;
		return true;
	}
	else return false;
}

bool View::timeToRender()
{
	_timeSinceLastRender += _renderClock.restart().asSeconds();
	if (_timeSinceLastRender >= _timePerRender) {
		_timeSinceLastRender = glm::mod(_timeSinceLastRender, _timePerRender);
		_rendersPerSecond += 1;
		return true;
	}
	return false;
}

bool View::timeToSecond()
{
	_timeSinceLastSecond += _secondClock.restart().asSeconds();
	if (_timeSinceLastSecond >= 1.0) {
		_timeSinceLastSecond = glm::mod(_timeSinceLastSecond, 1.0);
		Debug::log("C++ I: " + std::to_string(_inputsPerSecond) + " U: " + std::to_string(_updatesPerSecond) + " R: " + std::to_string(_rendersPerSecond));
		Debug::log("C++ AvgTime R: " + std::to_string(_renderTime / _rendersPerSecond));
		return true;
	}
	return false;
}

// -------------------------------------------- PRIVATE --------------------------------------------
void View::_initializeRenderWindow(HWND__* pHandle)
{
	sf::Context context;
	_printContextInfo();
	_renderWindow = new sf::RenderWindow(pHandle, sf::ContextSettings(24, 8, 0, Config::i["gl_major"], Config::i["gl_minor"]));
}

void View::_printContextInfo()
{
	const GLubyte* vendor = glGetString(GL_VENDOR); 
	Config::s["gl_vendor"] = reinterpret_cast<char const*>(vendor);
	const GLubyte* renderer = glGetString(GL_RENDERER);
	Config::s["gl_renderer"] = reinterpret_cast<char const*>(renderer);
	const GLubyte* version = glGetString(GL_VERSION);
	Config::s["gl_version"] = reinterpret_cast<char const*>(version);
	const GLubyte* glslVersion = glGetString(GL_SHADING_LANGUAGE_VERSION);
	std::string glslV(reinterpret_cast<char const*>(glslVersion));
	glslV = glslV.erase(1, 1).substr(0, 3); 
	//Debug::now("Parsed GLSL Version: " + glslV);

	Config::s["glsl_version"] = glslV;
	//nice consistency here in the way OpenGl retrieves values
	GLint major, minor;
	glGetIntegerv(GL_MAJOR_VERSION, &major);
	Config::s["gl_major"] = std::to_string(major);
	Config::i["gl_major"] = major;
	glGetIntegerv(GL_MINOR_VERSION, &minor);
	Config::s["gl_minor"] = std::to_string(minor) + "0";
	Config::i["gl_minor"] = minor;

	/*printf("GL Vendor : %s\n", vendor);
	printf("GL Renderer : %s\n", renderer);
	printf("GL Version (string) : %s\n", version);
	printf("GL Version (integer) : %d.%d\n", major, minor);
	printf("GLSL Version : %s\n", glslVersion);
	*/
}

void View::_initializeGLEW()
{
	GLint glewStatus = glewInit();
	if (glewStatus != GLEW_OK) {
		Debug::log("GLEW status = FAILED");
	}
}

void View::_initializeCamera(float pFov)
{
	_camera = new Camera(_renderWindow, glm::vec3(3 * Config::f["spacing_z"], Config::f["spacing_y"], 0), pFov, 0.001f, 1000.0f);
}

void View::_initializeSpace()
{
	space = new Space();
}

void View::_initializeFrameBuffer()
{
	glGenFramebuffers(1, &_frameBufferId);
	glBindFramebuffer(GL_FRAMEBUFFER, _frameBufferId);

	glGenTextures(1, &_objectTypeBufferId);
	glBindTexture(GL_TEXTURE_2D, _objectTypeBufferId);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, _camera->getWidth(), _camera->getHeight(), 0, GL_RGB, GL_UNSIGNED_BYTE, NULL);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glBindTexture(GL_TEXTURE_2D, 0); // Unbind

	glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, _objectTypeBufferId, 0);

	glGenRenderbuffers(1, &_renderBufferId);
	glBindRenderbuffer(GL_RENDERBUFFER, _renderBufferId);
	glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, _camera->getWidth(), _camera->getHeight());
	glBindRenderbuffer(GL_RENDERBUFFER, 0); // Unbind

	glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, _renderBufferId);

	if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE) {
		Debug::log("ERROR::FRAMEBUFFER:: Framebuffer is not complete!");
	}
	glBindFramebuffer(GL_FRAMEBUFFER, 0);
}

void View::alignCameraWithRoot()
{
	_camera->alignCameraWithRoot = true;
}

void View::setDefaultView()
{
	_camera->setDefaultView();
}

void View::processInput()
{
	_camera->setShiftPressed(Input::getKeyPressed(Input::Key::LShift));
	_camera->setCtrlPressed(Input::getKeyPressed(Input::Key::LControl));

	while (_renderWindow->pollEvent(_windowEvent)) {
		switch (_windowEvent.type) {
			// MouseWheel Scroll
		case sf::Event::MouseWheelScrolled:
			_camera->setMouseWheelDelta(_windowEvent.mouseWheelScroll.delta);
			break;
			// Mouse Move
		case sf::Event::MouseMoved:
			_camera->setMousePosition(_windowEvent.mouseMove.x, _windowEvent.mouseMove.y);
			space->selectionMaterial->onMouseMove(Input::getMousePosition(_renderWindow));
			break;
			// MouseButton Press
		case sf::Event::MouseButtonPressed:
			if (_windowEvent.mouseButton.button == sf::Mouse::Button::Middle) {
				_camera->setMiddlePressed(true);
				_renderWindow->setMouseCursorVisible(false);
			}
			if (_windowEvent.mouseButton.button == sf::Mouse::Button::Left) {
				space->selectionMaterial->onMouseDown(Input::getMousePosition(_renderWindow));
			}
			break;
		case sf::Event::MouseButtonReleased:
			if (_windowEvent.mouseButton.button == sf::Mouse::Button::Middle) {
				_camera->setMiddlePressed(false);
				_renderWindow->setMouseCursorVisible(false);
			}
			if (_windowEvent.mouseButton.button == sf::Mouse::Button::Left) {
				space->selectionMaterial->onMouseRelease();
			}
			break;
			// Window Closed
		case sf::Event::Closed:
			_renderWindow->close();
			break;
			// Window Resized
		case sf::Event::Resized:
			_camera->updateResolution();
			glBindFramebuffer(GL_FRAMEBUFFER, _frameBufferId);
			glViewport(0, 0, _renderWindow->getSize().x, _renderWindow->getSize().y);
			glBindFramebuffer(GL_FRAMEBUFFER, 0);
			space->onResize(_renderWindow->getSize().x, _renderWindow->getSize().y);
			break;
			/*// Window Lost Focus
		case sf::Event::LostFocus:
			break;
			// Window Gained Focus
		case sf::Event::GainedFocus:
			break;*/
		}
	}
}

void View::processUpdate()
{
	_camera->update(_timePerUpdate);
	space->update(_timePerUpdate);
}

void View::processRender()
{
	_renderTimeClock.restart();
	space->render(_camera);
	_renderTime += _renderTimeClock.getElapsedTime().asSeconds();
	_renderWindow->display();
}

void View::processSecond()
{
	_inputsPerSecond = 0;
	_updatesPerSecond = 0;
	_rendersPerSecond = 0;

	_renderTime = 0.0;
}

void View::toggleRender(int pType, bool pBool)
{
	space->toggleRender(pType, pBool);
}

std::string View::getObjectTypeIDVersion()
{
	sf::Vector2i position = sf::Mouse::getPosition(*_renderWindow);

	return space->pickObject(_camera->getTransform()->getTransform(), _camera->getProjection(), position.x, _camera->getHeight() - position.y, _frameBufferId);
}
