using MyMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/7/1 11:14:20
*
*/
namespace MyLib.CellAutomata.World.Room
{
    public interface IRoom
    {
        IEnumerable<Space> GetNeighbors(RoomPoint loc);
            
        RoomPointF CreateRoomPointF();

        RoomPoint CreateRoomPoint();

        /// <summary>
        /// 在Room的loc位置创建一个新的space，如果原本已经存在Space，则会覆盖之
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        Space CreateSpace(RoomPoint loc);

        int Count
        {
            get;
        }

        int Dimension
        {
            get;
        }

        Space this[RoomPoint loc]
        {
            get;
        }

        Vector Properties
        {
            get;
        }

        SpaceCollection Spaces
        {
            get;
        }
    }
}
