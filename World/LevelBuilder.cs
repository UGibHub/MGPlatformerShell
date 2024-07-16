using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class LevelBuilder {
    private int _xPos;
    private int _yPos;
    private bool _collideable;
    private Texture2D _texture;

    public LevelBuilder() {
    }


    public void BuildLevel(Dictionary<string, Dictionary<string, string>> TileDict, List<Component> components, ContentManager content) {
        var tileLoad = TileDict["tileProperties"]["TileNames"].Split(" ");
        Dictionary<string, Texture2D> TextureDict = new();
        foreach (var item in tileLoad) {
            if (item != ""){
                TextureDict[item] = content.Load<Texture2D>(TileDict["tileProperties"][item + "Path"]);
            }
        }

        foreach (var (key, value) in TileDict) {
            if(key == "mapInfo" || key == "tileProperties") {
                continue;
            }
            foreach (var (key2, value2) in value) {
                switch (key2) {
                    case "Texture":
                        _texture = TextureDict[value2];
                        break;
                    case "IsCollideable":
                        if (value2 == "true") {
                            _collideable = true;
                        } else if (value2 == "false") {
                            _collideable = false;
                        }
                        break;
                    case "X":
                        _xPos = Convert.ToInt32(value2);
                        break;
                    case "Y":
                        _yPos = Convert.ToInt32(value2);
                        break;
                }
            }
            
            var tile = new Tile(_texture, _collideable, _xPos, _yPos);
            components.Add(tile);
        }
    }

}