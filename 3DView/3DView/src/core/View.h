#pragma once

#include <iostream>

#include "SFML/Graphics.hpp"
#include "SFML/Window/WindowHandle.hpp"
#include "GL/glew.h"
#include "core/Config.h"

#include "core/Camera.h"
#include "core/Space.h"

class View {
public:

	View(HWND__* pHandle, float pFov, int pUpdatesPerSecond, int pRendersPerSecond);
	~View();

	Space* space = NULL;

	bool timeToInput();
	bool timeToUpdate();
	bool timeToRender();
	bool timeToSecond();

	void processInput();
	void processUpdate();
	void processRender();
	void processSecond();

	void toggleRender(int pType, bool pBool);
	void alignCameraWithRoot();
	void setDefaultView();

	std::string getObjectTypeIDVersion();

	//int getQuickAction(int pMouseX, int pMouseY);
	//void toggleQuickAction(bool pBool);

private:

	void _initializeRenderWindow(HWND__* pHandle);
	void _printContextInfo();
	void _initializeGLEW();

	void _initializeCamera(float pFov);
	void _initializeSpace();

	void _initializeFrameBuffer();

	sf::RenderWindow* _renderWindow = NULL;
	sf::Event _windowEvent;

	Camera* _camera = NULL;

	sf::Clock _inputClock;
	sf::Clock _updateClock;
	sf::Clock _renderClock;
	sf::Clock _secondClock;

	sf::Clock _renderTimeClock;

	double _timePerInput = 0.0;
	double _timePerUpdate = 0.0;
	double _timePerRender = 0.0;
	double _timeSinceLastInput = 0.0;
	double _timeSinceLastUpdate = 0.0;
	double _timeSinceLastRender = 0.0;
	double _timeSinceLastSecond = 0.0;

	int _updatesPerSecond = 0;
	int _rendersPerSecond = 0;
	int _inputsPerSecond = 0;

	double _renderTime = 0.0;

	GLuint _frameBufferId;
	GLuint _objectTypeBufferId;
	GLuint _renderBufferId;

}; 