#include "ThreeDView.h"

class Config;

// -------------------------------------------- PUBLIC --------------------------------------------
ThreeDView::ThreeDView(std::string pApplicationPath)
{
	Config::applicationPath = pApplicationPath;
	Debug::now("Application path: " + Config::applicationPath);
	Config::readConfig("assets/config.txt");
}

ThreeDView::~ThreeDView()
{
	delete _view;
	_view = NULL;
}

void ThreeDView::initializeView(HWND__* pHandle)
{
	Debug::log("C++: ThreeDView - InitializeView");
	_view = new View(pHandle,
		Config::f["field_of_view"],
		Config::i["update_frequency"],
		Config::i["render_frequency"]);
}

void ThreeDView::addRoot(int pType, int pID, int pVersion, std::string pTitle, std::string pModel)
{
	_view->space->addRoot(pType, pID, pVersion, pTitle, pModel);
}

void ThreeDView::makeRoot()
{
	_view->space->makeRoot();
}

void ThreeDView::addFrom(int pFromType, int pFromID, int pFromVersion, std::string pTitle, std::string pFromModel)
{
	_view->space->addFrom(pFromType, pFromID, pFromVersion, pTitle, pFromModel);
}

void ThreeDView::addTo(int pToType, int pToID, int pToVersion, std::string pTitle, std::string pToModel)
{
	_view->space->addTo(pToType, pToID, pToVersion, pTitle, pToModel);
}

void ThreeDView::removeFrom()
{
	_view->space->removeFrom();
}

void ThreeDView::removeTo()
{
	_view->space->removeTo();
}

void ThreeDView::clear()
{
	_view->space->clear();
}

bool ThreeDView::timeToInput()
{
	return _view->timeToInput();
}

bool ThreeDView::timeToUpdate()
{
	return _view->timeToUpdate();
}

bool ThreeDView::timeToRender()
{
	return _view->timeToRender();
}

bool ThreeDView::timeToSecond()
{
	return _view->timeToSecond();
}

void ThreeDView::processInput()
{
	_view->processInput();
}

void ThreeDView::processUpdate()
{
	_view->processUpdate();
}

void ThreeDView::processRender()
{
	_view->processRender();
}

void ThreeDView::processSecond()
{
	_view->processSecond();
}

void ThreeDView::toggleRender(int pType, bool pBool)
{
	_view->toggleRender(pType, pBool);
}

void ThreeDView::toggleRender(int pType, int pID, bool pBool)
{
	_view->space->toggleRender(pType, pID, pBool);
}

std::string ThreeDView::getObjectTypeIDVersion()
{
	return _view->getObjectTypeIDVersion();
}

void ThreeDView::setConfigString(std::string pKey, std::string pValue)
{
	Config::s[pKey] = pValue;
}

void ThreeDView::setConfigFloat(std::string pKey, float pValue)
{
	Config::f[pKey] = pValue;
}

void ThreeDView::setConfigInteger(std::string pKey, int pValue)
{
	Config::i[pKey] = pValue;
}

std::string ThreeDView::getConfigString(std::string pKey)
{
	return Config::s[pKey];
}

float ThreeDView::getConfigFloat(std::string pKey)
{
	return Config::f[pKey];
}

int ThreeDView::getConfigInteger(std::string pKey)
{
	return Config::i[pKey];
}

void ThreeDView::addTypeToLineStatus(int pType)
{
	Config::addTypeToLineStatus(pType);
}

void ThreeDView::cycleSkybox()
{
	_view->space->cycleSkybox();
}
