using System.Collections.Generic;
using UnityEngine;

namespace MaximovInk.NumbersSort
{
    [System.Serializable]
    public class ContaierData
    {
        public List<int> numbers = new List<int>();
    }

    [System.Serializable]
    public class LevelData
    {
        public ContaierData[] Containers;
        public int Total;
        public int Goal;
        public int Steps;
        public int SecretNumbers;
    }

    [CreateAssetMenu(fileName = "LevelDatabase", menuName = "NumbersSort/LevelDatabase", order = 1)]
    public class LevelDatabase : ScriptableObject
    {
        public class ContainerMixData
        {
            public int Count
            {
                get
                {
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

        public List<LevelData> LevelDatas = new List<LevelData>();

        public void Regenerate(int index)
        {
            LevelDatas[index] = GenerateLevel(index);
        }

        public void GenerateLevels(int levelCount)
        {
            //First 100 levels

            int startIndex = LevelDatas.Count;

            for (int i = startIndex; i < startIndex+levelCount; i++)
            {
                LevelDatas.Add(GenerateLevel(i));
            }

        }

        public LevelData GenerateLevel(int index)
        {
            LevelData levelData;
            if(index < 25)
            {
                levelData = GenerateLevel(3, 10, 4);
            }
            else if(index < 50)
            {
                levelData = GenerateLevel(4, 10, 4);
            }
            else
            {
                levelData = GenerateLevel(4, 20, 8);
            }

            return levelData;
        }

        private LevelData GenerateLevel(int containersCount = 3, int goal = 10, int containerSize = 4, int secretNumbers = 0)
        {
            bool isGood = false;

            ContainerMixData[] containersForMix = new ContainerMixData[containersCount];

            int findGoodLimit = 50;
            int findGoodCount = 0;

            int maxSteps = 20;

            while (!isGood && findGoodCount < findGoodLimit)
            {
                for (int i = 0; i < containersCount - 1; i++)
                {
                    var nums = GenerateNums(goal, containerSize);

                    containersForMix[i] = new ContainerMixData();

                    for (int j = 0; j < nums.Length; j++)
                    {
                        containersForMix[i].nums.Add(nums[j]);
                    }
                }
                containersForMix[containersCount - 1] = new ContainerMixData();

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
                        || (dstNum + srcNum == goal && containersForMix[dstIdx].nums.Count + 1 == containerSize)
                        || containersForMix[dstIdx].nums.Count == containerSize)
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
                    if (cCount == goal) brokes++;
                }

                isGood = checkSum == (containersCount - 1) * goal;
                isGood &= brokes < 1;

                if (isGood)
                    maxSteps = mixIterations;

                findGoodCount++;
            }

            LevelData levelData = new LevelData();
            levelData.Containers = new ContaierData[containersCount];

            for (int i = 0; i < containersForMix.Length; i++)
            {
                levelData.Containers[i] = new ContaierData();
                for (int j = 0; j < containersForMix[i].nums.Count; j++)
                {
                    levelData.Containers[i].numbers.Add(containersForMix[i].nums[j]);
                }
            }

            levelData.Steps = maxSteps;
            levelData.Goal = goal;
            levelData.Total = goal * (containersCount - 1);
            levelData.SecretNumbers = 0;

            return levelData;

        }

        public void Clear()
        {
            LevelDatas.Clear();
        }

        private int[] GenerateNums(int goal, int containerSize)
        {
            List<int> numbers = new List<int>();

            bool badGeneration = true;

           

            while (badGeneration)
            {
                badGeneration = false;

                int firstNumber = Random.Range(1, 7);
                numbers.Add(firstNumber);

                int sumToGenerate = goal - firstNumber;
                int slotLeft = containerSize-1;

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

                    int num = Random.Range(1, Mathf.Min(sumToGenerate+1, 7));
                    sumToGenerate -= num;
                    numbers.Add(num);
                }

                if (badGeneration)
                    numbers.Clear();
            }

            return numbers.ToArray();
        }


    }
}
