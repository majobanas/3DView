#include "core/Space.h"

#include <iostream>

class Config;


std::map<int, std::map<int, int>> Space::_occupied = std::map<int, std::map<int, int>>();
// -------------------------------------------- PUBLIC --------------------------------------------
Space::Space()
{
	_loadSkyboxTextures();
	//_skyboxTextures = Texture::loadCubemap(Config::s["skybox_basename"], Config::s["skybox_extension"]);
	_skybox = new Object(
		Config::f["skybox_scale"],
		"cube_flat.obj",
		new SkyboxMaterial(_currentSkyboxTexture));

	_selectionMarker = new Object(
		Config::f["selection_marker_scale"],
		Config::s["marker_model"],
		new TextureMaterial(
			Texture::load("marker_texture.png"),
			-1.0f,
			Texture::load("marker_specular.png"),
			_currentSkyboxTexture));

	_rootMarker = new Object(
		Config::f["root_marker_scale"],
		Config::s["root_model"],
		new TextureMaterial(
			Texture::load("root_texture.png"),
			-1.0f,
			Texture::load("root_specular.png"),
			_currentSkyboxTexture));

	_lineMaterial = new LineMaterial(200.0f);
	
	_root = NULL;
	_selectedObject = NULL;

	_objects = std::map<int, std::map<int, std::map<int, MObject*>>>();
	_indexOf = std::map<int, int>();
}

Space::~Space()
{
	delete _rootMarker;
	_rootMarker = NULL;
	delete _selectionMarker;
	_selectionMarker = NULL;
	delete _skybox;
	_skybox = NULL;
	delete _lineMaterial;
	_lineMaterial = NULL;
	_root = NULL;
	_selectedObject = NULL;
	clear();
	ShaderProgram::clearShaders();
	Mesh::clearMeshes();
	Texture::clearImages();
	// Problem
	//Texture::clearTextures();
	//Texture::clearCubemaps();
}

void Space::addRoot(int pType, int pID, int pVersion, std::string pTitle, std::string pModel)
{
	Config::rotationPoint = glm::vec3(0.0f);
	ObjVer objVer = ObjVer(pType, pID, pVersion, pTitle);
	float pickIdentifier = (1.0f / 256) * (_objectCount + 1);
	std::string model = Config::s[pModel];

	MObject* root = new MObject(objVer, pickIdentifier, model, _currentSkyboxTexture);

	_selectedObject = root;
	_root = _selectedObject;

	_add(root);

	int x = _indexOf[pType] * Config::f["spacing_x"];
	int y = (_objects[pType].size() - 1) * Config::f["spacing_y"];
	int z = 0;

	root->getTransform()->setPosition(freePosition(glm::vec3(x, -y, z)));
}

void Space::makeRoot()
{
	if (_selectedObject != NULL) {
		clear();
		_lineMaterial->clearFromTo();
		Config::rotationPoint = glm::vec3(0.0f);
		_selectionMarker->getTransform()->setPosition(glm::vec3(0.0f));
		_rootMarker->getTransform()->setPosition(glm::vec3(0.0f));
		/*Debug::now("MakeRoot");
		Debug::now("# " + std::to_string(_objectCount));
		Debug::now("Types " + std::to_string(_objects.size()));
		Debug::now("Indexes " + std::to_string(_indexOf.size()));
		Debug::now("Occupied " + std::to_string(_occupied.size()));*/
	}
}

void Space::addFrom(int pFromType, int pFromID, int pFromVersion, std::string pTitle, std::string pFromModel)
{
	if (_selectedObject != NULL) {
		ObjVer objVer = ObjVer(pFromType, pFromID, pFromVersion, pTitle);
		float pickIdentifier = (1.0f / 256) * (_objectCount + 1);
		std::string model = Config::s[pFromModel];

		MObject* from = new MObject(objVer, pickIdentifier, model, _currentSkyboxTexture);
		MObject* alreadyExisting = _add(from);

		if (alreadyExisting == NULL) {
			_selectedObject->addFrom(from, false);
			_lineMaterial->addFrom(_selectedObject, from); // Upravit len na tuto line // Vytvor ciaru
		}
		else { // ALREADY EXISTS
			delete from;
			from = NULL;

			_selectedObject->addFrom(alreadyExisting, true);
			_lineMaterial->addFrom(_selectedObject, alreadyExisting);
		}
		toggleRender(pFromType, Config::lineStatus[pFromType]);
	}
}

void Space::addTo(int pToType, int pToID, int pToVersion, std::string pTitle, std::string pToModel)
{
	if (_selectedObject != NULL) {
		ObjVer objVer = ObjVer(pToType, pToID, pToVersion, pTitle);
		float pickIdentifier = (1.0f / 256) * (_objectCount + 1);
		std::string model = Config::s[pToModel];
		
		MObject* to = new MObject(objVer, pickIdentifier, model, _currentSkyboxTexture);
		MObject* alreadyExisting = _add(to);

		if (alreadyExisting == NULL) {
			_selectedObject->addTo(to, false);
			_lineMaterial->addTo(_selectedObject, to);
		}
		else { // ALREADY EXISTS
			delete to;
			to = NULL;

			_selectedObject->addTo(alreadyExisting, true);
			_lineMaterial->addTo(_selectedObject, alreadyExisting);
		}
		toggleRender(pToType, Config::lineStatus[pToType]);
	}
}

