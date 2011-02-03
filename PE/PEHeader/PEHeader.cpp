#include "pe.h"
#include <iostream>
using namespace std;

int main(int argc, char* argv[])
{
	if(argc<2)
	{
		cout<<"Please Input The File Path"<<endl;
		return 0;
	}
	if(argc<3)
	{
		cout<<"Please Input The Alert Message"<<endl;
		return 0;
	}
	Cpe pe;
	if(pe.ModifyPe((CString)argv[1],(CString)argv[2]))
	{
		cout<<"Modify Success, Please Run The Application. "<<endl;
	}
	
	return 0;
}