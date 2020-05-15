#pragma once

#include <iostream>
#include <filesystem>

#include <vector>
#include <map>
#include "SFML/Graphics.hpp"
#include "core/Config.h"
#include "core/MObject.h"
#include "core/Camera.h"
#include "core/Texture.h"
#include "core/material/LineMaterial.h"
#include "core/material/SkyboxMaterial.h"

class Space {
public:
	Space();
	~Space();

	void addRoot(int pType, int pID, int pVersion, std::string pTitle, std::string pModel);

	void makeRoot();

	void addFrom(int pFromType, int pFromID, int pFromVersion, std::string pTitle, std::string pFromModel);
	void addTo(int pToType, int pToID, int pToVersion, std::string pTitle, std::string pToModel);
	
	void removeFrom();
	void removeTo();

	// Delete objects
	void clear();

	// Update space and existing objects
	void update(float pElapsedTime);

	std::string pickObject(glm::mat4* pModel, glm::mat4* pProjection, int pMouseX, int pMouseY, GLuint pFrameBufferId);
	// Render everything
	void render(Camera* pCamera);

	void toggleRender(int pType, bool pBool);
	void toggleRender(int pType, int pID, bool pBool);

	static glm::vec3 freePosition(glm::vec3 pPosition);

	void cycleSkybox();

private:
	Object* _rootMarker = NULL;
	Object* _selectionMarker = NULL;


	void _loadSkyboxTextures();
	std::vector<Texture*> _skyboxTextures;
	int _currentSkyboxIndex = 0;
	Texture* _currentSkyboxTexture = NULL;
	Object* _skybox = NULL;

	LineMaterial* _lineMaterial = NULL;

	MObject* _root = NULL;
	MObject* _selectedObject = NULL;

	MObject* _add(MObject* pObject);
	void _remove(MObject* pObject);

	// Render lines
	void _renderLines(glm::mat4* pViewProjection);

	int _objectCount = 0;
	std::map<int, std::map<int, std::map<int, MObject*>>> _objects;
	std::map<int, int> _indexOf;
	static std::map<int, std::map<int, int>> _occupied;


	glm::vec3 _calculateFromPosition(int pType);
	glm::vec3 _calculateToPosition(int pType);
};