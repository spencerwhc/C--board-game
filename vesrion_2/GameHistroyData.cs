using System;
namespace vesrion2
{
    public class GameHistroyData
    {
        //Singleton pattern
        private static GameHistroyData instance = null;
        public static GameHistroyData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameHistroyData();
                }

                return instance;
            }
        }

        private GameHistroyData() { }

        //Use this to stroe game histroy file lines;
        public string[,] oldData = new string[100, 100];
    }
}
