#include "core/MObject.h"
#include "core/Space.h"

class Space;

// -------------------------------------------- PUBLIC --------------------------------------------
MObject::MObject(ObjVer pObjVer, float pPickIdentifier, std::string pModel, Texture* pSkybox)
	:	Object(Config::i["load_models"] == 1 ? Config::f["model_scale"] : Config::f["object_scale"], 
			getNewModel(pObjVer.type), getNewMaterial(pObjVer.type, pPickIdentifier, pSkybox)),
		objVer(pObjVer),
		render(true),
		_pickIdentifier(pPickIdentifier),
		textMaterial(new TextMaterial(pObjVer.title))
{
	Config::addTypeIDToLineStatus(objVer.type, objVer.ID);

	lineTags = std::vector<std::string>();

	_from = std::map<int, std::vector<MObject*>>();
	_to = std::map<int, std::vector<MObject*>>();
	_fromExisting = std::map<int, std::vector<MObject*>>();
	_toExisting = std::map<int, std::vector<MObject*>>();

	_fromIndexOf = std::map<int, int>();
	_toIndexOf = std::map<int, int>();
}

MObject::~MObject()
{
	delete _transform;
	_transform = NULL;
	// Space.cpp deletes all Meshes (delete _mesh;)
	_mesh = NULL;
	delete _material;
	_material = NULL;
	_clear();
}

void MObject::update(float pElapsedTime)
{

}

void MObject::setPickIdentifier(float pPickIdentifier)
{
	_pickIdentifier = pPickIdentifier;
	_material->setPickIdentifier(_pickIdentifier);
}

float MObject::getPickIdentifier()
{
	return _pickIdentifier;
}

bool MObject::addFrom(MObject* pFrom, bool pAlreadyExists)
{
	if (!pAlreadyExists){
		pFrom->getTransform()->setPosition(Space::freePosition(getTransform()->getPosition() + _calculateFromPosition(pFrom->objVer)));
	}
		
	bool existsInFrom = false;
	bool existsInFromExisting = false;
	if (std::find(_from[pFrom->objVer.type].begin(), _from[pFrom->objVer.type].end(), pFrom) != _from[pFrom->objVer.type].end()) {
		existsInFrom = true;
	}
	if (std::find(_fromExisting[pFrom->objVer.type].begin(), _fromExisting[pFrom->objVer.type].end(), pFrom) != _fromExisting[pFrom->objVer.type].end()) {
		existsInFromExisting = true;
	}
	if (!pAlreadyExists && !existsInFrom) {
		_from[pFrom->objVer.type].push_back(pFrom);
		return true;
	}
	if (pAlreadyExists && !existsInFromExisting) {
		_fromExisting[pFrom->objVer.type].push_back(pFrom);
		return true;
	}
	return false;
}

bool MObject::addTo(MObject* pTo, bool pAlreadyExists)
{
	if (!pAlreadyExists) {
		pTo->getTransform()->setPosition(Space::freePosition(getTransform()->getPosition() + _calculateToPosition(pTo->objVer)));
	}
	bool existsInTo = false;
	bool existsInToExisting = false;
	if (std::find(_to[pTo->objVer.type].begin(), _to[pTo->objVer.type].end(), pTo) != _to[pTo->objVer.type].end()) {
		existsInTo = true;
	}
	if (std::find(_toExisting[pTo->objVer.type].begin(), _toExisting[pTo->objVer.type].end(), pTo) != _toExisting[pTo->objVer.type].end()) {
		existsInToExisting = true;
	}
	if (!pAlreadyExists && !existsInTo) {
		_to[pTo->objVer.type].push_back(pTo);
		return true;
	}
	if (pAlreadyExists && !existsInToExisting) {
		_toExisting[pTo->objVer.type].push_back(pTo);
		return true;
	}
	return false;
}

std::vector<MObject*> MObject::removeFrom()
{
	std::vector<MObject*> result;// = std::vector<Object*>();
	for (auto& type : _from) {
		for (auto fromObject : type.second) {
			for (auto fromBeDeleted : fromObject->removeFrom()) {
				result.push_back(fromBeDeleted);
			}
			for (auto toBeDeleted : fromObject->removeTo()) {
				result.push_back(toBeDeleted);
			}
			result.push_back(fromObject);
		}
	}
	_clearFrom();
	return result;
}

std::vector<MObject*> MObject::removeTo()
{
	std::vector<MObject*> result;// = std::vector<Object*>();
	for (auto& type : _to) {
		for (auto toObject : type.second) {
			for (auto toBeDeleted : toObject->removeTo()) {
				result.push_back(toBeDeleted);
			}
			for (auto fromBeDeleted : toObject->removeFrom()) {
				result.push_back(fromBeDeleted);
			}
			result.push_back(toObject);
		}
	}
	_clearTo();
	return result;
}

void MObject::_clearFrom()
{
	_from.clear();
	_fromIndexOf.clear();
}

void MObject::_clearTo()
{
	_to.clear();
	_toIndexOf.clear();
}

void MObject::_clearFromExisting()
{
	_fromExisting.clear();
}

void MObject::_clearToExisting()
{
	_toExisting.clear();
}

void MObject::_clearLineTags()
{
	lineTags.clear();
}

void MObject::_clear()
{
	_clearFrom();
	_clearTo();
	_clearFromExisting();
	_clearToExisting();
	_clearLineTags();
}

// -------------------------------------------- PRIVATE --------------------------------------------
glm::vec3 MObject::_calculateFromPosition(ObjVer pFrom)
{
	if (_fromIndexOf.find(pFrom.type) == _fromIndexOf.end()) {
		_fromIndexOf[pFrom.type] = _from.size();
	}

	int index = _fromIndexOf[pFrom.type];
	//Debug::now("F " + std::to_string(index));
	int x = index * Config::f["spacing_x"];
	int y = 0;
	int z = Config::f["spacing_z"];
	return glm::vec3(x, -y, z);
}

glm::vec3 MObject::_calculateToPosition(ObjVer pTo)
{
	if (_toIndexOf.find(pTo.type) == _toIndexOf.end()) {
		_toIndexOf[pTo.type] = _to.size();
	}

	int index = _toIndexOf[pTo.type];
	//Debug::now("T " + std::to_string(index));
	int x = index * Config::f["spacing_x"];
	int y = 0;
	int z = Config::f["spacing_z"];
	
	return glm::vec3(x, -y, -z);
}

std::string MObject::getNewModel(int pType)
{
	if (Config::i["load_models"] == 1) {
		std::string modelToFind = std::to_string(pType) + "_model";
		if (Config::s.find(modelToFind) != Config::s.end()) {
			return Config::s[modelToFind];
		}
	}
	return Config::s["project_model"];
}

AbstractMaterial* MObject::getNewMaterial(int pType, float pPickIdentifier, Texture* pSkybox)
{
	std::string textureToLoad = std::to_string(pType);
	if (Config::i["load_models"] == 1) {
		std::string modelToFind = std::to_string(pType) + "_model";
		if (Config::s.find(modelToFind) != Config::s.end()) {
			return new TextureMaterial(
				Texture::load(textureToLoad + "_texture.png"),
				pPickIdentifier,
				Texture::load(textureToLoad + "_specular.png"),
				pSkybox);
		}
	}
	return new TextureMaterial(
		Texture::load(textureToLoad + ".png"),
		pPickIdentifier,
		Texture::load("spec.png"),
		pSkybox);
}
