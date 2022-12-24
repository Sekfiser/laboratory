#include <iostream>
#include "Task.h"

int main()
{
	setlocale(LC_ALL, "Russian");
	
	int elementCount;
	int threadCount;

	bool errorFlag = true;

	do {
		std::cout << "Для выхода из программы введите 0.\nВведите количество элементов от 100000 до 1000000:\n";
		
		if (std::cin >> elementCount && elementCount >= 0) {
			if (elementCount == 0)
			{
				exit(0);
			}

			if (elementCount >= 100000 && elementCount <= 1000000) {
				errorFlag = false;
			} 
			else {
				std::cout << "Значение должно быть в промежутке от 100000 до 1000000! Попробуйте еще раз";
				std::cin.clear();
				std::cin.ignore(256, '\n');
			}
		}
		else {
			std::cout << "Ошибка ввода! Попробуйте еще раз";
			std::cin.clear();
			std::cin.ignore(256, '\n');
		}

	} while (errorFlag);

	errorFlag = true;
	
	do {
		std::cout << "Для выхода из программы введите 0.\nВведите количество потоков:\n";
		
		if (std::cin >> threadCount && threadCount >= 0) {
			if (threadCount == 0)
			{
				exit(0);
			}
			errorFlag = false;
		}
		else {
			std::cout << "Ошибка ввода! Попробуйте еще раз";
			std::cin.clear();
			std::cin.ignore(256, '\n');
		}

	} while (errorFlag);

	Task task(elementCount, threadCount);

	std::cout << "\n\nКонец программы!";

}
