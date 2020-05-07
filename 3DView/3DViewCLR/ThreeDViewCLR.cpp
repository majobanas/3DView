#include "pch.h"


#include <msclr/marshal_cppstd.h>

#include "ThreeDViewCLR.h"
#include "SFML/Window.hpp"

#include "core/ThreeDView.cpp"

ThreeDViewCLR::ThreeDViewWrapper::ThreeDViewWrapper(System::String^ pApplicationPath)
{
	_threeDView = new ThreeDView(msclr::interop::marshal_as<std::string>(pApplicationPath));
}

ThreeDViewCLR::ThreeDViewWrapper::~ThreeDViewWrapper()
{
	this->!ThreeDViewWrapper();
}

ThreeDViewCLR::ThreeDViewWrapper::!ThreeDViewWrapper()
{
	delete _threeDView;
	_threeDView = NULL;
}

void ThreeDViewCLR::ThreeDViewWrapper::initializeView(System::IntPtr pHandle)
{
	_threeDView->initializeView(static_cast<HWND__*>(pHandle.ToPointer()));
}

void ThreeDViewCLR::ThreeDViewWrapper::setConfigString(String^ pKey, String^ pValue)
{
	_threeDView->setConfigString(asString(pKey), asString(pValue));
}

void ThreeDViewCLR::ThreeDViewWrapper::setConfigFloat(String^ pKey, float pValue)
{
	_threeDView->setConfigFloat(asString(pKey), pValue);
}

void ThreeDViewCLR::ThreeDViewWrapper::setConfigInteger(String^ pKey, int pValue)
{
	_threeDView->setConfigInteger(asString(pKey), pValue);
}

String^ ThreeDViewCLR::ThreeDViewWrapper::getConfigString(String^ pKey)
{
	return gcnew String(_threeDView->getConfigString(asString(pKey)).c_str());
}

float ThreeDViewCLR::ThreeDViewWrapper::getConfigFloat(String^ pKey)
{
	return _threeDView->getConfigFloat(asString(pKey));
}

int ThreeDViewCLR::ThreeDViewWrapper::getConfigInteger(String^ pKey)
{
	return _threeDView->getConfigInteger(asString(pKey));
}

void ThreeDViewCLR::ThreeDViewWrapper::addRoot(int pType, int pID, int pVersion, String^ pTitle, System::String^ pModel)
{
	_threeDView->addRoot(pType, pID, pVersion, asString(pTitle), asString(pModel));
}

void ThreeDViewCLR::ThreeDViewWrapper::makeRoot()
{
	_threeDView->makeRoot();
}

void ThreeDViewCLR::ThreeDViewWrapper::addFrom(int pFromType, int pFromID, int pFromVersion, String^ pTitle, String^ pFromModel)
{
	_threeDView->addFrom(pFromType, pFromID, pFromVersion, asString(pTitle), asString(pFromModel));
}

void ThreeDViewCLR::ThreeDViewWrapper::addTo(int pToType, int pToID, int pToVersion, String^ pTitle, String^ pToModel)
{
	_threeDView->addTo(pToType, pToID, pToVersion, asString(pTitle), asString(pToModel));
}

void ThreeDViewCLR::ThreeDViewWrapper::removeFrom()
{
	_threeDView->removeFrom();
}

void ThreeDViewCLR::ThreeDViewWrapper::removeTo()
{
	_threeDView->removeTo();
}

void ThreeDViewCLR::ThreeDViewWrapper::clear()
{
	_threeDView->clear();
}

std::string ThreeDViewCLR::ThreeDViewWrapper::asString(String^ pString)
{
	return msclr::interop::marshal_as<std::string>(pString);
}

bool ThreeDViewCLR::ThreeDViewWrapper::timeToInput()
{
	return _threeDView->timeToInput();
}

bool ThreeDViewCLR::ThreeDViewWrapper::timeToUpdate()
{
	return _threeDView->timeToUpdate();;
}

bool ThreeDViewCLR::ThreeDViewWrapper::timeToRender()
{
	return _threeDView->timeToRender();
}

bool ThreeDViewCLR::ThreeDViewWrapper::timeToSecond()
{
	return _threeDView->timeToSecond();
}

void ThreeDViewCLR::ThreeDViewWrapper::processInput()
{
	_threeDView->processInput();
}

void ThreeDViewCLR::ThreeDViewWrapper::processUpdate()
{
	_threeDView->processUpdate();
}

void ThreeDViewCLR::ThreeDViewWrapper::processRender()
{
	_threeDView->processRender();
}

void ThreeDViewCLR::ThreeDViewWrapper::processSecond()
{
	_threeDView->processSecond();
}

void ThreeDViewCLR::ThreeDViewWrapper::toggleRender(int pType, bool pBool)
{
	_threeDView->toggleRender(pType, pBool);
}

void ThreeDViewCLR::ThreeDViewWrapper::addTypeToLineStatus(int pType)
{
	_threeDView->addTypeToLineStatus(pType);
}

String^ ThreeDViewCLR::ThreeDViewWrapper::getObjectTypeIDVersion()
{
	return gcnew String(_threeDView->getObjectTypeIDVersion().c_str());
}