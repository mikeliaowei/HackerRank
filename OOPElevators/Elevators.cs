using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPElevators
{
	public class ElevatorCar
	{
		private int currentFloor = 0;
		private CarDirection currentDirection = CarDirection.UP;
		private CarState currentState = CarState.IDLE;
	}

	public enum CarDirection
	{
		UP,
		DOWN,
		STANDING
	}

	public enum CarState
	{
		MOVING, 
		STOPPED, 
		IDLE
	}

	public class InternalRequest
	{
		private int destinationFloor;
		public InternalRequest(int destinationFloor)
		{
			this.destinationFloor = destinationFloor;
		}
		public int getDestinationFloor()
		{
			return destinationFloor;
		}
		public void setDestinationFloor(int destinationFloor)
		{
			this.destinationFloor = destinationFloor;
		}
	}

	public class ExternalRequest
	{
		private CarDirection directionToGo;
		private int sourceFloor;
		public ExternalRequest(CarDirection directionToGo, int sourceFloor)
		{
			this.directionToGo = directionToGo;
			this.sourceFloor = sourceFloor;
		}
		public CarDirection getDirectionToGo()
		{
			return directionToGo;
		}
		public void setDirectionToGo(CarDirection directionToGo)
		{
			this.directionToGo = directionToGo;
		}
		public int getSourceFloor()
		{
			return sourceFloor;
		}
		public void setSourceFloor(int sourceFloor)
		{
			this.sourceFloor = sourceFloor;
		}
	}

}
