#include "core/Mesh.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
std::map<std::string, Mesh*> Mesh::Meshes = std::map<std::string, Mesh*>();

Mesh::Mesh()
{
	_indexBufferId = 0;
	_vertexBufferId = 0; 
	_normalBufferId = 0;
	_uvBufferId = 0;

	_vertices = std::vector<glm::vec3>();
	_normals = std::vector<glm::vec3>();
	_uvs = std::vector<glm::vec2>();
	_indices = std::vector<unsigned int>();
}

Mesh::~Mesh()
{
	_vertices.clear();
	_normals.clear();
	_uvs.clear();
	_indices.clear();

	glDeleteBuffers(1, &_vertexBufferId);
	glDeleteBuffers(1, &_normalBufferId);
	glDeleteBuffers(1, &_uvBufferId);
	glDeleteBuffers(1, &_indexBufferId);
}

void Mesh::clearMeshes()
{
	for (auto& pair : Meshes) {
		delete pair.second;
		pair.second = NULL;
	}
	Meshes.clear();
}

Mesh* Mesh::load(std::string pModel)
{
	Mesh* mesh = NULL;
	// If mesh already exists
	if (Meshes.find(pModel) != Meshes.end()) {
		mesh = Meshes[pModel];
		Debug::log("C++ Mesh.load: loaded from cache");
	} else {
		Debug::log("C++ Mesh.load: loaded new model");
		// Else load and create a new one
		mesh = new Mesh();
		// .obj data storage
		std::vector<glm::vec3> vertices;
		std::vector<glm::vec3> normals;
		std::vector<glm::vec2> uvs;

		std::map <FaceIndexTriplet, unsigned int> mappedTriplets;

		std::ifstream obj(Config::applicationPath + Config::s["model_path"] + pModel, std::ios::in);
		if (obj.is_open()) {
			std::string line;
			while (getline(obj, line))
			{
				// Cmd read from obj file (v, vt, vn, f)
				char cmd[10];
				cmd[0] = 0;

				// Get the first string in the line of max 10 chars (c-style)
				sscanf(line.c_str(), "%10s", cmd);

				// "v" lines first, then "vt" lines, then "vn" lines, then "f" lines last
				if (strcmp(cmd, "v") == 0) {
					glm::vec3 vertex;
					sscanf(line.c_str(), "%10s %f %f %f ", cmd, &vertex.x, &vertex.y, &vertex.z);
					vertices.push_back(vertex);
				}
				else if (strcmp(cmd, "vn") == 0) {
					glm::vec3 normal;
					sscanf(line.c_str(), "%10s %f %f %f ", cmd, &normal.x, &normal.y, &normal.z);
					normals.push_back(normal);
				}
				else if (strcmp(cmd, "vt") == 0) {
					glm::vec2 uv;
					sscanf(line.c_str(), "%10s %f %f ", cmd, &uv.x, &uv.y);
					uvs.push_back(uv);
				}
				else if (strcmp(cmd, "f") == 0) {
					// "f" lines looks like: f 2/1/1 1/2/1 3/3/1
					// or: f v1/u1/n1 v2/u2/n2 v3/u3/n3
					// we need to check whether we already encountered it
					// and update our administration based on that
					glm::ivec3 vertexIndex;
					glm::ivec3 normalIndex;
					glm::ivec3 uvIndex;
					int count = sscanf(line.c_str(), "%10s %d/%d/%d %d/%d/%d %d/%d/%d", cmd, &vertexIndex[0], &uvIndex[0], &normalIndex[0], &vertexIndex[1], &uvIndex[1], &normalIndex[1], &vertexIndex[2], &uvIndex[2], &normalIndex[2]);

					// Have we read exactly 10 elements?
					if (count == 10) {
						// Process 3 triplets
						for (int i = 0; i < 3; ++i) {
							// Create key out of the triplet and check if we already encountered this before
							FaceIndexTriplet triplet(vertexIndex[i], uvIndex[i], normalIndex[i]);
							std::map<FaceIndexTriplet, unsigned int>::iterator found = mappedTriplets.find(triplet);
							// If not found
							if (found == mappedTriplets.end())
							{
								// Create a new index value, and map our triplet to it
								unsigned int index = mappedTriplets.size();
								mappedTriplets[triplet] = index;
								mesh->_indices.push_back(index);
								// All values in the f triplets in the .obj file
								// are 1 based, but our vectors are 0 based
								mesh->_vertices.push_back(vertices[vertexIndex[i] - 1]);
								mesh->_normals.push_back(normals[normalIndex[i] - 1]);
								mesh->_uvs.push_back(uvs[uvIndex[i] - 1]);
							}
							else
							{
								// If the key was already present, get the index value for it
								unsigned int index = found->second;
								mesh->_indices.push_back(index);
							}
						}
					}
					else {
						// If we read a different amount, something is wrong
						Debug::error("C++ Mesh.load: Error reading obj, needing v,vn,vt");
						delete mesh;
						return NULL;
					}
				}
			}
		}
		mesh->_buffer();
		Meshes[pModel] = mesh;
	}
    return mesh;
}

// -------------------------------------------- PRIVATE --------------------------------------------
void Mesh::_buffer()
{
	glGenBuffers(1, &_vertexBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferId);
	glBufferData(GL_ARRAY_BUFFER, _vertices.size() * sizeof(glm::vec3), &_vertices[0], GL_STATIC_DRAW);

	glGenBuffers(1, &_normalBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, _normalBufferId);
	glBufferData(GL_ARRAY_BUFFER, _normals.size() * sizeof(glm::vec3), &_normals[0], GL_STATIC_DRAW);

	glGenBuffers(1, &_uvBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, _uvBufferId);
	glBufferData(GL_ARRAY_BUFFER, _uvs.size() * sizeof(glm::vec2), &_uvs[0], GL_STATIC_DRAW);

	glGenBuffers(1, &_indexBufferId);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _indexBufferId);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indices.size() * sizeof(unsigned int), &_indices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
}

void Mesh::streamToOpenGL(GLint pVerticesAttrib, GLint pNormalsAttrib, GLint pUVsAttrib) {
	if (pVerticesAttrib != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferId);
		glEnableVertexAttribArray(pVerticesAttrib);
		glVertexAttribPointer(pVerticesAttrib, 3, GL_FLOAT, GL_FALSE, 0, 0);
	}

	if (pNormalsAttrib != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, _normalBufferId);
		glEnableVertexAttribArray(pNormalsAttrib);
		glVertexAttribPointer(pNormalsAttrib, 3, GL_FLOAT, GL_TRUE, 0, 0);
	}

	if (pUVsAttrib != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, _uvBufferId);
		glEnableVertexAttribArray(pUVsAttrib);
		glVertexAttribPointer(pUVsAttrib, 2, GL_FLOAT, GL_FALSE, 0, 0);
	}

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _indexBufferId);

	glDrawElements(GL_TRIANGLES, _indices.size(), GL_UNSIGNED_INT, (GLvoid*)0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	if (pUVsAttrib != -1) glDisableVertexAttribArray(pUVsAttrib);
	if (pNormalsAttrib != -1) glDisableVertexAttribArray(pNormalsAttrib);
	if (pVerticesAttrib != -1) glDisableVertexAttribArray(pVerticesAttrib);
}