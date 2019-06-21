using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }
        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }
        //first and second are for tie breaking in case for a tie. This method returns index of min
        //element otherwise
        public static int[] minIndex(int[] arr, int first, int second){
           int minInd = 0;
           int min = arr[0];
           int prevMinInd = minInd;
           int[] myMin = new int[2];
            //when tie is there
            if((first != -1) && (second != -1)){
                if(arr[first] < arr[second]){
                    myMin[0] = first; myMin[1] = first;
                }
                else if(arr[first] == arr[second]){
                    myMin[0] = -2; myMin[1] = -2;
                }
                else {
                    myMin[0] = second; myMin[1] = second;
                }
                return myMin;
            }
            //for no tie/initial case
           for(int i = 1; i < arr.Length;i++){
               if(arr[i] <= min){
                   min = arr[i];
                   prevMinInd = minInd;
                   minInd = i;
               }
           }
           //check for multiple min elements
           if(minInd==prevMinInd) {
               myMin[0] = minInd; myMin[1] = minInd;
           }
          else{
               if(arr[prevMinInd] == arr[minInd]){
               myMin[0] = prevMinInd; myMin[1] = minInd; }
               else myMin[0] = minInd; myMin[1] = minInd;
           }
           return myMin;
       }
       public static int[] maxIndex(int[] arr, int first, int second){
           int maxInd = 0;
           int max = arr[0];
           int prevMaxInd = maxInd;
           int[] myMax = new int[2];
            //when tie is there
            if((first != -1) && (second != -1)){
                if(arr[first] > arr[second]){
                    myMax[0] = first; myMax[1] = first;
                }
                else if(arr[first] == arr[second]){
                    myMax[0] = -2; myMax[1] = -2;
                }
                else {
                    myMax[0] = second; myMax[1] = second;
                }
                return myMax;
            }
           //for no tie/initial case
           for(int i = 1; i < arr.Length;i++){
               if(arr[i] >= max){
                   max = arr[i];
                   prevMaxInd = maxInd;
                   maxInd = i;
               }
           }
           //check for multiple max elements
           if(maxInd==prevMaxInd) {
               myMax[0] = maxInd; myMax[1] = maxInd;
           }
           else{
               if(arr[prevMaxInd] == arr[maxInd]){
               myMax[0] = prevMaxInd; myMax[1] = maxInd; }
               else myMax[0] = maxInd; myMax[1] = maxInd;
           }
           return myMax;
       }
       public static int[] performOp(char ch, int[] protein, int[] carbs, int[] fat,
                                            int[] calories, int first, int second){
            int[] ar = new int[2] { -1, -1};
           switch(ch){
               //comparing for every diet plan for each food constituent
               case 'C': return maxIndex(carbs, first, second);
               case 'c': return minIndex(carbs, first, second);
               case 'P': return maxIndex(protein, first, second);
               case 'p': return minIndex(protein, first, second);
               case 'F': return maxIndex(fat, first, second);
               case 'f': return minIndex(fat, first, second);
               case 'T': return maxIndex(calories, first, second);
               case 't': return minIndex(calories, first, second);
           }
           return ar;
       }
        public static int trivialOp(string DietPlan, int[] protein, int[] carbs, int[] fat,
                      int[] calories, int first, int second){
            
            int i = 0;
            int[] inds = new int[2];
            int finalInd = -1;
            if(DietPlan.Length == 0) return second;
            //to handle all tie based situations here. In event of a tie, I will deal with only 2 elements
            // and hence named trivial
             for(i = 0; i < DietPlan.Length; i++){
                inds = performOp(DietPlan[i],protein, carbs, fat, calories, first, second);
            }
            if((inds[0] == -2) && (inds[1] == -2)) finalInd = (first < second)?first:second;
            if(inds[0] != -2 && (inds[0] == inds[1])) finalInd = inds[0];
                    
            return finalInd;
        }
        public static int performOperation(string dietplan, int[] protein, int[] carbs,
                                            int[] fat, int[] calories){
           int[] indices = new int[2];
           bool isTie = false;
           int i = 0;
            int firstBest = -1; int secondBest = -1;
            //process diet plan for a single person.
            //firstBest and secondBest indicate indices which ties. In case of no ties it will run till 
            //completion.
            for(i = 0; i < dietplan.Length; i++){
                 indices = performOp(dietplan[i],protein, carbs, fat, calories, firstBest, secondBest);
                 if(indices[0] != indices[1]) isTie = true;
                 if(isTie){
                     firstBest = indices[0];
                     secondBest = indices[1];
                     break;
                 }
                 if(indices[0] == indices[1]) return indices[0];
            
            }
            int finalIndex = trivialOp(dietplan.Substring(1),protein, carbs, fat, calories, firstBest,
                                        secondBest);
            if(isTie) return finalIndex;
            return indices[0];
        }
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int k = 0;
            int index = 0;
            int[] calories = new int[carbs.Length];
            for(int i = 0; i < calories.Length; i++){
                calories[i] = 5 * (carbs[i] + protein[i]) + 9 * fat[i];
            }
            int[] mealIndices = new int[dietPlans.Length];
            for(int i = 0; i < dietPlans.Length; i++){
                    if(dietPlans[i] == "") { index = 0; }
                    else{
                    index = performOperation(dietPlans[i], protein, carbs, fat, calories);
                    }
                    mealIndices[k++] = index;
                }
            return mealIndices;
        }
    }
}
