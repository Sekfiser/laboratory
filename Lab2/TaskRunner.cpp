#include "TaskRunner.h"

TaskRunner::TaskRunner(std::vector <int> arrA, std::vector <int> arrB, int countTh) : arrA(arrA), arrB(arrB), countThreads(countTh)
{
	executeSingleThread();
	executeThreadCS();
	executeThreadRV();
}

void TaskRunner::executeSingleThread()
{
	double result = 0;

	double time = omp_get_wtime();
	for (int i = 0; i < arrA.size(); i++) {
		if (arrA[i] == arrB[i]) {
			result++;
		}
	}
	time = omp_get_wtime() - time;
	
	std::cout << "Выполнение в однопоточном режиме. Найдено соответствий: " << result << ". Время: " << time << "мс\n";
}

void TaskRunner::executeThreadCS()
{
	double result = 0;
	double pResult = 0;

	double time = omp_get_wtime();
	omp_set_dynamic(false);
	omp_set_num_threads(countThreads);
#pragma omp parallel firstprivate(pResult) shared(result) 
	{
		pResult = 0;
#pragma omp for
		for (int i = 0; i < arrA.size(); i++) {
			if (arrA[i] == arrB[i]) {
				pResult++;
			}
		}
#pragma omp critical
		{
			result += pResult;
		}
	}
	time = omp_get_wtime() - time;
	std::cout << "Выполнение в многопоточном режиме(критическая секция). Найдено соответствий: " << result << ". Время: " << time << "мс\n";
}

void TaskRunner::executeThreadRV()
{
	double result = 0;

	double time = omp_get_wtime();
#pragma omp parallel for reduction (+:result)	
	for (int i = 0; i < arrA.size(); i++) {
		if (arrA[i] == arrB[i]) {
			result++;
		}
	}
	time = omp_get_wtime() - time;
	std::cout << "Выполнение в многопоточном режиме(редуктивная переменная). Найдено соответствий: " << result << ". Время: " << time << "мс";
}