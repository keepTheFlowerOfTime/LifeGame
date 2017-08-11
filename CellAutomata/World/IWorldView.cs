using MyLib.CellAutomata.World.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/26 11:47:55
*
*/
namespace MyLib.CellAutomata.World
{
    public interface IWorldView
    {
        SpaceView this[int x,int y]
        {
            get;
        }

        SpaceView this[RoomPoint loc]
        {
            get;
        }

        int Width
        {
            get;
        }

        int Height
        {
            get;
        }

        bool IsStart
        {
            get;
        }
    }
}
