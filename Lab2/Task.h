#pragma once
#include <iostream>
#include <vector>
#include <string>
#include <sstream>

class Task {
public:
	Task(int countElements, int threadCount);
private:
	std::vector <int> arrA;
	std::vector <int> arrB;
	friend void writeOutputData(std::string name, int result, double time);
	void initArrayRandomData(int countElements);
};