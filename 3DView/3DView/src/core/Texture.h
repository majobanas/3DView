#pragma once

#include "core/Config.h"

#include <string>
#include <GL/glew.h>
#include "SFML/Graphics/Image.hpp"

class Texture
{
public:
	Texture();
	~Texture();

	int getWidth();
	int getHeight();

	static Texture* load(std::string pFileName);
	static Texture* loadCubemap(std::string pBaseName, std::string pExtension);

	static std::map<std::string, sf::Image*> Images;
	static void clearImages();
	static std::map<std::string, Texture*> Textures;
	static void clearTextures();
	static std::map<std::string, Texture*> Cubemaps;
	static void clearCubemaps();
	
	GLuint getTextureID();

private:
	int _width;
	int _height;

	GLuint _textureID;
};