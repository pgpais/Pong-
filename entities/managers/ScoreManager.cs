using System;
using Godot;
using Pong.Entities;

namespace Pong.Entities.Managers
{
    public class ScoreManager : Node
    {
        public static ScoreManager Instance { get; private set; }
        public int[] Score { get; private set; } = { 0, 0 };

        public ScoreManager()
        {
            if (Instance != null)
            {
                throw new Exception("ScoreManager is a singleton, cannot create more than one instance");
            }
            else
            {
                Score = new int[] { 0, 0 };
            }
        }

        public void ConnectGoalSignal(Goal goal)
        {
            goal.Connect(nameof(Goal.GoalScored), this, nameof(OnGoal));
        }

        private void OnGoal(PlayerNumber playerNumber)
        {
            AddScore(playerNumber);
        }

        void AddScore(PlayerNumber playerNumber)
        {
            Score[(int)playerNumber]++;
        }

        void ResetScore()
        {
            Score = new int[] { 0, 0 };
        }

        void SetScore(int[] score)
        {
            Score = score;
        }
    }
}