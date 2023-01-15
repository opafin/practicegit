namespace GitTest
{
    class PracticeClass
    {

        /*Ouch! On your travels you were caught by a speed camera. 
        It's a demerit point for each 5km\h of speed over the speed limit.
        For 12 or more demerit points, you lose your lisence.
        Did you go fast, or did you go slow? Sometimes it doesnt' really matter.
        The speed limit is randomly set by the local authorities.*/

        public static void SpeedCamera()
        {
            ToughCop copMiksu = new ToughCop();
            Console.WriteLine("How fast did you drive?");
            copMiksu.carSpeed = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(copMiksu.LayDownTheLaw());
        }

        public class ToughCop
        {

            public ToughCop()
            {
                Random random = new Random();
                speedLimit = random.Next(30, 120);
            }
            public int speedLimit { get; }
            public int carSpeed
            { get; set; }
            const int excessSpeedPerDemerit = 5;


            public string LayDownTheLaw()
            {
                int demerits = ((carSpeed - speedLimit) / excessSpeedPerDemerit);

                if (demerits <= 0)
                    return $"The speed limit is {speedLimit} Son. You are good.";

                else if (demerits < 12)
                    return ($"The speed limit is {speedLimit} Son. {demerits} demerits");
                else
                    return ($"Son, the speed limit is {speedLimit}. That's {demerits} demerits. Slap that lisence right into this hand, eh!.");

            }

        }
    }
}

