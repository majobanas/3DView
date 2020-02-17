#pragma once

#include "SFML/Graphics.hpp"
#include "GL/glew.h"

#include "core/Camera.h"
#include "core/Space.h"

class View {
public:

	View(int pWidth, int pHeight, float pFov, int pUpdatesPerSecond, int pRendersPerSecond);
	~View();

	void run();

private:
	void _initializeRenderWindow();
	void _printContextInfo();
	void _initializeGLEW();

	void _initializeCamera(float pFov);
	void _initializeSpace();

	int _height = 0;
	int _width = 0;
	sf::RenderWindow* _renderWindow = NULL;
	sf::Event _windowEvent;

	Camera* _camera = NULL;
	Space* _space = NULL;

	sf::Clock _updateClock;
	sf::Clock _renderClock;
	sf::Clock _frameClock;

	double _timePerUpdate = 0.0;
	double _timePerRender = 0.0;
	double _timePerFrame = 0.0;
	double _timeSinceLastUpdate = 0.0;
	double _timeSinceLastRender = 0.0;
	double _timeSinceLastFrame = 0.0f;

	int _updatesPerSecond = 0;
	int _rendersPerSecond = 0;
	int _framesPerSecond = 0;

	void _processInput();
	void _processUpdate(float pElapsedTime);
	void _processRender();
};