void Space::removeFrom()
{
	if (_selectedObject != NULL) {
		//Debug::now("F Remove");
		for (auto object : _selectedObject->removeFrom()) {
			_lineMaterial->removeFrom(object);
			_lineMaterial->removeTo(object);
			_remove(object);
		}
	}
}

void Space::removeTo()
{
	if (_selectedObject != NULL) {
		//Debug::now("T Remove");
		for (auto object : _selectedObject->removeTo()) {
			_lineMaterial->removeTo(object);
			_lineMaterial->removeFrom(object);
			_remove(object);
		}
	}
}

void Space::clear()
{
	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				delete version.second;
				version.second = NULL;
			}
			id.second.clear();
		}
		type.second.clear();
	}
	_objectCount = 0;
	_objects.clear();
	_indexOf.clear();
	_occupied.clear();

	/*for (auto x : _occupied) {
		x.second.clear();
	}
	_occupied.clear();*/

	Debug::log("C++ Space.clear: removed all objects");
}

void Space::update(float pElapsedTime)
{
	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				version.second->update(pElapsedTime);
			}
		}
	}
}

std::string Space::pickObject(glm::mat4* pModel, glm::mat4* pProjection, int pMouseX, int pMouseY, GLuint pFrameBufferId)
{
	float count = 256.0f;
	glBindFramebuffer(GL_FRAMEBUFFER, pFrameBufferId); // Object picking
	glClearColor(0.0f, 0.0f, 1.0f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	MObject* object = NULL; 
	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				object = version.second;
				object->getMaterial()->renderPickIdentifier(object->getMesh(), object->getTransform()->getTransform(), pModel, pProjection);
			}
		}
	}
	glFinish();
	glm::vec3 pixel;
	glReadPixels(pMouseX, pMouseY, 1, 1, GL_RGB, GL_FLOAT, &pixel);
	pixel = glm::vec3(std::roundf(pixel.r * count) / count);
	std::string objectTypeIDVersion = "NULL";
	if (_selectedObject != NULL) {
		_selectedObject = NULL;
	}
	bool foundObject = false; 
	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				object = version.second;
				if (pixel == glm::vec3((std::roundf(object->getPickIdentifier() * count) / count))) {
					_selectedObject = object;
					Config::rotationPoint = _selectedObject->getTransform()->getPosition();
					_selectionMarker->getTransform()->setPosition(_selectedObject->getTransform()->getPosition());
					objectTypeIDVersion = _selectedObject->objVer.toString();
					foundObject = true;
					break;
				}
			}
			if (foundObject) break;
		}
		if (foundObject) break;
	}
	float pickIdentifier = -1;
	if (_selectedObject != NULL) pickIdentifier = _selectedObject->getPickIdentifier();
	Debug::log("C++ Space.pickObject: " + objectTypeIDVersion + " | " + std::to_string(pickIdentifier));
	return objectTypeIDVersion;
}

void Space::render(Camera* pCamera)
{
	glBindFramebuffer(GL_FRAMEBUFFER, 0); // Default
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	// Render Skybox
	_skybox->getMaterial()->render(
		_skybox->getMesh(),
		_skybox->getTransform()->getTransform(),
		pCamera->getTransform()->getTransform(),
		pCamera->getProjection());
	// Render objects
	MObject* object = NULL;
	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				object = version.second;
				if (object->render) {
					object->getMaterial()->render(
						object->getMesh(), 
						object->getTransform()->getTransform(), 
						pCamera->getTransform()->getTransform(), 
						pCamera->getProjection());
				}
			}
		}
	}
	// Render selection marker
	if (_selectedObject != NULL) {
		_selectionMarker->getMaterial()->render(
			_selectionMarker->getMesh(), 
			_selectionMarker->getTransform()->getTransform(), 
			pCamera->getTransform()->getTransform(), 
			pCamera->getProjection());
	}
	// Render root marker
	_rootMarker->getMaterial()->render(
		_rootMarker->getMesh(),
		_rootMarker->getTransform()->getTransform(),
		pCamera->getTransform()->getTransform(),
		pCamera->getProjection());
	// Render lines
	_renderLines(pCamera->getViewProjection());
	// Render Text
	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				object = version.second;
				if (object->render) {
					object->textMaterial->render(
						object->getTransform()->getTransform(), 
						pCamera);
				}
			}
		}
	}
	glFinish();
}

