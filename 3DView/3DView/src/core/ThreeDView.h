#pragma once


#include <iostream>

#include "SFML/Window/WindowHandle.hpp"

#include "core/Config.h"
#include "core/View.h"

class ThreeDView {
public:
	ThreeDView(std::string pApplicationPath);
	~ThreeDView();

	void initializeView(HWND__* pHandle);

	void addRoot(int pType, int pID, int pVersion, std::string pTitle, std::string pModel);

	void makeRoot();

	void addFrom(int pFromType, int pFromID, int pFromVersion, std::string pTitle, std::string pFromModel);
	void addTo(int pToType, int pToID, int pToVersion, std::string pTitle, std::string pToModel);

	void removeFrom();
	void removeTo();

	void clear();

	bool timeToInput();
	bool timeToUpdate();
	bool timeToRender();
	bool timeToSecond();

	void processInput();
	void processUpdate();
	void processRender();
	void processSecond();

	void toggleRender(int pType, bool pBool);
	void toggleRender(int pType, int pID, bool pBool);

	std::string getObjectTypeIDVersion();

	void setConfigString(std::string pKey, std::string pValue);
	void setConfigFloat(std::string pKey, float pValue);
	void setConfigInteger(std::string pKey, int pValue);

	std::string getConfigString(std::string pKey);
	float getConfigFloat(std::string pKey);
	int getConfigInteger(std::string pKey);

	void addTypeToLineStatus(int pType);
	
	void cycleSkybox();
private:
	View* _view = NULL;
};
