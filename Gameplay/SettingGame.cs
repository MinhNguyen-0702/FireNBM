using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class SettingGame : Singleton<SettingGame>
    {
        private TypeRaceRTS m_playerRace;
        private float m_timeLeft;

        public SettingGame() {}

        public float        FunGetTimeLeft()                            => m_timeLeft;
        public TypeRaceRTS  FunGetRacePlayer()                          => m_playerRace;
        public void         FunSetTimeLeft(float timeleft)              => m_timeLeft = timeleft;
        public TypeRaceRTS  FunGetRaceOpponent(TypeRaceRTS racePlayer)  => (racePlayer == m_playerRace) ? TypeRaceRTS.Zerg : m_playerRace;
        public void         FunSetRacePlayer(TypeRaceRTS racePlayer)    => m_playerRace = racePlayer;
    }
} 