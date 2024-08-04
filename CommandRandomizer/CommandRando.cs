using KHMI;
using KHMI.Types;

namespace CommandRandomizer
{
    public class CommandRando : KHMod
    {
        public CommandRando(ModInterface mi) : base(mi)
        {
            if (!isDivingInHeart())
            {
                shuffleCommands();
            }
        }

        public override void roomUpdate(Room newRoom)
        {
            if(!isDivingInHeart())
            {
                shuffleCommands();
            }
        }

        private bool isDivingInHeart()
        {
            return World.Current(modInterface.dataInterface).WorldID == 0;
        }

        private void shuffleCommands(int commandCount=0x7C)
        {
            KHCommand[] originalOrder = new KHCommand[commandCount];
            for(int i = 0; i<originalOrder.Length; i++)
            {
                originalOrder[i] = KHCommand.FromID(modInterface.dataInterface, i);
            }
            KHCommand[] newOrder = new KHCommand[originalOrder.Length];
            Array.Copy(originalOrder, newOrder, newOrder.Length);
            Random r = new Random();
            r.Shuffle(newOrder);
            for(int i = 0; i<newOrder.Length; i++)
            {
                originalOrder[i].ActionID = newOrder[i].ActionID;
                originalOrder[i].CommandCode = newOrder[i].CommandCode;
                originalOrder[i].NextMenuID = newOrder[i].NextMenuID;
            }
        }
    }
}
