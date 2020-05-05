#pragma once

#include <iostream>
#include "GL/glew.h"
#include <map>


#include "glm/glm.hpp"
#include <glm/gtc/type_ptr.hpp>
#include "glm/gtx/transform.hpp"

#include "core/Config.h"
#include "core/Camera.h"
#include "core/ShaderProgram.h"


#define generic ABC
#include "ft2build.h"
#include FT_FREETYPE_H
#undef generic

class TextMaterial {
public:
	TextMaterial(std::string pText);
	~TextMaterial();

	void render(glm::mat4* pModel, Camera* pCamera);

private:
	FT_Library _freetypeLibrary;
	FT_Face _freetypeFace;

	ShaderProgram* _shader = NULL;
	void _lazyInitializeShader(std::string pShader);

	GLuint _vertexArrayID;
	GLuint _vertexBufferID;

	GLuint _uXOffset;
	GLuint _uXStep;
	GLuint _uPMatrix;
	GLuint _uVMatrix;
	GLuint _uCMatrix;
	GLuint _uMMatrix;
	GLuint _uTextColor;
	GLuint _aVertex;
	
	GLfloat _scale;
	std::string _text;
	glm::vec3 _color;
	float _xOffset;

	struct Character {
		GLuint TextureID;
		glm::ivec2 Size;
		glm::ivec2 Bearing;
		GLuint Advance;
		GLfloat vertices[6][4];

		void UpdateVBO(GLfloat pScale) {
			GLfloat x = Bearing.x * pScale;
			GLfloat y = -(Size.y - Bearing.y) * pScale;
			//y += 1.0f; // half size of the object

			GLfloat width = Size.x * pScale;

			GLfloat height = Size.y * pScale;
			// Update VBO for each character
			vertices[0][0] = x;
			vertices[0][1] = y + height;
			vertices[0][2] = 0.0f;
			vertices[0][3] = 0.0f;

			vertices[1][0] = x;
			vertices[1][1] = y;
			vertices[1][2] = 0.0f;
			vertices[1][3] = 1.0f;

			vertices[2][0] = x + width;
			vertices[2][1] = y;
			vertices[2][2] = 1.0f;
			vertices[2][3] = 1.0f;

			vertices[3][0] = x;
			vertices[3][1] = y + height;
			vertices[3][2] = 0.0f;
			vertices[3][3] = 0.0f;

			vertices[4][0] = x + width;
			vertices[4][1] = y;
			vertices[4][2] = 1.0f;
			vertices[4][3] = 1.0f;

			vertices[5][0] = x + width;
			vertices[5][1] = y + height;
			vertices[5][2] = 1.0f;
			vertices[5][3] = 0.0f;
		}
	};
	std::map <GLchar, Character> _characters;


	void _initializeLibrary();
	void _loadFont(std::string pPath);
	void _loadCharacters();
};