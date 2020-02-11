#include "core/Input.h"

bool Input::getKeyPressed(Input::Key pKey) {
	return sf::Keyboard::isKeyPressed((sf::Keyboard::Key)pKey);
}

Input::Input()
{

}

Input::~Input()
{

}
