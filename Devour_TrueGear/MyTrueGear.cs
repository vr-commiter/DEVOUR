using System.Threading;
using TrueGearSDK;
using System.Linq;


namespace MyTrueGear
{
    public class TrueGearMod
    {
        private static TrueGearPlayer _player = null;

        private static ManualResetEvent heartbeatMRE = new ManualResetEvent(false);
        private static ManualResetEvent interactionMRE = new ManualResetEvent(false);
        private static ManualResetEvent flashlightfryingMRE = new ManualResetEvent(false);
        private static ManualResetEvent pauseMRE = new ManualResetEvent(true);


        public void HeartBeat()
        {
            while(true)
            {
                heartbeatMRE.WaitOne();
                pauseMRE.WaitOne();
                _player.SendPlay("HeartBeat");
                Thread.Sleep(300);
            }            
        }

        public void Interaction()
        {
            while (true)
            {
                interactionMRE.WaitOne();
                pauseMRE.WaitOne();
                _player.SendPlay("Interaction");
                Thread.Sleep(70);
            }
        }

        public void FlashLightFrying()
        {
            while (true)
            {
                flashlightfryingMRE.WaitOne();
                pauseMRE.WaitOne();
                _player.SendPlay("FlashLightFrying");
                Thread.Sleep(70);
            }
        }

        

        public TrueGearMod() 
        {
            _player = new TrueGearPlayer("1274570","Devour");
            //_player.PreSeekEffect("DefaultDamage");
            _player.Start();
            new Thread(new ThreadStart(this.HeartBeat)).Start();
            new Thread(new ThreadStart(this.Interaction)).Start();
            new Thread(new ThreadStart(this.FlashLightFrying)).Start();
        }    


        public void Play(string Event)
        { 
            _player.SendPlay(Event);
        }


        public void StartFlashLightFrying()
        {
            flashlightfryingMRE.Set();
        }

        public void StopFlashLightFrying()
        {
            flashlightfryingMRE.Reset();
        }

        public void StartHeartBeat()
        {
            heartbeatMRE.Set();
        }

        public void StopHeartBeat()
        {
            heartbeatMRE.Reset();
        }

        public void StartInteraction()
        {
            interactionMRE.Set();
        }

        public void StopInteraction()
        {
            interactionMRE.Reset();
        }

        public void UnPause()
        {
            pauseMRE.Set();
        }

        public void Pause()
        {
            pauseMRE.Reset();
        }


    }
}
