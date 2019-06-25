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
            Func<int, int, int> myMax = (x,y) => { 
                                                  int temp = ( x > y)? x : y; 
                                                    return temp;
                                                 };
            Func<int, int, int> myMin = (x,y) => { 
                                                  int temp = ( x < y)? x : y; 
                                                    return temp;
                                                 };
            int max, min;
                foreach (char nutrient in dietPlan) {
                    switch(nutrient) {
                        case 'P':
                            max = FindMinMax(protein, indices, myMax);
                            indices = FindRequiredIndices(protein, indices, max).ToList();
                            break;
                        case 'p':
                            min = FindMinMax(protein, indices, myMin);
                            indices = FindRequiredIndices(protein, indices, min).ToList();
                            break;
                        case 'C':
                            max = FindMinMax(carbs, indices, myMax);
                            indices = FindRequiredIndices(carbs, indices, max).ToList();
                            break;
                        case 'c':
                            min = FindMinMax(carbs, indices, myMin);
                            indices = FindRequiredIndices(carbs, indices, min).ToList();
                            break;
                        case 'F':
                            max = FindMinMax(fat, indices, myMax);
                            indices = FindRequiredIndices(fat, indices, max).ToList();
                            break;
                        case 'f':
                            min = FindMinMax(fat, indices, myMin);
                            indices = FindRequiredIndices(fat, indices, min).ToList();
                            break;
                        case 'T':
                            max = FindMinMax(calories, indices, myMax);
                            indices = FindRequiredIndices(calories, indices, max).ToList();
                            break;
                        case 't':
                            min = FindMinMax(calories, indices, myMin);
                            indices = FindRequiredIndices(calories, indices, min).ToList();
                            break;
                    }
                }
            return indices[0];
        }

         public static IEnumerable<int> FindRequiredIndices(int[] arr, List<int> indicesArg, int element) {
            foreach (int i in indicesArg) {
                if (arr[i] == element) yield return i;
            }
        }
        public static int FindMinMax(int[] arr, List<int> indices, Func<int,int,int> operation){
            if(indices.Count == 1) return arr[indices[0]];
            int element = arr[indices[0]];
            for(int i = 1; i < indices.Count; i++){
                element = operation(arr[indices[i]], element);
            }
            return element;
        }
    }
}