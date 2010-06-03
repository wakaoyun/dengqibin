// PETest.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include "pe.h"

int main(int argc, char* argv[])
{
	Cpe pe;
	pe.ModifyPe("d:\\e.text","We are the world!");
	return 0;
}