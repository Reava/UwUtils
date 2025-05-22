
using Newtonsoft.Json.Linq;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Daily Check In")]
    public class DailyCheckin : UdonSharpBehaviour
    {
        [SerializeField] private const string scorePersistenceParameterDaily = "Reava_UwUtils_Scores_Daily";

        // There is current no check for actual streaks.
        [SerializeField] private string DisplayPrefix = "Days joined: ";
        [SerializeField] private TextMeshProUGUI currentStreak;
        [SerializeField] private string eventName = "_interact";
        [SerializeField] private UdonBehaviour[] TriggerOnFirstJoin;
        [SerializeField] private UdonBehaviour[] TriggerOnDailyReward;
        [SerializeField] private bool alsoTriggerOnSameDayRejoin = true;

        [SerializeField] private int rewardDaily = 1;

        private string dateFormat = "yyyy-MM-dd";
        private string LastJoinDateKey = "Reava_UwUtils_LastJoinDate";
        private string todayDate;
        private string lastJoinDate;

        private int currentValue = 0;

        private bool _playerRestored = false;

        void Start()
        {
            todayDate = System.DateTime.Now.ToString(dateFormat);
            SendCustomEventDelayedSeconds(nameof(_checkRestored), 5f);
        }

        public void _dailyFirstJoin()
        {
            currentValue = PlayerData.GetInt(Networking.LocalPlayer, scorePersistenceParameterDaily);
            currentValue += 1;
            PlayerData.SetInt(scorePersistenceParameterDaily, currentValue);
            _dailyRejoin();
        }

        public void _dailyRejoin()
        {
            if (currentStreak) currentStreak.text = DisplayPrefix + currentValue.ToString();
            foreach (UdonBehaviour eTarget in TriggerOnDailyReward)
            {
                if (eTarget) eTarget.SendCustomEvent(eventName);
            }
        }

        public void _checkRestored()
        {
            if (!_playerRestored)
            {
                PlayerData.SetInt(scorePersistenceParameterDaily, 1);
                foreach (UdonBehaviour eTarget in TriggerOnFirstJoin)
                {
                    if (eTarget) eTarget.SendCustomEvent(eventName);
                }
                _dailyRejoin();
            }
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (Networking.LocalPlayer != player) return;
            _playerRestored = true;
            // Get last joined
            if (!PlayerData.HasKey(player, LastJoinDateKey)) return;
            if (PlayerData.GetType(player, LastJoinDateKey) != typeof(string)) return;
            lastJoinDate = PlayerData.GetString(Networking.LocalPlayer, LastJoinDateKey);
            if (lastJoinDate != todayDate) _dailyFirstJoin();
            if (lastJoinDate == todayDate) _dailyRejoin();
        }
    }
}