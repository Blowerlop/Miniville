public abstract class Die
{
    private int _nbrFaces = 6;
    private static int face;

    private static Random random = new Random();

    /*
    public override static string ToString()
    {
        string toString = $"Face actuelle : {face}";
        return toString;
    }
    */

    public static int Roll(int nbrDeDés)
    {
        face = 0;
        int rollResult;

        for (int i = 0; i < nbrDeDés; i++)
        {
            rollResult = random.Next(1, 7);
            //Console.WriteLine(rollResult);

            face += rollResult;
        }

        ShowCurrentFace();
        return face;
    }


    private static void ShowCurrentFace()
    {
        Console.WriteLine($"Roll : {face}");
    }
}
