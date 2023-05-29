using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MaximovInk.NumbersSort
{
   

    public class LevelGenerator : MonoBehaviourSingleton<LevelGenerator>
    {
        public int BliskCount = 3;

        private List<NumbersContainer> containers = new List<NumbersContainer>();

        public class ContainerMixData
        {
            public int Count { get {
                    int sum = 0;
                    for (int i = 0; i < nums.Count; i++)
                    {
                        sum += nums[i];
                    }
                    return sum;
                }
            }

            

            public List<int> nums = new List<int>();
        }

        public bool IsGenerated { get; private set; } = false;

        public int CurrentLevel = 0;

        private void Awake()
        {
            MakeContainers();

            //Generate numbers and mix them

            bool isGood = false;

            ContainerMixData[] containersForMix = new ContainerMixData[containers.Count];

            int maxSteps = 20;

            int findGoodLimit = 50;
            int findGoodCount = 0;

            while (!isGood && findGoodCount < findGoodLimit)
            {
                for (int i = 0; i < containers.Count - 1; i++)
                {
                    var nums = GenerateNums();

                    containersForMix[i] = new ContainerMixData();

                    for (int j = 0; j < nums.Length; j++)
                    {
                        containersForMix[i].nums.Add(nums[j]);
                    }
                }
                containersForMix[containers.Count - 1] = new ContainerMixData();

                int mixIterations = Random.Range((int)(containersForMix.Length * 2f), (int)(containersForMix.Length * 3f));

                for (int mixI = 0; mixI < mixIterations;)
                {
                    int containerIndex = Random.Range(0, containersForMix.Length);
                    if (containersForMix[containerIndex].Count == 0)
                        continue;

                    var srcIdx = containerIndex;
                    var srcNum = containersForMix[srcIdx].nums[^1];

                    var dstIdx = srcIdx;
                    var dstNum = 0;

                    while (dstIdx == srcIdx
                        || (dstNum + srcNum == 10 && containersForMix[dstIdx].nums.Count + 1 > 3)
                        || containersForMix[dstIdx].nums.Count > 3)
                    {
                        dstIdx = Random.Range(0, containersForMix.Length);
                        if (containersForMix[dstIdx].nums.Count == 0)
                            dstNum = 0;
                        else
                            dstNum = containersForMix[dstIdx].nums[^1];
                    }

                    if (dstNum != 0)
                    {
                        containersForMix[dstIdx].nums.RemoveAt(containersForMix[dstIdx].nums.Count - 1);
                    }
                    containersForMix[srcIdx].nums.RemoveAt(containersForMix[srcIdx].nums.Count - 1);

                    if (dstNum != 0)
                    {
                        containersForMix[srcIdx].nums.Add(dstNum);
                    }
                    containersForMix[dstIdx].nums.Add(srcNum);

                    mixI++;
                }

                //CHECK IS GOOD

                int checkSum = 0;

                int brokes = 0;

                for (int i = 0; i < containersForMix.Length; i++)
                {
                    int cCount = containersForMix[i].Count;
                    checkSum += cCount;
                    if (cCount == 10) brokes++;
                }

                isGood = checkSum == (containers.Count - 1) * 10;
                isGood &= brokes < 1;

                Debug.Log($"Is GOOD:{isGood}? Iterations:{mixIterations} FindGoodIterations: {findGoodCount}");

                if (isGood)
                    maxSteps = mixIterations;

                findGoodCount++;
            }
            

            //Apply mixed numbers to containers
            for (int i = 0; i < containersForMix.Length; i++)
            {
                for (int j = 0; j < containersForMix[i].nums.Count; j++)
                {
                    containers[i].Add(containersForMix[i].nums[j]);
                }
            }

            IsGenerated = true;
            NumberSortManager.Instance.SetMaxSteps(maxSteps);
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

            bool badGeneration = true;

            while (badGeneration)
            {
                badGeneration = false;

                int firstNumber = Random.Range(1, 7);
                numbers.Add(firstNumber);

                int sumToGenerate = 10 - firstNumber;
                int slotLeft = 3;

                while (sumToGenerate > 0)
                {
                    if (slotLeft < 2)
                    {
                        if (sumToGenerate > 6)
                            badGeneration = true;

                        numbers.Add(sumToGenerate);

                        break;
                    }
                    slotLeft--;

                    int num = Random.Range(1, Mathf.Min(sumToGenerate, 7));
                    sumToGenerate -= num;
                    numbers.Add(num);
                }

                if (badGeneration)
                    numbers.Clear();
            }

            string log = string.Empty;
            for (int i = 0; i < numbers.Count; i++)
            {
                log += numbers[i] + ";";
            }
            Debug.Log(log);

            return numbers.ToArray();
        }
         
    }
}

  
