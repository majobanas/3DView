#include "core/material/TextMaterial.h"

// -------------------------------------------- PUBLIC --------------------------------------------
TextMaterial::TextMaterial(std::string pText)
{
	_text = pText;
	_color = Config::getColor(Config::s["text_color"]);
	_scale = Config::f["font_scale"];

	_initializeLibrary();
	//std::cout << Config::ApplicationPath + Config::S["font_path"] + "arial.ttf" << std::endl;
	_loadFont("arial.ttf");
	_characters = std::map<GLchar, Character>();
	_loadCharacters();
	FT_Done_Face(_freetypeFace);
	FT_Done_FreeType(_freetypeLibrary);

	glGenVertexArrays(1, &_vertexArrayID);
	glGenBuffers(1, &_vertexBufferID);
	glBindVertexArray(_vertexArrayID);
	glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(GLfloat) * 6 * 4, NULL, GL_DYNAMIC_DRAW);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 4, GL_FLOAT, GL_FALSE, 4 * sizeof(GLfloat), 0);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindVertexArray(0);

	_lazyInitializeShader(Config::s["text_shader_vertex"]);
}

TextMaterial::~TextMaterial()
{
	glDeleteBuffers(1, &_vertexBufferID);
	glDeleteVertexArrays(1, &_vertexArrayID);
}

void TextMaterial::render(glm::mat4* pModel, Camera* pCamera)
{
	_shader->use();

	glUniform3fv(_uTextColor, 1, glm::value_ptr(_color));

	glUniformMatrix4fv(_uPMatrix, 1, GL_FALSE, glm::value_ptr(*pCamera->getProjection()));
	glUniformMatrix4fv(_uVMatrix, 1, GL_FALSE, glm::value_ptr(pCamera->getTransform()->getInverse()));
	glUniformMatrix4fv(_uCMatrix, 1, GL_FALSE, glm::value_ptr(*pCamera->getTransform()->getTransform()));
	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pModel));
	glActiveTexture(GL_TEXTURE0);
	glBindVertexArray(_vertexArrayID);

	std::string::const_iterator c;
	/*float totalW = 0.0f;
	if (_characters.size() != 0) {
		for (c = _text.begin(); c != _text.end(); c++)
		{
			totalW += (_characters[*c].Bearing.x * pScale * 2.0f) + (_characters[*c].Size.x * pScale);
		}
		totalW = (totalW / 2.0f) + (_characters[0].Bearing.x * pScale);
	}*/
	GLfloat xStep = 0.0f;
	for (c = _text.begin(); c != _text.end(); c++)
	{
		Character ch = _characters[*c];

		/*GLfloat x = pX + ch.Bearing.x * pScale;
		x -= totalW;
		GLfloat y = pY - (ch.Size.y - ch.Bearing.y) * pScale;
		y += 1.0f; // half size of the object

		GLfloat width = ch.Size.x * pScale;

		GLfloat height = ch.Size.y * pScale;
		// Update VBO for each character
		GLfloat vertices[6][4] = {
			{ x,     y + height,   0.0, 0.0 },
			{ x,     y,       0.0, 1.0 },
			{ x + width, y,       1.0, 1.0 },

			{ x,     y + height,   0.0, 0.0 },
			{ x + width, y,       1.0, 1.0 },
			{ x + width, y + height,   1.0, 0.0 }
		};*/
		// Render glyph texture over quad
		//glUniform1ui(_uXOffset, _xOffset);
		glUniform1f(_uXStep, xStep);
		glBindTexture(GL_TEXTURE_2D, ch.TextureID);
		// Update content of VBO memory
		glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferID);
		glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(ch.vertices), ch.vertices);
		glBindBuffer(GL_ARRAY_BUFFER, 0);
		// Render quad
		glDrawArrays(GL_TRIANGLES, 0, 6);
		// Now advance cursors for next glyph (note that advance is number of 1/64 pixels)
		xStep += (ch.Advance >> 6) * _scale; // Bitshift by 6 to get value in pixels (2^6 = 64)
	}
	glBindVertexArray(0);
	glBindTexture(GL_TEXTURE_2D, 0);

}

void TextMaterial::_lazyInitializeShader(std::string pShader)
{
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];

	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["text_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["text_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}
	//_uXOffset = _shader->getUniformLocation("xOffset");
	_uXStep = _shader->getUniformLocation("xStep");
	_uPMatrix = _shader->getUniformLocation("pMatrix");
	_uVMatrix = _shader->getUniformLocation("vMatrix");
	_uCMatrix = _shader->getUniformLocation("cMatrix");
	_uMMatrix = _shader->getUniformLocation("mMatrix");
	_uTextColor = _shader->getUniformLocation("textColor");

	_aVertex = _shader->getAttribLocation("vertex");

}

// -------------------------------------------- PRIVATE --------------------------------------------

void TextMaterial::_initializeLibrary()
{
	if (FT_Init_FreeType(&_freetypeLibrary)) {
		Debug::error("C++ TextMaterial: FT_Library could not be initialized");
	}
}

void TextMaterial::_loadFont(std::string pPath)
{
	if (FT_New_Face(_freetypeLibrary, (Config::applicationPath + Config::s["font_path"] + pPath).c_str(), 0, &_freetypeFace))
		Debug::error("C++ TextMaterial: Failed to load font");
	FT_Set_Pixel_Sizes(_freetypeFace, 0, Config::i["font_size"]);
}

void TextMaterial::_loadCharacters()
{
	glPixelStorei(GL_UNPACK_ALIGNMENT, 1); // Disable byte-alignment restriction

	for (GLubyte c = 0; c < 128; c++)
	{
		// Load character glyph 
		if (FT_Load_Char(_freetypeFace, c, FT_LOAD_RENDER))
		{
			Debug::error("C++ TextMaterial: Failed to load Glyph");
			continue;
		}
		// Generate texture
		GLuint texture;
		glGenTextures(1, &texture);
		glBindTexture(GL_TEXTURE_2D, texture);
		glTexImage2D(
			GL_TEXTURE_2D,
			0,
			GL_RED,
			_freetypeFace->glyph->bitmap.width,
			_freetypeFace->glyph->bitmap.rows,
			0,
			GL_RED,
			GL_UNSIGNED_BYTE,
			_freetypeFace->glyph->bitmap.buffer
		);
		// Set texture options
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		// Now store character for later use
		Character character = {
			texture,
			glm::ivec2(_freetypeFace->glyph->bitmap.width, _freetypeFace->glyph->bitmap.rows),
			glm::ivec2(_freetypeFace->glyph->bitmap_left, _freetypeFace->glyph->bitmap_top),
			_freetypeFace->glyph->advance.x
		};
		character.UpdateVBO(_scale);
		_characters.insert(std::pair<GLchar, Character>(c, character));
		_xOffset += character.Bearing.x + character.Size.x;
	}
}
