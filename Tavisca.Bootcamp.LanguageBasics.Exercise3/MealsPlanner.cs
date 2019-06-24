using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public  class MealsPlanner
    {
        public static int[] GetMeals(int[] protein, int[] carbs, int[] fat,int[] calories,
                                     string[] dietPlans)
        {
            int[] mealPlan = new int[dietPlans.Length];

            for(int i = 0; i < dietPlans.Length; i++){
                string dietPlan = dietPlans[i];

                if(dietPlan.Length == 0){
                    mealPlan[i] = 0;
                    continue;
                }

                mealPlan[i] = ProcessDietPlan(dietPlan, protein, carbs, fat, calories);

            }
            return mealPlan;
        }
        public static int ProcessDietPlan(string dietPlan, int[] protein,
                                           int[] carbs, int[] fat, int[] calories)
        {
            var indices = new List<int>();
            for (int j = 0; j < protein.Length; j++) indices.Add(j);
            int max, min;
                foreach (char ch in dietPlan) {
                    switch(ch) {
                        case 'P':
                            max = FindMax(protein, indices);
                            indices = FindRequiredIndices(protein, indices, max);
                            break;
                        case 'p':
                            min = FindMin(protein, indices);
                            indices = FindRequiredIndices(protein, indices, min);
                            break;
                        case 'C':
                            max = FindMax(carbs, indices);
                            indices = FindRequiredIndices(carbs, indices, max);
                            break;
                        case 'c':
                            min = FindMin(carbs, indices);
                            indices = FindRequiredIndices(carbs, indices, min);
                            break;
                        case 'F':
                            max = FindMax(fat, indices);
                            indices = FindRequiredIndices(fat, indices, max);
                            break;
                        case 'f':
                            min = FindMin(fat, indices);
                            indices = FindRequiredIndices(fat, indices, min);
                            break;
                        case 'T':
                            max = FindMax(calories, indices);
                            indices = FindRequiredIndices(calories, indices, max);
                            break;
                        case 't':
                            min = FindMin(calories, indices);
                            indices = FindRequiredIndices(calories, indices, min);
                            break;
                    }
                }
            return indices[0];
        }

         public static List<int> FindRequiredIndices(int[] arr, List<int> indicesArg, int element) {
            List<int> indices = new List<int>();
            foreach (int i in indicesArg) {
                if (arr[i] == element) indices.Add(i);
            }
            return indices;
        }

        public static int FindMax(int[] arr, List<int> indices) {
            if (indices.Count == 1) return arr[indices[0]];
            int max = arr[indices[0]];
            for (int i = 1; i < indices.Count; i++) {
                if (arr[indices[i]] > max) max = arr[indices[i]];
            }
            return max;
        }
        public static int FindMin(int[] arr, List<int> indices) {
            if (indices.Count == 1) return arr[indices[0]];
            int min = arr[indices[0]];
            for (int i = 1; i < indices.Count; i++) {
                if (arr[indices[i]] < min) min = arr[indices[i]];
            }
            return min;
        }
    }
}