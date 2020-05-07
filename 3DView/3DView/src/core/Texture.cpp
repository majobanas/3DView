#include "core/Texture.h"

class Config;
// -------------------------------------------- PUBLIC --------------------------------------------
std::map<std::string, sf::Image*> Texture::Images = std::map<std::string, sf::Image*>();
std::map<std::string, Texture*> Texture::Textures = std::map<std::string, Texture*>();
std::map<std::string, Texture*> Texture::Cubemaps = std::map<std::string, Texture*>();

Texture::Texture()
{
	glGenTextures(1, &_textureID);
}

Texture::~Texture()
{
	glDeleteTextures(1, &_textureID);
}

int Texture::getWidth()
{
	return _width;
}

int Texture::getHeight()
{
	return _height;
}

Texture* Texture::load(std::string pFileName)
{
	if (Textures.find(pFileName) != Textures.end()) {
		return Textures[pFileName];
	}
	else {
		Texture* texture = new Texture();

		sf::Image* image;
		if (Images.find(pFileName) != Images.end()) {
			image = Images[pFileName];
		}
		else {
			image = new sf::Image();
			if (!image->loadFromFile(Config::applicationPath + Config::s["texture_path"] + pFileName)) {
				Debug::error("Texture could not load from file.");
			}
			Images[pFileName] = image;
		}
		texture->_width = image->getSize().x;
		texture->_height = image->getSize().y;
		if (Config::i["load_models"] == 1) image->flipVertically();
		//image->flipHorizontally();
		glBindTexture(GL_TEXTURE_2D, texture->getTextureID());
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, image->getSize().x, image->getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image->getPixelsPtr());
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		// Removes leftover pixels when discarding fragments based on color
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
		glBindTexture(GL_TEXTURE_2D, 0);
		Textures[pFileName] = texture;
		return texture;
	}
}

Texture* Texture::loadCubemap(std::string pBaseName, std::string pExtension)
{
	if (Cubemaps.find(pBaseName + pExtension) != Cubemaps.end()) {
		return Cubemaps[pBaseName + pExtension];
	}
	else {
		Texture* texture = new Texture();

		sf::Image image = sf::Image();

		glBindTexture(GL_TEXTURE_CUBE_MAP, texture->getTextureID());

		const char* suffix[] = { "posx", "negx", "posy", "negy", "posz", "negz" };

		// Load the first one to get width/height
		std::string fullName = pBaseName + "_" + suffix[0] + pExtension;
		if (!image.loadFromFile(Config::applicationPath + Config::s["cubemap_path"] + fullName)) {
			Debug::error("Texture could not load cubemap from file.");
		}

		// Allocate immutable storage for the whole cube map texture
		glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X, 0, GL_RGBA8, image.getSize().x, image.getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image.getPixelsPtr());
		//glTexStorage2D(GL_TEXTURE_CUBE_MAP, 1, GL_RGBA8, image.getSize().x, image.getSize().y);
		//glTexSubImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X, 0, 0, 0, image.getSize().x, image.getSize().y, GL_RGBA, GL_UNSIGNED_BYTE, image.getPixelsPtr());

		// Load the other 5 cube-map faces
		for (int i = 1; i < 6; i++) {
			fullName = pBaseName + "_" + suffix[i] + pExtension;
			if (!image.loadFromFile(Config::applicationPath + Config::s["cubemap_path"] + fullName)) {
				Debug::error("Texture could not load cubemap part from file.");
			}

			//glTexSubImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X + i, 0, 0, 0, image.getSize().x, image.getSize().y, GL_RGBA, GL_UNSIGNED_BYTE, image.getPixelsPtr());
			glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X + i, 0, GL_RGBA8, image.getSize().x, image.getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image.getPixelsPtr());
		}

		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_R, GL_CLAMP_TO_EDGE);

		Cubemaps[pBaseName + pExtension] = texture;
		return texture;
	}
}

void Texture::clearImages()
{
	for (auto& pair : Images) {
		delete pair.second;
		pair.second = NULL;
	}
	Images.clear();
}

void Texture::clearTextures()
{
	for (auto& pair : Textures) {
		delete pair.second;
		pair.second = NULL;
	}
	Textures.clear();
}

void Texture::clearCubemaps() {
	for (auto& pair : Cubemaps) {
		delete pair.second;
		pair.second = NULL;
	}
	Cubemaps.clear();
}

GLuint Texture::getTextureID()
{
	return _textureID;
}
