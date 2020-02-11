#pragma once

#include "SFML/Graphics.hpp"
#include "GL/glew.h"

#include "core/Camera.h"
#include "core/Space.h"

class View {
public:

	View(int pWidth, int pHeight);
	~View();

	void run();

private:
	void _initializeRenderWindow();
	void _printContextInfo();
	void _initializeGLEW();

	void _initializeCamera();
	void _initializeSpace();

	int _height = 0;
	int _width = 0;
	sf::RenderWindow* _renderWindow = NULL;
	sf::Event _windowEvent;

	Camera* _camera = NULL;
	Space* _space = NULL;

	void _processInput();
	void _processUpdate();
	void _processRender();
};
