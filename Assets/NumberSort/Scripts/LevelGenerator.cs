using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MaximovInk.NumbersSort
{
    public class LevelGenerator : MonoBehaviour
    {
        public int BliskCount = 3;

        private List<NumbersContainer> containers = new List<NumbersContainer>();

        private void Awake()
        {
            MakeContainers();


            List<int> integers = new List<int>();



            for (int i = 0; i < containers.Count - 1; i++)
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
                while (containers[index].count >= 4 && iterations < save_limit)
                {
                    index = Random.Range(0, containers.Count); iterations++;
                }
                containers[index].Add(integers[i]);
                integers.RemoveAt(i);
            }



        }

        private void MakeContainers()
        {
            for (int i = 0; i < BliskCount; i++)
            {
                containers.Add(LevelManager.Instance.AddContainer());
            }
        }


        
         
        private int[] GenerateNums()
        {
            int[] numbers = new int[Random.Range(2, 5)];
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 7);
                sum += numbers[i];
            }
            while (sum != 10)
            {
                int index = Random.Range(0, numbers.Length);
                int diff = Random.Range(1, 7) - numbers[index];
                if (numbers[index] + diff > 0)
                {
                    numbers[index] += diff;
                    sum += diff;
                }
            }
            return numbers;
        }
         
    }
}

  
