#include <iostream>

#include "core/Config.h"
#include "core/View.h"

int main() {
	std::cout << "Start up" << std::endl;
	Config::initialize();
	View* view = new View(
		std::stoi(Config::VALUE["resolution_x"]),
		std::stoi(Config::VALUE["resolution_y"]),
		std::stof(Config::VALUE["field_of_view"]),
		std::stoi(Config::VALUE["update_frequency"]),
		std::stoi(Config::VALUE["render_frequency"]));
	view->run();

	std::cout << "Press any key to delete allocated memory" << std::endl;
	std::cin.get();

	delete view;
	view = NULL;

	std::cout << "Press any key to close the program" << std::endl;
	std::cin.get();
	return 0;
}