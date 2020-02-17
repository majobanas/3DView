#include "core/Config.h"

char Config::DELIMITER = '\n';
std::string Config::CONFIG = "../assets/";

std::map<std::string, std::string> Config::VALUE = std::map<std::string, std::string>();

void Config::initialize()
{
	std::ifstream config;
	config.open(CONFIG + "config.txt");
	if (config.is_open()) {
		std::stringstream s;
		s << config.rdbuf();
		DELIMITER = '\n';
		std::vector<std::string> lines = _split(s.str());
		DELIMITER = '=';
		for (int i = 0; i < lines.size(); i++) {
			if (lines.at(i).length() != 0) {
				std::vector<std::string> line = _split(lines.at(i));
				VALUE[line.at(0)] = line.at(1);
			}
		}
		config.close();
	}
	else {
		std::cout << "Unable to open config file." << std::endl;
	}
}

Config::Config()
{
	// Static class, never instantiated
}

Config::~Config()
{
	// Static class, never instantiated
}

std::vector<std::string> Config::_split(std::string pString)
{
	std::vector<std::string> result;
	boost::split(result, pString, [](char d) {return d == DELIMITER; });
	return result;
}
