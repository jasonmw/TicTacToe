using System.Linq;
using Xunit;

namespace TicTacToe {
    public class TicTacToeTests {


        // TIC TAC TOE BOARD IS
        // REPRESENTED AS FOLLOWS

        //  | 0 | 1 | 2 |
        //   -----------
        //  | 3 | 4 | 5 |
        //   -----------
        //  | 6 | 7 | 8 |


        [Fact]
        public void CreateGame_WhenGameIsCreated_BoardIsNotNull() {
            var board = GameBoard.CreateGame();
            Assert.NotNull(board);
        }

        [Fact]
        public void CreateGame_WhenGameIsCreated_BoardIsEmpty() {
            var board = GameBoard.CreateGame();
            Assert.True(board.IsEmpty);
        }

        [Fact]
        public void CreateGame_WhenGameIsCreated_ItHas9Places() {

            var board = GameBoard.CreateGame();
            Assert.Equal(9, board.Places.Count());
        }

        [Fact]
        public void CreateGame_WhenGameIsCreated_ItHas9EmptyPlaces() {
            var board = GameBoard.CreateGame();
            Assert.Equal(9, board.Places.Count());
            foreach (var place in board.Places) {
                Assert.True(place.IsEmpty);
            }
        }

        [Fact]
        public void CreateGame_WhosTurnIsIt_ReturnsPlayer1() {
            var board = GameBoard.CreateGame();
            Assert.True(board.IsPlayer1sTurn());
        }
        [Fact]
        public void AfterOneMove_WhosTurnIsIt_ReturnsPlayer2() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            Assert.False(board.IsPlayer1sTurn());
        }

        [Fact]
        public void MakeMove_AfterFirstPlayerMoves_ItHas8EmptySpaces() {
            var board = GameBoard.CreateGame();
            Assert.Equal(9, board.Places.Count());

            board.MakePlay(2);

            int emptyCount = 0;
            foreach (var place in board.Places) {
                if (place.IsEmpty) {
                    emptyCount++;
                }
            }
            Assert.Equal(8, emptyCount);
        }

        [Fact]
        public void MakeMove_IfMoveIsMadeOnNonEmptyCell_ThrowsDuplicatePlaceAttemptedException() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            Assert.Throws<DuplicatePlaceAttemptedException>(() => board.MakePlay(0));
        }

        [Fact]
        public void MakeMove_IfMoveIsMadeOnDifferentNonEmptyCell_ThrowsDuplicatePlaceAttemptedException() {
            var board = GameBoard.CreateGame();
            board.MakePlay(5);
            Assert.Throws<DuplicatePlaceAttemptedException>(() => board.MakePlay(5));
        }

        [Fact]
        public void MakeSecondMove_WhenMovesIsMade_FirstMoveIsMadeByPlayer1() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(1);
            Assert.Equal(1, board.Places[0].TakenBy ?? 0);
        }

        [Fact]
        public void MakeSecondMove_When2MovesAreMade_SecondMoveIsMadeByPlayer2() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(1);
            Assert.Equal(2, board.Places[1].TakenBy ?? 0);
        }

        [Fact]
        public void MakeMove_AfterOneMove_TheGameIsNotOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            Assert.False(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithTopRow_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(3);
            board.MakePlay(1);
            board.MakePlay(4);
            board.MakePlay(2);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithSecondRow_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(3);
            board.MakePlay(0);
            board.MakePlay(4);
            board.MakePlay(1);
            board.MakePlay(5);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithThirdRow_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(6);
            board.MakePlay(0);
            board.MakePlay(7);
            board.MakePlay(1);
            board.MakePlay(8);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithFirstColumn_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(2);
            board.MakePlay(3);
            board.MakePlay(1);
            board.MakePlay(6);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithSecondColumn_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(1);
            board.MakePlay(2);
            board.MakePlay(4);
            board.MakePlay(6);
            board.MakePlay(7);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithThirdColumn_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(2);
            board.MakePlay(3);
            board.MakePlay(5);
            board.MakePlay(1);
            board.MakePlay(8);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithThirdColumnAndAnotherPlayIsMade_ThrowsGameOverException() {
            var board = GameBoard.CreateGame();
            board.MakePlay(2);
            board.MakePlay(3);
            board.MakePlay(5);
            board.MakePlay(1);
            board.MakePlay(8);
            Assert.Throws<GameOverException>(() => board.MakePlay(0));
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithDiagonal_0_8_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(3);
            board.MakePlay(4);
            board.MakePlay(1);
            board.MakePlay(8);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithDiagonal_2_5_TheGameIsOver() {
            var board = GameBoard.CreateGame();
            board.MakePlay(2);
            board.MakePlay(3);
            board.MakePlay(4);
            board.MakePlay(1);
            board.MakePlay(6);
            Assert.True(board.IsGameOver);
        }

        [Fact]
        public void Make5Moves_Player1HasWonWithDiagonal_2_5_Player1IsTheWinner() {
            var board = GameBoard.CreateGame();
            board.MakePlay(2);
            board.MakePlay(3);
            board.MakePlay(4);
            board.MakePlay(1);
            board.MakePlay(6);
            Assert.Equal(1, board.Winner);
        }

        [Fact]
        public void Make5Moves_NoWinnerYet_TryToGetWinner_None() {
            var board = GameBoard.CreateGame();
            board.MakePlay(2);
            board.MakePlay(3);
            board.MakePlay(4);
            board.MakePlay(1);
            board.MakePlay(0);
            Assert.Equal(-1, board.Winner);
        }

        [Fact]
        public void Make9Moves_NoWinner_CatsGameWinnerIs0() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(1);
            board.MakePlay(2);
            board.MakePlay(4);
            board.MakePlay(3);
            board.MakePlay(5);
            board.MakePlay(7);
            board.MakePlay(6);
            board.MakePlay(8);
            Assert.True(board.IsGameOver);
            Assert.Equal(0, board.Winner);
        }

        [Fact]
        public void Make9Moves_WinnerOnLastMove_HasWinner() {
            var board = GameBoard.CreateGame();
            board.MakePlay(0);
            board.MakePlay(1);
            board.MakePlay(2);
            board.MakePlay(4);
            board.MakePlay(3);
            board.MakePlay(5);
            board.MakePlay(7);
            board.MakePlay(8);
            board.MakePlay(6);
            Assert.True(board.IsGameOver);
            Assert.Equal(1, board.Winner);
        }

    }
}
