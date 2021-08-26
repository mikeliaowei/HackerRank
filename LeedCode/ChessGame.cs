using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    public class ChessGame
    {

        private InputState inputState;

        public ChessGame()
        {
            // user needs to enter first "Board" or "Display"
            this.inputState = new InformationInputState(this);
        }

        public void setState(InputState inputState)
        {
            this.inputState = inputState;
        }

        public void play(String input)
        {
            inputState.execute(input);
        }
    }

    public interface InputState
    {
        void execute(String input);
    }

    public class InformationInputState : InputState
    {

        private ChessGame chessGame;

        // constructor
        public InformationInputState(ChessGame chessGame)
        {
            this.chessGame = chessGame;
        }

        public void execute(String input)
        {
            if (input.Equals("Board"))
            {
                // ..
                chessGame.setState(new MoveInputState(chessGame));
            } else if (input.Equals("Display"))
            {
                // ..
                chessGame.setState(new MoveInputState(chessGame));
            }
        }
    }

    public class MoveInputState : InputState
    {

        private ChessGame chessGame;

        // constructor
        public MoveInputState(ChessGame chessGame)
		{
            this.chessGame = chessGame;
		}

        public void execute(String input)
        {
            if (input.Equals("Move"))
            {
                chessGame.setState(new InformationInputState(chessGame));
            }
        }
    }
}


