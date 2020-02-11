using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Properties;

namespace Chess.Model
{
    public class ImageProvider
    {
        public Dictionary<Player,Dictionary<PieceType, Image>> Pieces { get; private set; }
        public Dictionary<Player, Image> TurnIndicator { get; private set; }

        public ImageProvider()
        {
            Load();   
        }

        /// <summary>
        /// Load the graphices from the resources
        /// </summary>
        private void Load()
        {
            TurnIndicator = new Dictionary<Player, Image>();
            TurnIndicator[Player.White] = new Bitmap(Resources.TurnWhite);
            TurnIndicator[Player.Black] = new Bitmap(Resources.TurnBlack);

            Pieces = new Dictionary<Player, Dictionary<PieceType, Image>>();
            foreach (Player pl in Enum.GetValues(typeof(Player)))
            {
                Pieces[pl] = new Dictionary<PieceType, Image>();

                foreach (PieceType p in Enum.GetValues(typeof(PieceType)))
                {
                    string file = string.Empty;

                    switch (pl)
                    {
                        case Player.Black:
                            file += "Black";
                            break;
                        case Player.White:
                            file += "White";
                            break;
                    }

                    switch (p)
                    {
                        case PieceType.Pawn:
                            file += "Pawn";
                            break;
                        case PieceType.Rook:
                            file += "Rook";
                            break;
                        case PieceType.Knight:
                            file += "Knight";
                            break;
                        case PieceType.Bishop:
                            file += "Bishop";
                            break;
                        case PieceType.Queen:
                            file += "Queen";
                            break;
                        case PieceType.King:
                            file += "King";
                            break;
                        case PieceType.None:
                            continue;
                    }


                    Pieces[pl][p] = (Bitmap)Resources.ResourceManager.GetObject(file); 
                }
            }
        }
    }
}
