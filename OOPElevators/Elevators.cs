using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPElevators
{
	public class ElevatorCar
	{
		private int minFloor;
		private int maxFloor;
		private int maxLoadInKgs;
		private int currentLoadInKgs;
		private bool doorClosed;
		private CarDirection direction;
	}

	enum CarDirection
	{
		UP,
		DOWN,
		STANDING
	}
}
