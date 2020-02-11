using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Chess.Configuration
{
    [Serializable]
    [XmlRoot(ElementName = "Selection")]
    public class Selection
    {
        [XmlAttribute(AttributeName = "row")]
        public int Row { get; set; }
        [XmlAttribute(AttributeName = "column")]
        public int Column { get; set; }
    }


    [Serializable]
    [XmlRoot(ElementName = "Piece")]
    public class Piece
    {
        [XmlElement(ElementName = "Position")]
        public Selection Position { get; set; }
        [XmlElement(ElementName = "LastPosition")]
        public Selection LastPosition { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "player")]
        public string Player { get; set; }
    }


    [Serializable]
    [XmlRoot(ElementName = "Move")]
    public class Move
    {
        [XmlElement(ElementName = "Piece")]
        public Piece Piece { get; set; }
        [XmlElement(ElementName = "FromPosition")]
        public Selection FromPosition { get; set; }
        [XmlElement(ElementName = "ToPosition")]
        public Selection ToPosition { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "LastMove")]
    public class LastMove
    {
        [XmlElement(ElementName = "Position")]
        public Selection Position { get; set; }
        [XmlAttribute(AttributeName = "player")]
        public string Player { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "LastMoves")]
    public class LastMoves
    {
        [XmlElement(ElementName = "LastMove")]
        public List<LastMove> List { get; set; }
    }


    [Serializable]
    [XmlRoot(ElementName = "Moves")]
    public class Moves
    {
        [XmlElement(ElementName = "Move")]
        public List<Move> List { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "Pieces")]
    public class Pieces
    {
        [XmlElement(ElementName = "Piece")]
        public List<Piece> List { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "Game")]
    public class Game
    {
        [XmlElement(ElementName = "Selection")]
        public Selection Selection { get; set; }

        [XmlElement(ElementName = "LastMoves")]
        public LastMoves LastMoves { get; set; }

        [XmlElement(ElementName = "Moves")]
        public Moves Moves { get; set; }
        
        [XmlElement(ElementName = "Pieces")]
        public Pieces Pieces { get; set; }
        
        [XmlAttribute(AttributeName = "players")]
        public int NumberOfPlayers { get; set; }
        
        [XmlAttribute(AttributeName = "turn")]
        public string Turn { get; set; }
        
        [XmlAttribute(AttributeName = "whiteTime")]
        public long WhiteTime { get; set; }
        
        [XmlAttribute(AttributeName = "blackTime")]
        public long BlackTime { get; set; }
        

        public override string ToString()
        {
            var serailizer = new XmlSerializer(GetType());

            //Add an empty namespace and empty value
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            string xml;

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    serailizer.Serialize(writer, this, ns);
                    xml = sww.ToString(); 
                }
            }
            return xml;
        }
    }
}
