using System.Collections.Generic;
using _01.Code.Enemies;

namespace _01.Code.Tower
{
    public interface ITargeting
    {
        Enemy FindTarget(List<Enemies.Enemy> enemies);
    }
}