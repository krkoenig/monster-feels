# Monster have Feelings Too!

This is the GitHub repository for the game, Monsters have Feelings Too!. Included are instructions on how to use various aspects, objects, and scripts when building for this project in Unity.

## Using the Tilemap [Still in Progress!]

The tilemap is a single script attached to an empty game object in each scene. When Creating a new level or game scene that uses a tilemap follow these steps.

1. Create an empty game object and name it "TileMap".
2. Drag the tilemap script to the new "TileMap" object. 
3. Enter a width and height in tiles for the tilemap. 
4. Drag over a tiledata source for the tilemap. * This will be a CSV with matching dimensions as the tilemap containing data regarding terrain information.
5. Create a material and assign it a texture. * This will be the background drawn for the map. ** It is a good idea to make sure that the drawn texture matches the dimensions of the tilemap (64 X Height X Width).
6. Drag the material to the TileMap's Mesh Renderer.

The TileMap should now render correctly every time the game is run.

### Notes
The tilemap's (0,0) is it's bottom left hand corner.