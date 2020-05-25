#include "core/material/LineMaterial.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
LineMaterial::LineMaterial(float pPositiveNegativeLength) {
	_positiveColor = glm::vec3(0.0f, 0.0f, 1.0f);
	_positiveVertices = std::vector<glm::vec3>{ glm::vec3(-50.0f, 0.0f, 0.0f), glm::vec3(-50.0f, 0.0f, -pPositiveNegativeLength) };
	_positiveIndices = std::vector<unsigned>();

	_negativeColor = glm::vec3(1.0f, 0.0f, 0.0f);
	_negativeVertices = std::vector<glm::vec3>{ glm::vec3(-50.0f, 0.0f, 0.0f), glm::vec3(-50.0f, 0.0f, pPositiveNegativeLength) };
	_negativeIndices = std::vector<unsigned>();

	_addedRemovedFrom = false;
	_from = std::map<std::string, Line>();
	_fromColor = Config::getColor(Config::s["from_color"]);
	_fromVertices = std::vector<glm::vec3>();
	_fromIndices = std::vector<unsigned>();

	_addedRemovedTo = false;
	_to = std::map<std::string, Line>();
	_toColor = Config::getColor(Config::s["to_color"]);
	_toVertices = std::vector<glm::vec3>();
	_toIndices = std::vector<unsigned>();

	_lazyInitializeShader(Config::s["line_shader_vertex"]);
	//_bufferPositive();
	_buffer(_positiveVertexBufferID, _positiveIndexBufferID, _positiveVertices, _positiveIndices, GL_STATIC_DRAW);
	//_bufferNegative();
	_buffer(_negativeVertexBufferID, _negativeIndexBufferID, _negativeVertices, _negativeIndices, GL_STATIC_DRAW);
}

LineMaterial::~LineMaterial()
{
	// Space.cpp deletes all ShaderPrograms (delete _shader;)
	_shader = NULL;

	glDeleteBuffers(1, &_positiveVertexBufferID);
	glDeleteBuffers(1, &_positiveIndexBufferID);

	glDeleteBuffers(1, &_negativeVertexBufferID);
	glDeleteBuffers(1, &_negativeIndexBufferID);

	glDeleteBuffers(1, &_fromVertexBufferID);
	glDeleteBuffers(1, &_fromIndexBufferID);

	glDeleteBuffers(1, &_toVertexBufferID);
	glDeleteBuffers(1, &_toIndexBufferID);
}

void LineMaterial::addFrom(MObject* pSelectedObject, MObject* pFrom)
{

	glm::vec3 offset = glm::vec3(0.0f, 0.1f, 0.0f);
	ObjVer selectedVersion = pSelectedObject->objVer;
	ObjVer fromVersion = pFrom->objVer;
	glm::vec3 selectedPosition = pSelectedObject->getTransform()->getPosition() - offset;
	glm::vec3 fromPosition = pFrom->getTransform()->getPosition() - offset;
	std::string lineTag = "_" + selectedVersion.toString() + Config::objVerSplitter + "_" + fromVersion.toString();

	pSelectedObject->lineTags.push_back(lineTag);
	pFrom->lineTags.push_back(lineTag);

	_from[lineTag] = Line(selectedPosition, fromPosition);



	_addedRemovedFrom = true;
}

void LineMaterial::removeFrom(MObject* pFrom)
{
	for (auto& lineTag : _from) {
		for (auto& fromLineTag : pFrom->lineTags) {
			if (lineTag.first == fromLineTag) {
				_from.erase(lineTag.first);
			}
		}
	}
	
	_addedRemovedFrom = true;
}

void LineMaterial::addTo(MObject* pSelectedObject, MObject* pTo)
{
	glm::vec3 offset = glm::vec3(0.0f, 0.1f, 0.0f);
	ObjVer selectedVersion = pSelectedObject->objVer;
	ObjVer toVersion = pTo->objVer;
	glm::vec3 selectedPosition = pSelectedObject->getTransform()->getPosition() + offset;
	glm::vec3 toPosition = pTo->getTransform()->getPosition() + offset;
	std::string lineTag = "_" + selectedVersion.toString() + Config::objVerSplitter + "_" + toVersion.toString();

	pSelectedObject->lineTags.push_back(lineTag);
	pTo->lineTags.push_back(lineTag);

	_to[lineTag] = Line(selectedPosition, toPosition);

	_addedRemovedTo = true;
}

