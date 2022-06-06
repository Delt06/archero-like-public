using System;
using Leopotam.Ecs;

namespace Features.Characters.Components
{
    [Serializable]
    public struct TeamData : IEcsAutoReset<TeamData>
    {
        public Team Team;

        public void AutoReset(ref TeamData c)
        {
            c.Team = Team.Enemy;
        }
    }
}