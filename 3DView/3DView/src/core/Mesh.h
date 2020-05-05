#pragma once

#include "core/Config.h"

#include <map>
#include <vector>
#include <string>
#include <iostream>
#include <fstream>

#include <boost/algorithm/string.hpp> 

#include "GL/glew.h"
#include "glm/glm.hpp"

class Mesh {
public:
	Mesh();
	~Mesh();

	static std::map<std::string, Mesh*> Meshes;
	static void clearMeshes();

	static Mesh* load(std::string pFilename);

	void streamToOpenGL(GLint pVerticesAttrib, GLint pNormalsAttrib, GLint pUVsAttrib);

protected:

	//OpenGL id's for the different buffers created for this mesh
	GLuint _indexBufferId;
	GLuint _vertexBufferId;
	GLuint _normalBufferId;
	GLuint _uvBufferId;

	//the actual data
	std::vector<glm::vec3> _vertices;       //vec3 with 3d coords for all vertices
	std::vector<glm::vec3> _normals;        //vec3 with 3d normal data
	std::vector<glm::vec2> _uvs;            //vec2 for uv

	//references to the vertices/normals & uvs in previous vectors
	std::vector<unsigned> _indices;

	//buffer vertices, normals, and uv's
	void _buffer();

	class FaceIndexTriplet {
	public:
		unsigned v; //vertex
		unsigned uv;//uv
		unsigned n; //normal
		FaceIndexTriplet(unsigned pV, unsigned pUV, unsigned pN)
			: v(pV), uv(pUV), n(pN) {
		}
		//needed for use as key in map
		bool operator<(const FaceIndexTriplet other) const {
			return memcmp((void*)this, (void*)&other, sizeof(FaceIndexTriplet)) > 0;
		}
	};
private:
};