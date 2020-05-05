#pragma once

#include <iostream>
#include <fstream>
#include <istream>
#include <sstream>
#include <boost/algorithm/string.hpp>
#include "glm/glm.hpp"

#include <string>
#include <map>
#include <vector>

class Config {
public:
	static std::map<int, bool> lineStatus;
	static void addTypeToLineStatus(int pType);

	static const std::string objVerSplitter;
	static const std::string itemSplitter;

	static std::map<std::string, int> i;
	static std::map<std::string, float> f;
	static std::map<std::string, std::string> s;

	static std::string applicationPath;

	static glm::vec3 rotationPoint;

	static glm::vec3 selectedColor;
	static glm::vec3 rootColor;

	static void readConfig(std::string pConfigPath);

	static float lerp(float pA, float pB, float pT);
	static glm::vec3 lerp(glm::vec3 pA, glm::vec3 pB, float pT);

	static glm::vec3 getColor(std::string pHexColor);

private:
	Config();
	~Config();
};

class Debug {
public:
	static void now(std::string pMessage);
	static void log(std::string pMessage);
	static void error(std::string pMessage);
private:
	static bool _now;
	static bool _log;
	static bool _error;

	Debug();
	~Debug();
};
