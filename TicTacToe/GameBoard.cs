using System.Collections.Generic;
using System.Linq;

namespace TicTacToe {
    public class GameBoard {

        // TIC TAC TOE BOARD IS
        // REPRESENTED AS FOLLOWS

        //  | 0 | 1 | 2 |
        //   -----------
        //  | 3 | 4 | 5 |
        //   -----------
        //  | 6 | 7 | 8 |


        public static GameBoard CreateGame() {
            return new GameBoard();
        }

        private IList<PlaceOnTheBoard> places;
        private bool isPlayer2sTurn;

        private GameBoard() {
            initializePlaces();
        }

        private void initializePlaces() {
            places = new List<PlaceOnTheBoard>();
            for (int i = 0; i < 9; i++) {
                Places.Add(new PlaceOnTheBoard());
            }
        }

        public IList<PlaceOnTheBoard> Places {
            get { return places; }
        }

        public bool IsEmpty {
            get { return places.All(s => s.IsEmpty); }
        }

        public bool IsPlayer1sTurn() {
            return !isPlayer2sTurn;
        }

        public bool IsGameOver {
            get {
                return allPlacesAreUsed() || hasWinner();
            }
        }

        private bool allPlacesAreUsed() {
            return places.All(s => !s.IsEmpty);
        }

        public int Winner {
            get {
                if(isCatsGame()) {
                    return 0;
                }
                if (!hasWinner()) {
                    return -1;
                }
                return isPlayer2sTurn ? 1 : 2;
            }
        }

        private bool isCatsGame() {
            return allPlacesAreUsed() && !hasWinner();
        }

        private bool hasWinner() {
            return hasHorizontalWinner() || hasVerticalWinner() || hasDiagonalWinner();
        }

        private bool hasHorizontalWinner() {
            var topRowWinner = allAreTakenBySamePlayer(0, 1, 2);
            var secondRowWinner = allAreTakenBySamePlayer(3, 4, 5);
            var thirdRowWinner = allAreTakenBySamePlayer(6, 7, 8);
            return topRowWinner || secondRowWinner || thirdRowWinner;
        }

        private bool hasVerticalWinner() {
            var topColumnWinner = allAreTakenBySamePlayer(0, 3, 6);
            var secondColumnWinner = allAreTakenBySamePlayer(1, 4, 7);
            var thirdColumnWinner = allAreTakenBySamePlayer(2, 5, 8);
            return topColumnWinner || secondColumnWinner || thirdColumnWinner;
        }

        private bool hasDiagonalWinner() {
            return allAreTakenBySamePlayer(0, 4, 8)
                   || allAreTakenBySamePlayer(2, 4, 6);
        }

        public void MakePlay(int index) {
            ensureTheGameIsPlayable();
            ensurePlaceMonogomy(index);
            places[index].TakenBy = isPlayer2sTurn ? 2 : 1;
            isPlayer2sTurn = !isPlayer2sTurn;
        }

        private void ensureTheGameIsPlayable() {
            if (IsGameOver) {
                throw new GameOverException();
            }
        }

        private void ensurePlaceMonogomy(int index) {
            if (!places[index].IsEmpty) {
                throw new DuplicatePlaceAttemptedException();
            }
        }

        private bool allAreTakenBySamePlayer(int x, int y, int z) {
            if (anyPlacesAreEmpty(x, y, z)) {
                return false;
            }
            return allPlacesTakenByHoldSameValue(x, y, z);
        }

        private bool allPlacesTakenByHoldSameValue(int x, int y, int z) {
            return takenByTheSamePlayer(x, y)
                   && takenByTheSamePlayer(x, z);
        }

        private bool takenByTheSamePlayer(int x, int y) {
            return (places[x].TakenBy ?? -42) == (places[y].TakenBy ?? -167);// random illogical variables for null coalescing 
        }

        private bool anyPlacesAreEmpty(int x, int y, int z) {
            return places[x].IsEmpty
                   || places[y].IsEmpty
                   || places[z].IsEmpty;
        }
    }
}