void LineMaterial::removeTo(MObject* pTo)
{
	for (auto& lineTag : _to) {
		for (auto& toLineTag : pTo->lineTags) {
			if (lineTag.first == toLineTag) {
				_to.erase(lineTag.first);
			}
		}
	}
	
	_addedRemovedTo = true;
}
// -------------------------------------------- PRIVATE --------------------------------------------
void LineMaterial::_lazyInitializeShader(std::string pShader) {
	if (ShaderProgram::Shaders.find(pShader) != ShaderProgram::Shaders.end()) {
		_shader = ShaderProgram::Shaders[pShader];
	}
	else {
		_shader = new ShaderProgram();
		_shader->addShader(GL_VERTEX_SHADER, Config::s["line_shader_vertex"]);
		_shader->addShader(GL_FRAGMENT_SHADER, Config::s["line_shader_fragment"]);
		_shader->finalize();
		ShaderProgram::Shaders[pShader] = _shader;
	}

	_uVPMatrix = _shader->getUniformLocation("vpMatrix");
	_uDiffuseColor = _shader->getUniformLocation("diffuseColor");

	_aVertex = _shader->getAttribLocation("vertex");

}
void LineMaterial::render(glm::mat4* pViewProjection) {
	_shader->use();

	_render(_positiveColor, _positiveVertexBufferID, _positiveIndexBufferID, _positiveIndices, pViewProjection);

	_render(_negativeColor, _negativeVertexBufferID, _negativeIndexBufferID, _negativeIndices, pViewProjection);

	if (_addedRemovedFrom) {
		_fromVertices.clear();
		for (auto& lineTag : _from) {
			if (lineTag.second.render) {
				_fromVertices.push_back(lineTag.second.start);
				_fromVertices.push_back(lineTag.second.end);
			}
		}
		_buffer(_fromVertexBufferID, _fromIndexBufferID, _fromVertices, _fromIndices, GL_DYNAMIC_DRAW);
		_addedRemovedFrom = false;
	}
	if (_fromVertices.size() != 0) {
		_render(_fromColor, _fromVertexBufferID, _fromIndexBufferID, _fromIndices, pViewProjection);
	}

	if (_addedRemovedTo) {
		_toVertices.clear();
		for (auto& lineTag : _to) {
			if (lineTag.second.render) {
				_toVertices.push_back(lineTag.second.start);
				_toVertices.push_back(lineTag.second.end);
			}
		}
		_buffer(_toVertexBufferID, _toIndexBufferID, _toVertices, _toIndices, GL_DYNAMIC_DRAW);
		_addedRemovedTo = false;
	}
	if (_toVertices.size() != 0) {
		_render(_toColor, _toVertexBufferID, _toIndexBufferID, _toIndices, pViewProjection);
	}
}

void LineMaterial::toggleRender(int pType, bool pBool)
{
	std::string tag = "";
	std::string type = "_" + std::to_string(pType) + Config::itemSplitter;

	for (auto& pair : _from) {
		tag = pair.first;
		if (tag.find(type) != std::string::npos) {
			Config::lineStatus[pType] = pBool;
			pair.second.render = help(tag, pType);
		}
	}
	for (auto& pair : _to) {
		tag = pair.first;
		if (tag.find(type) != std::string::npos) {
			Config::lineStatus[pType] = pBool;
			pair.second.render = help(tag, pType);
		}
	}
	_addedRemovedFrom = true;
	_addedRemovedTo = true;
}

bool LineMaterial::help(std::string pTag, int pType)
{
	int type2;
	int id1;
	int id2;
	std::vector<std::string> split;
	std::vector<std::string> split2;
	boost::split(split, pTag, boost::is_any_of(Config::objVerSplitter));
	if (split.at(0).find("_" + std::to_string(pType)) == std::string::npos) {
		boost::split(split2, split.at(0), boost::is_any_of(Config::itemSplitter));
		type2 = std::stoi(split2.at(0).substr(1, split2.at(0).size()));
		id2 = std::stoi(split2.at(1));

		split2.clear();
		boost::split(split2, split.at(1), boost::is_any_of(Config::itemSplitter));
		id1 = std::stoi(split2.at(1));
	}
	else {
		boost::split(split2, split.at(1), boost::is_any_of(Config::itemSplitter));
		type2 = std::stoi(split2.at(0).substr(1, split2.at(0).size()));
		id2 = std::stoi(split2.at(1));

		split2.clear();
		boost::split(split2, split.at(0), boost::is_any_of(Config::itemSplitter));
		id1 = std::stoi(split2.at(1));
	}
	return (Config::lineStatus[pType] == true && Config::lineStatus[type2] == true) &&
		   (Config::lineIDStatus[pType][id1] == true && Config::lineIDStatus[type2][id2] == true);

}

