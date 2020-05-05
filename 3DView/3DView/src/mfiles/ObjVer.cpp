#include "mfiles/ObjVer.h"

class Config;

ObjVer::ObjVer()
{
	type = -1;
	ID = -1;
	version = -1;
	title = "";
}

ObjVer::ObjVer(int pType, int pID, int pVersion, std::string pTitle)
{
	type = pType;
	ID = pID;
	version = pVersion;
	title = pTitle;
}

ObjVer::~ObjVer()
{
}

bool ObjVer::equals(ObjVer pObjVer)
{
	return pObjVer.type == type && pObjVer.ID == ID && pObjVer.version == version;
}

std::string ObjVer::toString()
{
	return std::to_string(type) + Config::itemSplitter + std::to_string(ID) + Config::itemSplitter + std::to_string(version) + Config::itemSplitter + title;
}
