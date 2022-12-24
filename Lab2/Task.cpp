#include "Task.h"
#include "TaskRunner.h"

Task::Task(int countElements, int threadCount)
{
	initArrayRandomData(countElements); 

	TaskRunner taskRunner(arrA, arrB, threadCount);
}

void Task::initArrayRandomData(int countElements)
{
	srand(time(0));
	for (int i = 0; i < countElements; i++) {
		arrA.push_back(rand() % ((10000000 + 1) - 100) + 100);
		arrB.push_back(rand() % ((10000000 + 1) - 100) + 100);
	}
}

void writeOutputData(std::string name, int result, double time)
{
	std::cout << name;

	std::cout << "\nРезультат: " << result << "\nВремя выполнения: " << time << " сек.\n";
}