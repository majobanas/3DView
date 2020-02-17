#pragma once

#include <iostream>
#include <fstream>
#include <istream>
#include <sstream>
#include <boost/algorithm/string.hpp>


#include <map>
#include <vector>

class Config {
public:
	static char DELIMITER;
	static std::string CONFIG;

	static void initialize();

	static std::map<std::string, std::string> VALUE;
	

private:
	Config();
	~Config();

	static std::vector<std::string> _split(std::string pString);
};
