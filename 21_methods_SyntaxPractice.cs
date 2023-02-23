namespace PracticeNameSpace
{
    public class PracticeClass
    {
        public static void LikersOfYourPost()
        {
            List<string> likers = new List<string>();
            while (true)
            {
                Console.WriteLine("Enter the names of the friends who like your post!");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    break;
                likers.Add(input);
            }
            if (likers.Count > 2)
                Console.WriteLine("{0} and {1} and {2} others like your post!", likers[0], likers[1], likers.Count);
            else if (likers.Count == 2)
                Console.WriteLine("{0} and {1} like your post!", likers[0], likers[1]);
            else if (likers.Count == 1)
                Console.WriteLine("{0} likes your post!", likers[0]);
            else
                Console.WriteLine();
        }

        public static void ReversedName()
        {
            Console.WriteLine("Enter your entire name");
            var name = Console.ReadLine();

            var array = new char[name.Length];
            for (var i = name.Length; i > 0; i--)
                array[name.Length - i] = name[i - 1];
            var reversed = new string(array);
            Console.WriteLine("Reversed name: " + reversed);
        }

        public static void FiveUniques()
        {
            var uniqueNumbers = new List<int>();

            while (uniqueNumbers.Count < 5)
            {
                Console.WriteLine("Enter five unique numbers one by one");
                var input = Convert.ToInt32(Console.ReadLine());

                if (uniqueNumbers.Contains(input))
                {
                    Console.WriteLine($"You already entered number {input} Try another number");
                    continue;
                }
                uniqueNumbers.Add(input);
            }
            string[] positions = { "first", "second", "third", "fourth", "fifth" };
            uniqueNumbers.Sort();

            for (int i = 0; i < uniqueNumbers.Count; i++)
            {
                Console.Write($"The {positions[i]} number is: " + uniqueNumbers[i] + " ");
            }
        }
        public static void UniquesInInputs()
        {
            var numbers = new List<int>();
            Console.WriteLine("Keep entering numbers or enter any non-number to quit");
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    numbers.Add(Convert.ToInt32(input));
                }
                catch
                {
                    break;
                }
            }
            foreach (int num in numbers.Distinct())
            {
                Console.WriteLine(num);
            }
        }
        public static void SmallestOfList()
        {
            var listForSearch = new List<int>();
            Console.WriteLine("Input a list of numbers separated by dots e.g: 1.2.4.7.2");

            var input = Console.ReadLine().Split('.');
            foreach (string num in input)
            {
                if (num == ".")
                    break;
                listForSearch.Add(Convert.ToInt32(num));
            }

            if (listForSearch.Count < 5)
                Console.WriteLine("Invalid");

            listForSearch.Sort();
            for (int i = 0; i <= 2; i++)
            {
                Console.Write($"{listForSearch[i]}.");
            }

            //*for generic lists: DESC and ASC*
            //var ascendingOrder = li.OrderBy(i => i);  Linq without mutation
            //var descendingOrder = li.OrderByDescending(i => i); Linq without mutation

            // li = li.OrderBy(i => i).ToList(); Linq with mutation

            // li.Sort((a, b) => b.CompareTo(a)); Ascending sort that mutates the list without linq
            // li.Sort((a, b) => a.CompareTo(b)); Descending sort that mutates the list without linq


            /*you can also sort manually using two lists and some loops!
            here the lists are "numbers" "and "smallest"

            while (smallests.Count < 3)
            {
                var min = numbers[0];
                foreach (var number in numbers)
                {
                    if (number < min)
                        min = number;
                }
                smallest.Add(min);
                numbers.Remove(min);
            */

        }
        public static bool AreNumbersConsecutive(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i + 1] - nums[i] != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AreThereDoublesOf(string numbers)
        {
            if (String.IsNullOrWhiteSpace(numbers))
            {
                return false;
            }
            string[] parsedNums = numbers.Split('-');
            foreach (string num in parsedNums)
            {
                Console.WriteLine(num);
            }
            for (int i = 0; i < parsedNums.Length; i++)
            {
                for (int j = i + 1; j < parsedNums.Length; j++)
                {
                    if (parsedNums[i] == parsedNums[j])
                    {
                        Console.WriteLine("There are doubles");
                        return true;
                    }
                }
            }

            Console.WriteLine("No doubles");
            return false;
        }
        public static bool IsValidTimeFormat()
        {
            Console.WriteLine("What's the time. Can you tell me in the 24:00-format?");
            var clockInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(clockInput))
            {
                Console.WriteLine("Invalid");
                return false;
            }
            var components = clockInput.Split(':');
            if (components.Length != 2)
            {
                Console.WriteLine("Invalid");
                return false;
            }

            try
            {
                var hours = Int32.Parse(components[0]);
                var minutes = Int32.Parse(components[1]);

                if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {
                    Console.WriteLine("Ok, thank you!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid");
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid");
                return false;
            }
        }

        public static string PascalWords(string input)
        {
            var output = "";

            foreach (var word in input.Split(' '))
            {
                var pascalFirstChar = char.ToUpper(word[0]);
                var toLowerWord = word.ToLower().Substring(1);
                output += (pascalFirstChar + toLowerWord);
            }
            Console.WriteLine(output);
            return output;
        }

        public static int CountVowels(string input)
        {
            string vowels = "aeoui";
            int vowelCount = 0;

            foreach (char letter in input)
            {
                if (vowels.Contains(char.ToLower(letter)))
                    vowelCount++;
            }
            return vowelCount;
        }
        public static int CountOfTriplets()
        {
            Console.WriteLine("How many students to divide into triplets?");
            var students = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(students))
                return 0;
            int studentCount = Int32.Parse(students);
            int triplets = 0;

            for (int i = 0; i < studentCount; i++)
            {
                if (i % 3 == 0)
                    triplets++;
            }
            return triplets;
        }

        public static int SumOfNumbers(string input)
        {
            var sum = 0;
            while (true)
            {
                if (input.ToUpper() == "OK")
                    break;

                sum += Convert.ToInt32(input);
            }
            return sum;
        }

        public static int FactorialOfNumber(int num)
        {
            int factorial = 1;

            for (int i = 1; i <= num; i++)
                factorial *= i;

            return factorial;
        }

        public static int RecursiveFactorializing(int num)
        {
            if (num == 1)
                return 1;
            else
                return num * RecursiveFactorializing(num - 1);
        }

        public static bool NumberGuessing()
        {
            var secret = new Random().Next(1, 10);
            int guesses = 0;
            while (guesses < 4)
            {
                Console.WriteLine("Guess which number I'm thinking of!");
                var input = Convert.ToInt32(Console.ReadLine());
                guesses++;

                if (input == secret)
                {
                    Console.WriteLine("You won!");
                    return true;
                }
            }
            Console.WriteLine("Uii! Try again!");
            return false;
        }

        public static int FindMax(string input)
        {
            List<int> numbers = new List<int>();
            var justTheNumbers = input.Split(',');
            foreach (var number in justTheNumbers)
            {
                int num = Convert.ToInt32(number);
                numbers.Add(num);
            }
            numbers.Sort();
            numbers.Reverse();
            return numbers[0];
        }

        public static bool IsValidNumber(int num)
        {
            if (num <= 10 && num >= 1)
                return true;
            else
                return false;
        }

        public static void MaxOfNums()
        {
            Console.WriteLine("Enter a number to compare");
            int firstNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("which number to compare to?");
            int secondNum = Convert.ToInt32(Console.ReadLine());

            int max = (firstNum > secondNum) ? firstNum : secondNum;
            Console.WriteLine($"Number {max} is the maximum!");
            return;
        }

        public static void PaintingOrientationChecker()
        {
            /*Two ways to do this, with lavish if-staments, or an enum, and the ternary operator, 
            which is usually considered best practice for real world applications*/
            Console.WriteLine("Enter the width of your painting");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the height of your painting");
            int height = Convert.ToInt32(Console.ReadLine());
            /*if (width > height)
                Console.WriteLine("This is a landscape painting");
            if (height > width)
                Console.WriteLine("this is a portrait painting");
            if (height == width)
                Console.WriteLine("This is a square painting");*/
            var orientation = (width > height) ?
            PaintingOrientation.Landscape : (width == height) ?
            PaintingOrientation.Square : PaintingOrientation.Portrait;
            Console.WriteLine($"This is a {orientation} painting!");
            return;
            // The ternary operator ? checks whether the preceding logic in the paratheses() is ? true : false;
            // By adding another logic check with parentheses() into a condition, we can check for more conditions.
        }

        public enum PaintingOrientation
        {
            Landscape,
            Portrait,
            Square
        }

        /*Ouch! On your travels you were caught in a speed camera. 
        It's a demerit point for each 5km\h of speed over the speed limit.
        For 12 or more demerit points, you lose your lisence.
        Did you go fast, or did you go slow? Sometimes it doesnt' really matter.
        The speed limit is randomly set by the local authorities.*/
        public static void CaughtInASpeedCamera()
        {
            ToughCop copBob = new ToughCop();
            Console.WriteLine("How fast did you drive?");
            var fumblingWords = Console.ReadLine();
            var factualCarSpeed = Convert.ToInt32(fumblingWords);
            var theFinalJudgement = copBob.LayDownTheLaw(factualCarSpeed);
            Console.WriteLine(theFinalJudgement);
        }
        public class ToughCop
        {
            public ToughCop()
            {
                Random random = new Random();
                speedLimit = random.Next(30, 120);
            }
            private int speedLimit;
            const int excessSpeedPerDemerit = 5;
            public string LayDownTheLaw(int carSpeed)
            {
                int demerits = ((factualCarSpeed - speedLimit) / excessSpeedPerDemerit);

                if (demerits <= 0)
                    return $"The speed limit is {speedLimit} Son. You are good.";
                else if (demerits < 12)
                    return ($"The speed limit is {speedLimit} Son. {demerits} demerits");
                else
                    return ($"Son, the speed limit is {speedLimit}. That's {demerits} demerits. Your lisence, this hand, eh!.");
            }
        }
    }
}