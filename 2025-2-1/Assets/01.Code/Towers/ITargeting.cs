using System.Collections.Generic;
using Code.Enemies;

namespace Code.Towers
{
    public interface ITargeting
    {
        Enemy FindTarget(List<Enemies.Enemy> enemies);
    }
}