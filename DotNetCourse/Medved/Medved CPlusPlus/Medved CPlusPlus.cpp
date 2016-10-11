// Medved CPlusPlus.cpp : main project file.

#include "stdafx.h"

using namespace System;

private ref class MedvedCPlusPlus sealed : MedvedFS::MedvedFSharp
{
public:
	void MeetMedved() override 
	{
		Console::WriteLine("Preved from C++");
		MedvedFS::MedvedFSharp::MeetMedved();
	}
};
