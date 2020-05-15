#pragma once
#include "core/ThreeDView.h"

using namespace System;

namespace ThreeDViewCLR {
	public ref class ThreeDViewWrapper
	{
	public:
		ThreeDViewWrapper(System::String^ pApplicationPath);
		~ThreeDViewWrapper();
		!ThreeDViewWrapper();

		void initializeView(IntPtr pHandle);

		bool timeToInput();
		bool timeToUpdate();
		bool timeToRender();
		bool timeToSecond();

		void processInput();
		void processUpdate();
		void processRender();
		void processSecond();

		void toggleRender(int pType, bool pBool);
		void toggleRender(int pType, int pID, bool pBool);

		void addTypeToLineStatus(int pType);

		void cycleSkybox();
	protected:
		String^ getObjectTypeIDVersion();

		void setConfigString(String^ pKey, String^ pValue);
		void setConfigFloat(String^ pKey, float pValue);
		void setConfigInteger(String^ pKey, int pValue);

		String^ getConfigString(String^ pKey);
		float getConfigFloat(String^ pKey);
		int getConfigInteger(String^ pKey);

		void addRoot(int pType, int pID, int pVersion, String^ pTitle, String^ pModel);

		void makeRoot();

		void addFrom(int pFromType, int pFromID, int pFromVersion, String^ pTitle, String^ pFromModel);
		void addTo(int pToType, int pToID, int pToVersion, String^ pTitle, String^ pToModel);

		void removeFrom();
		void removeTo();

		void clear();

	private:
		ThreeDView* _threeDView = NULL;
		
		std::string asString(String^ pString);
	};
}
