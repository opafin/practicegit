namespace PracticeGit
{
    class PracticeClass
    {
        /*Ouch! On your travels you were caught by a speed camera. 
        It's a demerit point for each 5km\h of speed over the speed limit.
        For 12 or more demerit points, you lose your lisence.
        Did you go fast, or did you go slow? Sometimes it doesnt' really matter.
        The speed limit is randomly set by the local authorities.*/

        public static void CaughtInASpeedCamera()
        {
            ToughCop copBob = new ToughCop();
            Console.WriteLine("How fast did you drive?");
            var theFinalJudgement = copBob.LayDownTheLaw(Convert.ToInt32(Console.ReadLine()));
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
                int demerits = ((carSpeed - speedLimit) / excessSpeedPerDemerit);

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

