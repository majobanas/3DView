#include <iostream>

#include "core/View.h"

int main() {
	std::cout << "Start up" << std::endl;
	View* view = new View(640, 480);
	view->run();

	std::cout << "Press any key to delete allocated memory" << std::endl;
	std::cin.get();

	delete view;
	view = NULL;

	std::cout << "Press any key to close the program" << std::endl;
	std::cin.get();
	return 0;
}