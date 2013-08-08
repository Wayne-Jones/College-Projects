#include <string>
#include <iostream>
#include <algorithm>
#include <vector>
#include <random>
#include <ctime>
#include <map>


using namespace std;

class gumBall{
private:
	string color; //color of the gumball
	bool purchased; //bool value on whether the gumball has been purchased or not

public:
	gumBall(string color, bool purchased): color(color), purchased(purchased){}
	string getColor(){return color;}
	bool getPurchased(){return purchased;}
	void setPurchased(bool val){this->purchased=val;}
};
void randomizeMachine(vector <gumBall> & gumBallVector){
	//Randomizes the number of gumballs of each color and insert it into the passed in array
	srand((unsigned)time(nullptr)); //resets algorithm for rand() function
	int randomNumber;
	cout<<"Welcome to the CIMS Gumball Machine Simulator"<<endl;
	cout<<"You are starting with the following Gumballs:"<<endl;

	randomNumber = rand()% 6 + 10; //Randomly generates a number between 10 and 15
	cout<<randomNumber<<" Yellow"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Inserts the number of gumballs based on the random number generated
		//Yellow gumball
		gumBall tempGB= gumBall("Yellow", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 10 + 1; //Random number between 1 and 10
	cout<<randomNumber<<" Blue"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Blue gumball
		gumBall tempGB= gumBall("Blue", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 10 + 6; //Random number between 6 and 15
	cout<<randomNumber<<" White"<<endl;
	for(int i=0; i<randomNumber; i++){
		//White gumball
		gumBall tempGB= gumBall("White", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 16 + 10; //Random number between 10 and 25
	cout<<randomNumber<<" Green"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Green gumball
		gumBall tempGB= gumBall("Green", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 13 + 1; //Random number between 1 and 12
	cout<<randomNumber<<" Black"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Black gumball
		gumBall tempGB= gumBall("Black", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 6 + 5; //Random number between 5 and 10
	cout<<randomNumber<<" Purple"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Purple gumball
		gumBall tempGB= gumBall("Purple", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 3 + 4; //Random number between 4 and 6
	cout<<randomNumber<<" Silver"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Silver gumball
		gumBall tempGB= gumBall("Silver", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 8 + 5; //Random number between 5 and 12
	cout<<randomNumber<<" Cyan"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Cyan gumball
		gumBall tempGB= gumBall("Cyan", false);
		gumBallVector.push_back(tempGB);
	}
	randomNumber = rand()% 10; //Random number between 0 and 10
	cout<<randomNumber<<" Magenta"<<endl;
	for(int i=0; i<randomNumber; i++){
		//Magenta gumball
		gumBall tempGB= gumBall("Magenta", false);
		gumBallVector.push_back(tempGB);
	}
	cout<<"and 1 Red"<<endl;
	gumBall tempGB= gumBall("Red", false);
	gumBallVector.push_back(tempGB);
}
void purchaseGumball(vector <gumBall> & gumBallVector){
	//Function picks a random gumBall from the gumBallVector, changes it's status to purchased
	int vecSize = gumBallVector.size();
	int randomNumber;
	int totalPurchased=0;
	cout<<"Here are your random purchases:"<<endl;
	vector<pair<string,int>> counterVec;
	while(gumBallVector[vecSize-1].getPurchased()==false){
		//While Red GumBall is not purchased, execute loop
		randomNumber=rand()%(vecSize); //Random number between 0 and the Vector Size-1
		if(gumBallVector[randomNumber].getPurchased()==false){
			//If the gumball at the selected random point in the vector is not purchased, purchase it
			gumBallVector[randomNumber].setPurchased(true);
			totalPurchased++;
			cout<<gumBallVector[randomNumber].getColor()<<endl;
		}
		bool colorInVec=false;
		for(vector<pair<string,int>>::iterator m = counterVec.begin(); m!= counterVec.end(); m++){
			//Go through the vector map
			if(m->first==gumBallVector[randomNumber].getColor()){
				//If the color exists in the vector update the counter by one
				m->second++;
				colorInVec=true;
			}
		}
		if(colorInVec==false){
			//If the color does not exist in the vector, add it and start the counter at 1
			pair<string,int> p;
			p.first=gumBallVector[randomNumber].getColor();
			p.second=1;
			counterVec.push_back(p);
		}
	}
	
	double totalCost=(totalPurchased*0.25);
	cout<<"You purchased "<<totalPurchased<<" Gumballs, for a total of $"<<totalCost<<"."<<endl;
	int highestVal=0;
	for(vector<pair<string,int>>::iterator m = counterVec.begin(); m!= counterVec.end(); m++){
		//Go through the vector map to find the highest number in the vector map
		if(m->second>highestVal){
			//If there is a higher value than the current one set, update the value
			highestVal=m->second;
		}
	}
	cout<<"The color(s) purchased most: ";
	for(vector<pair<string,int>>::iterator m = counterVec.begin(); m!= counterVec.end(); m++){
		//Print out all possible colors that match the highest number of gumballs
		if(m->second==highestVal){
			cout<<m->first<<" ";
		}
	}
	cout<<endl;

}
void gumBallMachineSim(){
	//This function will execute all code and call functions that will randomize and purchase the gumballs
	vector<gumBall> gumBallVect;
	randomizeMachine(gumBallVect);
	purchaseGumball(gumBallVect);
}

int main(){
	gumBallMachineSim();
	system("pause");
	return 0;
}