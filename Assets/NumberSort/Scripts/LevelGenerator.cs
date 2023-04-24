using System.Collections.Generic;
using UnityEngine;

namespace MaximovInk.NumbersSort
{
    public class LevelGenerator : MonoBehaviour
    {
        public int BliskCount = 3;

        private List<NumbersContainer> containers = new List<NumbersContainer>();

        private void Awake()
        {
            for (int i = 0; i < BliskCount; i++)
            {
                containers.Add(LevelManager.Instance.AddContainer());
            }

            List<int> integers = new List<int>();

            for (int i = 0; i < containers.Count-1; i++)
            {
                var nums = GenerateNums();
                for (int j = 0; j < nums.Length; j++)
                {
                    integers.Add(nums[j]);
                }
            }

            int iterations = 0;
            int save_limit = containers.Count * 4 * 4;
            for (int i = 0; i < integers.Count;)
            {
                var index = Random.Range(0, containers.Count);
                Debug.Log(containers[index].count);
                while (containers[index].count >= 4 && iterations < save_limit)
                {
                    index = Random.Range(0, containers.Count); iterations++;
                }
                containers[index].Add(integers[i]);
                integers.RemoveAt(i);
            }
            Debug.Log(iterations);

        }

        private int[] GenerateNums()
        {
            int[] numbers = new int[Random.Range(2, 5)];
            int sum = 0;

            // Генерируем случайные числа от 1 до 6
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 7);
                sum += numbers[i];
            }

            // Используем алгоритм перебора, чтобы получить числа с суммой равной 10
            while (sum != 10)
            {
                // Определяем случайный индекс массива
                int index = Random.Range(0, numbers.Length);

                // Вычисляем разность между числом в этом индексе и новым случайным числом
                int diff = Random.Range(1, 7) - numbers[index];

                // Обновляем значение числа в массиве и сумму
                if (numbers[index] + diff > 0) // проверка на отрицательное число
                {
                    numbers[index] += diff;
                    sum += diff;
                }
            }

            Debug.Log($"Получены {numbers.Length} случайных числа от 1 до 6, сумма которых равна 10: "
                + string.Join(", ", numbers));

            return numbers;
        }

        private int[] GenerateNums1()
        {
            int[] numbers = new int[4];
            int sum = 0;

            // Генерируем случайные числа от 1 до 6
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 7);
                sum += numbers[i];
            }

            // Используем алгоритм перебора, чтобы получить числа с суммой равной 10
            while (sum != 10)
            {
                // Определяем случайный индекс массива
                int index = Random.Range(0, numbers.Length);

                // Вычисляем разность между числом в этом индексе и новым случайным числом
                int diff = Random.Range(1, 7) - numbers[index];

                // Обновляем значение числа в массиве и сумму
                numbers[index] += diff;
                sum += diff;
            }

            Debug.Log("Получены 4 случайных числа от 1 до 6, сумма которых равна 10: "
                + numbers[0] + ", " + numbers[1] + ", " + numbers[2] + ", " + numbers[3]);

            return numbers;
        }
    }
}

  
