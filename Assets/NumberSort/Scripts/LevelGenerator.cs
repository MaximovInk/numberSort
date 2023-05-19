
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
            MakeContainers();

            List<int> integers = new List<int>();

            for (int i = 0; i < containers.Count - 1; i++)
            {
                var nums = GenerateNums();
                for (int j = 0; j < nums.Length; j++)
                {
                    //integers.Add(nums[j]);
                    containers[i].Add(nums[j]);
                }
            }

            int iterations = 0;
            int save_limit = containers.Count * 4 * 4;
            for (int i = 0; i < integers.Count; i++)
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
            List<int> numbers = new List<int>();

            int firstNumber = Random.Range(1, 7);

            numbers.Add(firstNumber);

            int sumToGenerate = 10 - firstNumber;

            int slotLeft = 3;

            while(sumToGenerate > 0)
            {
                if(slotLeft < 2)
                {
                    numbers.Add(sumToGenerate);

                    break;
                }
                slotLeft--;

                int num = Random.Range(1, Mathf.Min(sumToGenerate, 7));
                sumToGenerate -= num;
                numbers.Add(num);
            }


            return numbers.ToArray();
        }
         
    }
}

  
