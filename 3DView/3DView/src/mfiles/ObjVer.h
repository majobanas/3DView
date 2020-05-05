#pragma once

#include <iostream>
#include <string>

#include "core/Config.h"

class ObjVer {
public:
	ObjVer();
	ObjVer(int pType, int pID, int pVersion, std::string pTitle);
	~ObjVer();

	int type;
	int ID;
	int version;
	std::string title;

	bool equals(ObjVer pObjVer);

	std::string toString();
private:
	
};