void Space::toggleRender(int pType, bool pBool)
{
	if (_objects.find(pType) != _objects.end()) {
		for (auto& id : _objects[pType]) {
			for (auto& version : id.second) {
				version.second->render = Config::lineIDStatus[pType][id.first] && pBool;
				//version.second->render = pBool;
			}
		}
		_lineMaterial->toggleRender(pType, pBool);
	}
}
void Space::toggleRender(int pType, int pID, bool pBool)
{
	if (_objects.find(pType) != _objects.end()) {
		for (auto& id : _objects[pType]) {
			if (id.first == pID) {
				for (auto& version : id.second) {
					version.second->render = pBool;
				}
			}
		}
		_lineMaterial->toggleRender(pType, pID, pBool);
	}
}
glm::vec3 Space::freePosition(glm::vec3 pPosition)
{
	int yCount = 0;
	if (_occupied.find(pPosition.x) != _occupied.end()) {
		if (_occupied[pPosition.x].find(pPosition.z) != _occupied[pPosition.x].end()) {
			yCount = _occupied[pPosition.x][pPosition.z];
		}
		else {
			_occupied[pPosition.x][pPosition.z] = 0;
		}
	}
	else {
		_occupied[pPosition.x][pPosition.z] = 0;
	}


	_occupied[pPosition.x][pPosition.z] += 1;
	return pPosition + glm::vec3(0.0f, -yCount * Config::f["spacing_y"], 0.0f);
}

void Space::cycleSkybox()
{
	_currentSkyboxIndex = (_currentSkyboxIndex < _skyboxTextures.size() - 1) ? _currentSkyboxIndex + 1 : 0;
	_currentSkyboxTexture = _skyboxTextures.at(_currentSkyboxIndex);

	_skybox->getMaterial()->setSkyboxCubemap(_currentSkyboxTexture);

	for (auto& type : _objects) {
		for (auto& id : type.second) {
			for (auto& version : id.second) {
				version.second->getMaterial()->setSkyboxCubemap(_currentSkyboxTexture);
			}
		}
	}
}

void Space::_loadSkyboxTextures()
{
	std::string directory = Config::applicationPath + Config::s["cubemap_path"];
	std::string path;
	std::string cmd;
	for (const auto& entry : std::filesystem::directory_iterator(directory)) {
		path = entry.path().string();
		cmd = path.substr(directory.size(), path.size());
		cmd += "/";
		std::cout << cmd << std::endl;
		_skyboxTextures.push_back(Texture::loadCubemap(cmd, ".png"));
	}
	_currentSkyboxTexture = _skyboxTextures.at(0);
	_currentSkyboxIndex = 0;
}

// -------------------------------------------- PRIVATE --------------------------------------------
void Space::_renderLines(glm::mat4* pViewProjection)
{
	_lineMaterial->render(pViewProjection);
}

glm::vec3 Space::_calculateFromPosition(int pType)
{
	int x = _indexOf[pType] * Config::f["spacing_x"];
	int y = (_objects[pType].size() - 1) * Config::f["spacing_y"];
	int z = Config::f["spacing_z"];

	return glm::vec3(x, -y, z);
}

glm::vec3 Space::_calculateToPosition(int pType)
{
	int x = _indexOf[pType] * Config::f["spacing_x"];
	int y = (_objects[pType].size() - 1) * Config::f["spacing_y"];
	int z = Config::f["spacing_z"];

	return glm::vec3(x, -y, -z);
}

MObject* Space::_add(MObject* pObject)
{
	ObjVer objVer = pObject->objVer;
	MObject* result = NULL;

	if (_indexOf.find(objVer.type) == _indexOf.end()) {
		_indexOf[objVer.type] = _objects.size();
	}
	
	if (_objects[objVer.type][objVer.ID].find(objVer.version) == _objects[objVer.type][objVer.ID].end()) {
		_objects[objVer.type][objVer.ID][objVer.version] = pObject;
		_objectCount += 1;
		Debug::log("C++ Space._add: " + objVer.toString());
		result = NULL;
	}
	else {
		Debug::log("C++ Space.add: ALREADY COINTAINS " + std::to_string(objVer.type) + Config::itemSplitter + std::to_string(objVer.ID) + Config::itemSplitter + std::to_string(objVer.version));
		result = _objects[objVer.type][objVer.ID][objVer.version];
	}
	return result;
}

void Space::_remove(MObject* pObject)
{
	if (pObject != NULL) {
		_occupied[pObject->getTransform()->getPosition().x][pObject->getTransform()->getPosition().z] -= 1;

		for (auto& type : _objects) {
			if (type.first != pObject->objVer.type) continue;
			for (auto& id : type.second) {
				if (id.first != pObject->objVer.ID) continue;
				for (auto& version : id.second) {
					if (version.first != pObject->objVer.version) continue;
					if (version.second == pObject) {
						delete version.second;
						version.second = NULL;
						id.second.erase(version.first);
						_objectCount -= 1;
					}
				}
				if (id.second.size() == 0) {
					type.second.erase(id.first);
				}
			}
			if (type.second.size() == 0) {
				_objects.erase(type.first);
			}
		}
		Debug::log("C++ Space._remove: " + std::to_string(_objects.size()) + " types | " + std::to_string(_objectCount) + " objects");

		int index = 0;
		for (auto& type : _objects) {
			for (auto& id : type.second) {
				for (auto& version : id.second) {
					version.second->setPickIdentifier((1.0f / 256) * (index + 1));
					index += 1;
				}
			}
		}
	}
}