#pragma once
#include <iostream>
#include <ctime>
#include <stdio.h>
#include <omp.h>
#include <conio.h>
#include <vector>

class TaskRunner {
public:
	TaskRunner(std::vector <int> arrA, std::vector <int> arrB, int countTh);
private:
	int countThreads;
	std::vector <int> arrA;
	std::vector <int> arrB;
	void executeSingleThread();
	void executeThreadCS();
	void executeThreadRV();
};