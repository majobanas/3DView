#include "core/Material.h"

ShaderProgram* Material::_shader = NULL;

GLint Material::_uMMatrix = 0;
GLint Material::_uVPMatrix = 0;
GLint Material::_uDiffuseColor = 0;

GLint Material::_aVertex = 0;
GLint Material::_aNormal = 0;
GLint Material::_aUV = 0;

Material::Material(glm::vec3 pColor) {
	std::cout << "----Creating Material----" << std::endl;
	_color = pColor;

	//every time we create an instance of colormaterial we check if the corresponding shader has already been loaded
	_lazyInitializeShader();
}

Material::~Material()
{
	std::cout << "Deleting Material" << std::endl;
	delete _shader;
	_shader = NULL;
}

void Material::_lazyInitializeShader() {
	//this shader contains everything the material can do (it can render something in 3d using a single color)
	if (!_shader) {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::VALUE["shader_path"] + "color.vs");
		_shader->addShader(GL_FRAGMENT_SHADER, Config::VALUE["shader_path"] + "color.fs");
		_shader->finalize();

		_uMMatrix = _shader->getUniformLocation("mMatrix");
		_uVPMatrix = _shader->getUniformLocation("vpMatrix");
		_uDiffuseColor = _shader->getUniformLocation("diffuseColor");

		_aVertex = _shader->getAttribLocation("vertex");
		_aNormal = _shader->getAttribLocation("normal");
		_aUV = _shader->getAttribLocation("uv");
	}
}

void Material::render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pViewProjection) {
	_shader->use();

	//set the material color
	glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(_color));

	glUniformMatrix4fv(_uMMatrix, 1, GL_FALSE, glm::value_ptr(*pTransform));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr(*pViewProjection));
	//now inform mesh of where to stream its data
	pMesh->streamToOpenGL(_aVertex, _aNormal, _aUV);
}
