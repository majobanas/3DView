#include "core/Config.h"

// -------------------------------------------- CONFIG --------------------------------------------
// -------------------------------------------- PUBLIC --------------------------------------------
const std::string Config::objVerSplitter = "%";
const std::string Config::itemSplitter = "?";
std::map<int, bool> Config::lineStatus = std::map<int, bool>();
std::map<int, std::map<int, bool>> Config::lineIDStatus = std::map<int, std::map<int, bool>>();

std::map<std::string, int> Config::i = std::map<std::string, int>();
std::map<std::string, float> Config::f = std::map<std::string, float>();
std::map<std::string, std::string> Config::s = std::map<std::string, std::string>();

std::string Config::applicationPath = "";

glm::vec3 Config::rotationPoint = glm::vec3(0.0f);

glm::vec3 Config::selectedColor = glm::vec3(0.0f, 1.0f, 1.0f);
glm::vec3 Config::rootColor = glm::vec3(1.0f, 0.0f, 1.0f);

void Config::addTypeToLineStatus(int pType)
{
	lineStatus[pType] = true;
}

void Config::addTypeIDToLineStatus(int pType, int pID) {
	lineIDStatus[pType][pID] = true;
}

void Config::readConfig(std::string pConfigPath)
{
	std::ifstream config(applicationPath + pConfigPath, std::ios::in);
	if (config.is_open()) {
		std::string line;
		while (getline(config, line))
		{
			if (line.size() != 0 && line[0] != '/') {
				std::vector<std::string> typePair;
				boost::split(typePair, line, boost::is_any_of(" "));
				std::vector<std::string> keyValue;
				boost::split(keyValue, typePair.at(1), boost::is_any_of("="));
				if (typePair.at(0) == "i") {
					if (keyValue.at(0) == "models") {
						std::vector<std::string> models;
						boost::split(models, keyValue.at(1), boost::is_any_of(","));
						for (auto model : models) {
							Config::s[model + "_model"] = model + ".obj";
						}
					}
					else {
						Config::i[keyValue.at(0)] = std::stoi(keyValue.at(1));
					}
				}
				if (typePair.at(0) == "f")
					Config::f[keyValue.at(0)] = std::stof(keyValue.at(1));
				if (typePair.at(0) == "s")
					Config::s[keyValue.at(0)] = keyValue.at(1);
			}
		}
		config.close();
	} else std::cout << "C++ Config: readConfig: could not find or open config" << std::endl;
}

float Config::lerp(float pA, float pB, float pT)
{
	if (glm::abs(pA - pB) < 0.01f)
		return pA;
	return pA * (1.0f - pT) + pB * pT;
}

glm::vec3 Config::lerp(glm::vec3 pA, glm::vec3 pB, float pT)
{
	return glm::vec3(
		lerp(pA.x, pB.x, pT),
		lerp(pA.y, pB.y, pT),
		lerp(pA.z, pB.z, pT));
}

glm::vec3 Config::getColor(std::string pHexColor)
{
	const char* cstr = pHexColor.c_str();
	int r, g, b;
	sscanf(cstr, "%02x%02x%02x", &r, &g, &b);
	return glm::vec3(r / 255.0f, g / 255.0f, b / 255.0f);
}


// -------------------------------------------- PRIVATE --------------------------------------------
Config::Config()
{
}

Config::~Config()
{
}


void Debug::now(std::string pMessage)
{
	if (_now) std::cout << pMessage << std::endl;
}

// -------------------------------------------- DEBUG --------------------------------------------
// -------------------------------------------- PUBLIC --------------------------------------------
void Debug::log(std::string pMessage)
{
	if (_log) std::cout << pMessage << std::endl;
}
void Debug::error(std::string pMessage)
{
	if (_error) std::cout << pMessage << std::endl;
}
// -------------------------------------------- PRIVATE --------------------------------------------

bool Debug::_now = true;
bool Debug::_log = false;
bool Debug::_error = true;

Debug::Debug()
{
}

Debug::~Debug()
{
}
