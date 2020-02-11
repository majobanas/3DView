#pragma once

#include "cfg.h"

#include "core/ShaderProgram.h"
#include "core/Mesh.h"
#include <glm/gtc/type_ptr.hpp>

/**
 * Material should be subclassed for all materials, for example ColorMaterial, TextureMaterial.
 *
 * Basically a material is responsible for setting up the correct shader,
 * filling all the correct parameters and actually rendering a mesh by streaming all mesh data through a
 * specific shader. Subclasses often use a static shader variable which is initialized once upon first use,
 * but that is fully optional (!).
 *
 * This works like the Unity Material, where you have to select a shader for a material and the shader chosen
 * determines which properties actually become available for the material itself.
 */
class Material
{
public:
	Material(glm::vec3 pColor);
	~Material();

	/**
	 * Render the given mesh in the given world using the given mvp matrices. Implement in subclass.
	 */
	void render(Mesh* pMesh, glm::mat4* pTransform, glm::mat4* pViewProjection);

private:
	//all the static properties are shared between instances of ColorMaterial
	//note that they are all PRIVATE, we do not expose this static info to the outside world
	static ShaderProgram* _shader;
	static void _lazyInitializeShader();

	//this one is unique per instance of a material
	glm::vec3 _color;

	static GLint _uVPMatrix;
	static GLint _uMMatrix;
	static GLint _uDiffuseColor;

	static GLint _aVertex;
	static GLint _aNormal;
	static GLint _aUV;
};