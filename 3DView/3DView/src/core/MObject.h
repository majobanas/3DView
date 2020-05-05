#pragma once

#include "core/Object.h"

#include "mfiles/ObjVer.h"


class MObject : public Object {
public:
	MObject(ObjVer pObjVer, float pPickIdentifier, std::string pModel, Texture* pSkybox = NULL);
	virtual ~MObject() override;

	void update(float pElapsedTime);
	ObjVer objVer;


	bool render;

	void setPickIdentifier(float pPickIdentifier);
	float getPickIdentifier();

	bool addFrom(MObject* pFrom, bool pAlreadyExists);
	bool addTo(MObject* pTo, bool pAlreadyExists);

	std::vector<MObject*> removeFrom();
	std::vector<MObject*> removeTo();

	std::vector<std::string> lineTags;


	TextMaterial* textMaterial = NULL;

private:

	float _pickIdentifier = -1.0f;

	std::map<int, std::vector<MObject*>> _from;
	std::map<int, std::vector<MObject*>> _to;
	std::map<int, std::vector<MObject*>> _fromExisting;
	std::map<int, std::vector<MObject*>> _toExisting;

	std::map<int, int> _fromIndexOf;
	std::map<int, int> _toIndexOf;

	void _clearFrom();
	void _clearTo();
	void _clearFromExisting();
	void _clearToExisting();
	void _clearLineTags();
	void _clear();
	// Helper functions
	glm::vec3 _calculateFromPosition(ObjVer pFrom);
	glm::vec3 _calculateToPosition(ObjVer pTo);

	std::string getNewModel(int pType);
	AbstractMaterial* getNewMaterial(int pType, float pPickIdentifier, Texture* pSkyBox);
};
