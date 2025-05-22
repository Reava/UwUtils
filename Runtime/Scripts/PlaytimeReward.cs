
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
    [AddComponentMenu("UwUtils/Playtime Reward")]
    public class PlaytimeReward : UdonSharpBehaviour
    {
        [SerializeField] private string scorePersistenceParameter = "Reava_UwUtils_Scores_Minutes";
        private int currentValue = 0;
        [SerializeField] private int rewardOnJoin = 0;
        [SerializeField] private int rewardPerCall = 1;
        [SerializeField] private int loopLengthSeconds = 60;
        public TextMeshProUGUI currentScore;
        [SerializeField] UdonBehaviour LeaderBoardRef;

        private bool _playerRestored = false;

        void Start()
        {
            _addPoints(rewardOnJoin);
            SendCustomEventDelayedSeconds(nameof(_minutePassed), loopLengthSeconds);
        }

        private void _minutePassed()
        {
            Debug.Log("[Reava_/UwUtils/PlaytimeReward.cs]: A Minute passed!", gameObject);
            _addPoints(rewardPerCall);
            SendCustomEventDelayedSeconds(nameof(_minutePassed), loopLengthSeconds);
            if (LeaderBoardRef != null) LeaderBoardRef.SendCustomEvent("RefreshLeaderboard");
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (Networking.LocalPlayer != player) return;
            _playerRestored = true;
            // Get score
            if (!PlayerData.HasKey(player, scorePersistenceParameter)) return;
            if (PlayerData.GetType(player, scorePersistenceParameter) != typeof(int)) return;
            int recoveredValue = PlayerData.GetInt(Networking.LocalPlayer, scorePersistenceParameter);
            Debug.Log("[Reava_/UwUtils/PlaytimeReward.cs]: Player restored. Recovered Value: " + recoveredValue + "| Current Value:" + currentValue + "", gameObject);
            currentValue += recoveredValue;
            if (currentScore) currentScore.text = "Score: " + currentValue.ToString();
        }

        public void _addPoints(int value)
        {
            if(value == 0) value = rewardPerCall;
            currentValue = PlayerData.GetInt(Networking.LocalPlayer, scorePersistenceParameter);
            currentValue += value;
            PlayerData.SetInt(scorePersistenceParameter, currentValue);
            if(currentScore) currentScore.text = "Score: "+currentValue.ToString();
            Debug.Log("[Reava_/UwUtils/PlaytimeReward.cs]: Added " + value + " points ! Total:" + currentValue, gameObject);
        }
    }
}