void LineMaterial::toggleRender(int pType, int pID, bool pBool)
{

	std::string tag = "";
	std::string typeID = "_" + std::to_string(pType) + Config::itemSplitter + std::to_string(pID);

	for (auto& pair : _from) {
		tag = pair.first;
		if (tag.find(typeID) != std::string::npos) {
			Config::lineIDStatus[pType][pID] = pBool;
			pair.second.render = help(tag, pType, pID);
		}
	}
	for (auto& pair : _to) {
		tag = pair.first;
		if (tag.find(typeID) != std::string::npos) {
			Config::lineIDStatus[pType][pID] = pBool;
			pair.second.render = help(tag, pType, pID);
		}
	}
	_addedRemovedFrom = true;
	_addedRemovedTo = true;
}

bool LineMaterial::help(std::string pTag, int pType, int pID)
{
	int type2;
	int id2;
	std::vector<std::string> split;
	std::vector<std::string> split2;
	boost::split(split, pTag, boost::is_any_of(Config::objVerSplitter));
	if (split.at(0).find("_" + std::to_string(pType) + Config::itemSplitter + std::to_string(pID)) == std::string::npos) {
		boost::split(split2, split.at(0), boost::is_any_of(Config::itemSplitter));
		type2 = std::stoi(split2.at(0).substr(1, split2.at(0).size()));
		id2 = std::stoi(split2.at(1));
	}
	else {
		boost::split(split2, split.at(1), boost::is_any_of(Config::itemSplitter));
		type2 = std::stoi(split2.at(0).substr(1, split2.at(0).size()));
		id2 = std::stoi(split2.at(1));
	}
	return (Config::lineIDStatus[pType][pID] == true && Config::lineIDStatus[type2][id2] == true);

}

void LineMaterial::clearFromTo()
{
	_clearFrom();
	_clearTo();
}


void LineMaterial::_buffer(GLuint& pVertexBufferID, GLuint& pIndexBufferID, std::vector<glm::vec3>& pVertices, std::vector<unsigned int>& pIndices, int pGL_DRAW_TYPE)
{
	if (pVertexBufferID == -1)
		glGenBuffers(1, &pVertexBufferID);
	glBindBuffer(GL_ARRAY_BUFFER, pVertexBufferID);
	glBufferData(GL_ARRAY_BUFFER, pVertices.size() * sizeof(glm::vec3), &pVertices[0], pGL_DRAW_TYPE);

	pIndices.clear();
	for (int i = 0; i < pVertices.size(); i++)
		pIndices.push_back(i);

	if (pIndexBufferID == -1)
		glGenBuffers(1, &pIndexBufferID);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, pIndexBufferID);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, pIndices.size() * sizeof(unsigned int), &pIndices[0], pGL_DRAW_TYPE);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
}

void LineMaterial::_render(glm::vec3& pColor, GLuint& pVertexBufferID, GLuint& pIndexBufferID, std::vector<unsigned int>& pIndices, glm::mat4* pViewProjection)
{
	glUniform3fv(_uDiffuseColor, 1, glm::value_ptr(pColor));
	glUniformMatrix4fv(_uVPMatrix, 1, GL_FALSE, glm::value_ptr(*pViewProjection));

	_streamToOpenGL(pVertexBufferID, pIndexBufferID, pIndices.size());
}

void LineMaterial::_streamToOpenGL(GLint pVBID, GLint pIBID, int pIndicesSize)
{
	if (_aVertex != -1) {
		glBindBuffer(GL_ARRAY_BUFFER, pVBID);
		glEnableVertexAttribArray(_aVertex);
		glVertexAttribPointer(_aVertex, 3, GL_FLOAT, GL_FALSE, 0, 0);
	}
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, pIBID);
	glDrawElements(GL_LINES, pIndicesSize, GL_UNSIGNED_INT, (GLvoid*)0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	if (_aVertex != -1) glDisableVertexAttribArray(_aVertex);
}

void LineMaterial::_clearFrom()
{
	_fromVertices.clear();
	_fromIndices.clear();
	_from.clear();
}

void LineMaterial::_clearTo()
{
	_toVertices.clear();
	_toIndices.clear();
	_to.clear();
}

LineMaterial::Line::Line()
{
}

LineMaterial::Line::Line(glm::vec3 pStart, glm::vec3 pEnd)
{
	start = pStart;
	end = pEnd;

	render = true;
}

LineMaterial::Line::~Line()
{
}
