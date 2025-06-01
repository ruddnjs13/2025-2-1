using System.Collections.Generic;

namespace _01.Code.Tower
{
    public interface ITargeting
    {
        Enemies.Enemy FindTarget(List<Enemies.Enemy> enemies);
    }
}