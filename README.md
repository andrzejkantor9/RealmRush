### Project Information
Simple tower defense game. Player starts with 250 gold, enemies try to get from starting to finish gate. Killing enemy grants 25 gold, placing turret uses 75 gold, enemy reaching finish gate removes 25 gold. 
Enemies are object-pooled and their hit points increase by 1 each time they die and they respawn at starting gate with a delay indefinitely. When gold goes below 0 player loses and scene is reloaded. Enemies move via pathfinding path - player can place turrets to change their path, but cannot block their path.

- [Image album]()
- [Executable build](https://drive.google.com/file/d/1ZCls-VWJnf8OkeBVj4300zni7-AceVNE/view?usp=sharing)

### Input Information
Input | Action
--- | ---
LMB |  place turret

### Features
+ currency
+ placing turrets, infinite enemy waves
+ object-pooled enemies, enemy hit points increasing every death
+ enemies moving via pathfinding, readjusting for newly placed turrets

### Limitations
+ Alt + F4 to quit game
+ no audio

### Course link
- [Course link](https://www.gamedev.tv/p/complete-c-unity-game-developer-3d-online-course-2020/?coupon_code=HOORAY)
