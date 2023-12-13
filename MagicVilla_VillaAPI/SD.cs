namespace MagicVilla_VillaAPI
{
    public static class SD
    {
        static SD()
        {
            DealthyHalloRace = new Dictionary<string, int>();
            DealthyHalloRace.Add(Wand, 0);
            DealthyHalloRace.Add(Stone, 0);
            DealthyHalloRace.Add(Cloak, 0);
        }


        public const string Wand = "Wand";
        public const string Stone = "Stone";
        public const string Cloak = "Cloak";

        public static Dictionary<string, int> DealthyHalloRace;
    }
}
