#include "core/Input.h"

// -------------------------------------------- PUBLIC --------------------------------------------
bool Input::getKeyPressed(Key pKey)
{
	return sf::Keyboard::isKeyPressed((sf::Keyboard::Key)pKey);
}

bool Input::getMousePressed(Key pMouseButton)
{
	return sf::Mouse::isButtonPressed((sf::Mouse::Button)pMouseButton);
}
glm::ivec2 Input::getMousePosition(sf::RenderWindow* pWindow)
{
	sf::Vector2i position = sf::Mouse::getPosition(*pWindow);
	return glm::ivec2(position.x, position.y);
}
// -------------------------------------------- PRIVATE --------------------------------------------
Input::Input()
{
}

Input::~Input()
{
}
