#pragma once

#include "core/Config.h"

#include <GL/glew.h>
#include <map>
#include <vector>
#include <string>

class ShaderProgram
{
public:
	ShaderProgram();
	virtual ~ShaderProgram();

	static std::map<std::string, ShaderProgram*> Shaders;
	// Delete all unique shaders
	static void clearShaders();

	// Add shader (GL_VERTEX_SHADER / GL_FRAGMENT_SHADER / ...)
	void addShader(GLuint pShaderType, std::string pShader);
	// Link and compile added shaders
	void finalize();
	// Tell OpenGL to use this shader
	void use();

	// Get uniform variable address
	GLuint getUniformLocation(const std::string& pName);
	// Get attribute variable address
	GLuint getAttribLocation(const std::string& pName);

private:
	GLint _currentProgramId = 0;
	GLint _programId;

	std::vector<GLuint> _shaderIds;

	std::string _readFile(const std::string& pShaderPath);
	// Compile shader
	GLuint _compileShader(GLuint pShaderType, const std::string& pShaderSource);
};
