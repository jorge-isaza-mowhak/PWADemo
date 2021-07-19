using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWADemoFrameWork5.Models
{
    public class StateContainer
    {

        private static string appIsOnline;

        public static string AppIsOnline
        {
            get => appIsOnline;
            set
            {
                appIsOnline = value;
                NotifyStateChanged();
            }
        }

        public static event Action OnChange;

        private static void NotifyStateChanged() => OnChange?.Invoke();

    }
}
