
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
        [SerializeField] private bool trackDayCount = true;
        [SerializeField] private string DisplayPrefix = "Days joined: ";
        [SerializeField] private TextMeshProUGUI currentStreak;
        [SerializeField] private string eventName = "_interact";
        [Header("Events on first join of the day")]
        [SerializeField] private UdonBehaviour[] TriggerOnFirstJoin;
        [SerializeField] private GameObject[] toggleOnFirstJoin;
        [SerializeField] private bool alsoTriggerOnSameDayRejoin = true;

        [SerializeField] private int rewardDaily = 1;

        private string dateFormat = "yyyy-MM-dd";
        private string LastJoinDateKey = "Reava_UwUtils_LastJoinDate";
        private string todayDate;
        private string lastJoinDate;

        private int currentValue = 0;

        private bool _playerRestored = false;

        [HideInInspector] public bool wasFirstDailyJoin = false;

        void Start()
        {
            todayDate = System.DateTime.Now.ToString(dateFormat);
            SendCustomEventDelayedSeconds(nameof(_checkRestored), 5f);
        }

        public bool _isFirstDailyJoin()
        {
            return wasFirstDailyJoin;
        }

        public void _dailyFirstJoin()
        {
            currentValue = PlayerData.GetInt(Networking.LocalPlayer, scorePersistenceParameterDaily);
            currentValue += 1;
            PlayerData.SetInt(scorePersistenceParameterDaily, currentValue);
            foreach (UdonBehaviour eTarget in TriggerOnFirstJoin)
            {
                if (eTarget) eTarget.SendCustomEvent(eventName);
            }
            foreach (GameObject o in toggleOnFirstJoin)
            {
                if (o) o.SetActive(!o.activeSelf);
            }
            _dailyRejoin();
        }

        public void _dailyRejoin()
        {
            if (currentStreak) currentStreak.text = DisplayPrefix + currentValue.ToString();
        }

        public void _checkRestored()
        {
            if (!_playerRestored)
            {
                _dailyRejoin();
                if (!trackDayCount) return;
                PlayerData.SetInt(scorePersistenceParameterDaily, 1);
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
            if(alsoTriggerOnSameDayRejoin || lastJoinDate != todayDate) _dailyFirstJoin(); wasFirstDailyJoin = true;
            if (lastJoinDate == todayDate) _dailyRejoin();
        }
    }
}