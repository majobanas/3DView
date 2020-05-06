#include "core/ShaderProgram.h"

#include <iostream>
#include <fstream>

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
std::map<std::string, ShaderProgram*> ShaderProgram::Shaders = std::map<std::string, ShaderProgram*>();

ShaderProgram::ShaderProgram() {
	_programId = 0;
	_shaderIds = std::vector<GLuint>();

	_programId = glCreateProgram();
}

ShaderProgram::~ShaderProgram() {
}

void ShaderProgram::clearShaders()
{
	for (auto& pair : Shaders) {
		delete pair.second;
		pair.second = NULL;
	}
	Shaders.clear();
}

void ShaderProgram::addShader(GLuint pShaderType, std::string pShader) {
	std::string shaderCode = 
		"#version " + Config::s["glsl_version"]/*Config::s["gl_major"] + Config::s["gl_minor"]*/ + '\n'
		+ _readFile(Config::applicationPath + Config::s["shader_path"] + pShader);

	GLuint shaderId = _compileShader(pShaderType, shaderCode);

	if (shaderId != 0) {
		_shaderIds.push_back(shaderId);
	}
}

std::string ShaderProgram::_readFile(const std::string& pShaderPath)
{
	std::string contents;
	std::ifstream file(pShaderPath, std::ios::in);
	if (file.is_open()) {
		std::string line = "";
		while (getline(file, line)) contents += "\n" + line;
		file.close();
	}
	else {
		Debug::error("Error reading shader " + pShaderPath);
		contents = "";
	}
	return contents;
}

GLuint ShaderProgram::_compileShader(GLuint pShaderType, const std::string& pShaderSource)
{
	const char* sourcePointer = pShaderSource.c_str();
	GLuint shaderId = glCreateShader(pShaderType);
	glShaderSource(shaderId, 1, &sourcePointer, NULL);
	glCompileShader(shaderId);

	GLint compilerResult = GL_FALSE;
	glGetShaderiv(shaderId, GL_COMPILE_STATUS, &compilerResult);

	if (compilerResult) {
		//std::cout << "Shader compiled ok." << std::endl;
		return shaderId;
	}
	else {
		Debug::error("Shader error:");
		int infoLogLength;
		glGetShaderiv(shaderId, GL_INFO_LOG_LENGTH, &infoLogLength);
		char* errorMessage = new char[infoLogLength + 1];
		glGetShaderInfoLog(shaderId, infoLogLength, NULL, errorMessage);
		std::cout << errorMessage << std::endl;
		delete[] errorMessage;
		return 0;
	}
}

void ShaderProgram::finalize() {
	for (size_t i = 0; i < _shaderIds.size(); ++i) {
		glAttachShader(_programId, _shaderIds[i]);
	}

	glLinkProgram(_programId);

	// Check the program
	GLint linkResult = GL_FALSE;
	glGetProgramiv(_programId, GL_LINK_STATUS, &linkResult);

	if (linkResult) {
		//std::cout << "Program linked ok." << std::endl;
	}
	else {
		Debug::error("Program error:");

		int infoLogLength;
		glGetProgramiv(_programId, GL_INFO_LOG_LENGTH, &infoLogLength);
		char* errorMessage = new char[infoLogLength + 1];
		glGetProgramInfoLog(_programId, infoLogLength, NULL, errorMessage);
		std::cout << errorMessage << std::endl;
		delete[] errorMessage;
	}

	for (size_t i = 0; i < _shaderIds.size(); ++i) {
		glDeleteShader(_shaderIds[i]);
	}
}

GLuint ShaderProgram::getUniformLocation(const std::string& pName) {
	return glGetUniformLocation(_programId, pName.c_str());
}

GLuint ShaderProgram::getAttribLocation(const std::string& pName) {
	return glGetAttribLocation(_programId, pName.c_str());
}
// -------------------------------------------- PRIVATE --------------------------------------------
void ShaderProgram::use()
{
	glGetIntegerv(GL_CURRENT_PROGRAM, &_currentProgramId);
	if (_currentProgramId != _programId) {
		glUseProgram(_programId);
	}

}