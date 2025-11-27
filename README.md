Jonah Gibson 100910759
Zachary O'Brien 100909491

proof of progression and Gantt chart in Gantt.Gantt
this file can be read at https://www.onlinegantt.com/#/gantt or as an alternative there is a image in the google doc

Progress demo video:https://drive.google.com/file/d/107VFRAHbSe8uG9Qwk37cU0ynMAhWDKnP/view?usp=sharing
Final demo video: https://drive.google.com/file/d/1eXdg8l1DSNZ7S9xDPaRgIv9jdYG0K3HG/view?usp=sharing 

Team formation

Our team was formed because I (Jonah) was the only member of my GDW group in the class and I didn’t want to start a new project. Zach and I have worked together in the past on multiple projects and have found we work well together, and being roommates would make coordination much easier. At first we discussed whether we would make tools for one of our GDW games or if we were going to work on something original. Ultimately we realized that we had a finished, albeit very rough, game made for a game jam last year and decided we would work on updating it using the tools learned in class.

Zach:
Factory pattern implementations
Enemy spawning
Pick up spawning
DLL/file reading implementation
Observer
Dirty Flag

Jonah:
Singleton implementations
Game manager
UI manager
Command pattern implementation
Rebindable inputs
Text commands
State machine
Object pool

Zach contributions: 50%
Jonah contributions: 50%

Project explanation

Bubble Wrap was an old game jam project that was inspired by the release of Animal Crossing and Doom. Its a violent horde shooter where you need to survive for as long as possible while waves of rabbits come and attack you. The player should use their bubble gun to trap the rabbits and claim the loot to increase the strength of your character. The player spawns into a field with a gun and waves of rabbits approaching. Shooting the rabbits causes them to raise and then fall to their death. On Death, they drop loot which the player can pickup by walking over. Players have to manage resources like health and ammo through the loot. 

Singleton stuff

This project uses two singletons that follow the same implementation format. The first singleton is the GameManager, one of the more common uses of singletons in games. Our GameManager is responsible for controlling the game state and major game functions such as enemy spawning and win/lose conditions. The game benefits from a singleton game manager because many of its functions and variables need to be accessed by multiple scripts really easily. It would be extremely tedious to manually create references in every script that uses it and it would become messy very fast. Additionally there are several scripts that are instantiated on runtime, such as the text commands, that need access to information on the game manager and can’t easily have a reference passed to them. Overall, this implementation simplifies the management of states and game actions significantly and allows for more scripts to easily access those kinds of controls. The baseline functionality of the GameManager had been made during the gamejam, but it lacked the proper instance checking and it did not prevent its destruction. Additionally it contained a lot of functionality that it should not have had as a singleton.

The second implementation of singleton pattern is our UIManager. Much of the code in the UIManager was actually taken out of the Gamemanager to make the code easier to understand and formatted better. The UI manager controls the activation/deactivation of UI elements, Instantiating new UI elements and updating UI. These functions work best as a singleton because they need to be accessed by a wide variety of scripts and keeping all UI-related functions to a single script encourages better encapsulation and makes controlling the elements much simpler. Scripts like PlayerHealth, BubbleGun, GameManager and InputHandler all need to affect UI elements in some way, by keeping all the functionality to a single script it reduces the complexity of the other scripts and simplifies their use. 

Pseuodcode:
—--------------------------------------------------------------------------------------
Both implementations:

Public static singleton Instance;
 awake(){
if(singleton != null)
If(instance != this)
Destroy this

Else 
Instance = this
Dontdestroy on load
}
—--------------------------------------------------------------------------------------
Gamemanager specific functionality:

Update{
Countdown timer
uimanager.updatetimer(timer)
if(timer <= 0) Endgame(“survived”);
}

This function is what triggers the gameplay state change, its triggered by the king rabbits death and by the StartCombat command
Public Void activateSleeperagent(){
UImanagerInstance.gameplayUI()
Start music
Begin spawning enemies
Transition skybox
}

Public void endgame(text){
uimanager.setendgameUI(text)
Turn off player controls
Stop spawning
Turn on mouse controls
Set game state to inactive
}
—--------------------------------------------------------------------------------------
UImanager specific functionality

button populatebindingtext(key, binding name){
Instantiate a new button the text set to the key and a description of the binding name
Return new button
}

Void adjustmagazinecount(int){
Set magazine count text to reflect current number of magazines
}
Void adjusthealth(float healthratio){
Set the healthbar fill to be equal to the current health percentage
}
Several more functions for updating specific UI elements

Toggle UI(){
UI.active = ! UI.state
}

—--------------------------------------------------------------------------------------

Command stuff

I adapted the text parsing code in TextInputHandler from here: https://www.youtube.com/watch?v=usShGWFLvUk along with the idea of making commands scriptable objects. I chose to adapt the text parsing code because I was unsure what the best ways to parse a command name and parameter from text and his implementation was cleaner than anything I could come up with. My command system had already been made before watching the video, but I liked the suggestion of using scriptable objects to make adding and searching through new commands easier so I modified my existing code to use scriptable objects (I just made BaseCommand inherit from scriptableobject rather than nothing).

The two implementations of command pattern are rebindable player controls and console commands for testing. 

The rebindable keys work by storing a list of commands in a dictionary using unity KeyCodes as the key. When a key is pressed, the system checks if there is a key in the dictionary that matches it and if there is it tells the command invoker to execute the associated command. To rebind keys, all you need to do is press the button corresponding to the control you'd like to rebind and the system waits for a second keypress, setting the desired new binding, then it switches the old key for the new key. Command pattern makes this possible by divorcing the input and the player controls allowing any keystroke to represent any action. Being able to rebind controls is an important accessibility option and any project benefits from it. 

The console commands uses the TextInputHandler which takes in a string input by the user into a TMPro_inputfield and breaks down the text into individual components for parsing. First it checks if the command is an actual command by checking if it uses the required prefix, / in this case, and then breaks up the command name and any possible parameters that are separated by spaces. Then it checks if any of the commands in the command array have a command word that matches the command name, if a match is found the parameters are sent to that command and the command is sent to the invoker. This system is not meant to make the systems better for the users, rather they make testing and debugging easier by allowing us, the developers, to shortcut to specific phases of the game or trigger certain scripts for testing purposes. Using a command pattern allows us to make new commands on the fly without having to write the full functionality of every command. 

Both command types inherit from BaseCommand which is a scriptable object. Using the same base class means both implementations share the same invoker. Using Scriptable objects allows us to create lists of commands on scripts that need references to them without hardcoding the references each time a new script is created. This however has a few downsides, the most significant of which is that since they are not instantiating new references it is much more difficult to pass variables to them since there is no constructor. To get around this on TextCommands, I created an abstract function called “SetVariables” that takes the arguments string from the parser. And the movement commands get a reference to the player from the gamemanager.

—--------------------------------------------------------------------------------------
BaseCommand abstract class : scriptableobject{
String command word
Abstract void execute();
Abstract void reverse();
}

—--------------------------------------------------------------------------------------
MovementCommand abstract class: BaseCommand{
PlayerController playercontroller;
KeyCode defaultBinding;
Override execute(){
Playercontroller = gamemanager.getplayer();
}
}

—--------------------------------------------------------------------------------------
Class Forwardcommand: movementcommand{
execute(){
Playercontroller.move(forward)
}
reverse(){
playercontroller.move(-forward)
}

}
—--------------------------------------------------------------------------------------
Textcommand abstract class: basecommand{
Public abstract void SetVar(string[] args);
}

Changehealth class: textcommand{
setvar(args[]){
Healthchangeamount = argos[0] to int

execute(){
playerhealth.takedamage(healthchangeamount)
}
}

}

—--------------------------------------------------------------------------------------

CommandInvoker class{

Public void executecommand(basecommand){
basecommand.execute
}
Public void reversecommand(basecommand){

}
}
—--------------------------------------------------------------------------------------
Playerinputhandler class{
Movementcommand[] commands;
Dictionary <keybinding, command> bindings
IsRebinding = false
Keycode newinput

Update{
If rebinding send pressed key to rebinding
Otherwise
foreach(key in bindings)
If the key is in bindings send the command to the command invoker
}

Public StartRebinding(string ){
IsRebinding = true
}

Private rebindkey(oldkeycode, newkeycode)
If the new keycode isnt already in use
	Change the key in bindings to the new keycode

}

—--------------------------------------------------------------------------------------

TextCommandhandler class{
Textcommands[] commandlist
String prefix

Public processtext(string input){
If it has the prefix continue
Remove prefix
Split string on spaces
Put the first word of string as the commandword
The rest are in a string array for argumments
processtext(commandword, argument string array)
}

Public processtext(string commandword, string[] arguments){
foreach(command in commandlist)
If a commands commandWord is the same as the input commandword
command.setvar(arguments)
Send the command to the command invoker

}

}
—--------------------------------------------------------------------------------------

Observer stuff
The observer design pattern is a super helpful pattern where an object often referred to as the subject, can send out notifications to subscribers to the event when something specific happens. Unity has a built in event system and even uses it for things like their collision system. This pattern can help reduce dependencies between scripts and make it easier to add or remove features later. We are using Observer for two specific events: the player's death and when combat starts. Both of these events trigger lots of effects and state changes, and by using observer, we can quickly add more effects by simply having them subscribing to their respective effects. It allows many things to be added without the code becoming overly messy or complicated. 


Factory stuff
Refer to screenshot 1

Factory was chosen for the rabbit spawning and the pickup spawning for its possibility and ease of use for adding in later spawn options. Right now both just spawn one type of object but in our later development we have plans for multiple enemy types and different types of pickups and factory will help add these different types without having to refactor much of the code. The effects of it is not seen by the player but will shorten the time for us to implement new things. 

Dll stuff

refer to screenshot 2

I used a dll to create a weighted sheet where you can set the drop chances for the pickups. The previous way we handled the these values was through manually setting values in the script. This required recompiling and this new implementation solves that. This also allows us to save multiple weighted chart so we can quickly swap between them for testing. 

FINAL SUBMISSION

Object Pooling

Pseudocode

Public GameObjectPool{
List<object> inactivePool;
Object object to pool;

constructor(object to pool, int num to initialize with){
Object to pool = object to pool
for(num to initialize with){
Instantiate the object to pool and add to inactivepool, set the object to inactive
}

Public object Retrievefrompool(){
If pool is empty instantiate a new object and return it
Else return the first object in the pool and remove it from the pool
}

Public returntopool(object)
{
Adds the object back to the inactive pool
}

}

}

Gamemanager{
GameObjectPool bunnyPool = new Gameobjectpool(enemy prefab, 10)

SpawnEnemy()
{
Enemy = bunnyPool.retrieveFromPool()
Set the position of the found enemy and activate it
}


}

Enemy{
Die(){
Return this to the gameManager bunnyPool and deactivate it
}
}

The generic object pooling script contains a list of gameObjects and a reference to an object prefab that it will be pooling. When an instance of the class is created, its constructor is passed both the prefab and how many it will start with in the pool. This pool was implemented in multiple systems, the key few are enemy spawning, the bubble shooter and gore creation. These systems were chosen because they each create multiple instances of identical prefabs that are subsequently destroyed. Rather than destroy and remake prefabs over and over, pooling them simply deactivates and reactivates the objects, changing parameters as this happens, allowing them to be reused. Reusing gameobjects significantly reduces garbage, improving performance during garbage collection. The pool was not made a mono behaviour allowing it to be reusable to pool all game object types and be fully controlled and instantiated by the scripts that would manage the pools themselves. 

Dirty flag:

Bubble{
Bool isActive;
update(){
If active
Increase life timer
Move bubble forward
if(timer >= lifetime){
Pop bubble
Active = false;
}

}
}

When any script retrieves a bubble from the bubble pool, set pop to false

The IsActive boolean is used as a dirty flag in the Bubble script, preventing several functions from running within the update loop when the bubble is no longer active. As long as the flag is true, the lifetime timer counts up, the motion is calculated and it checks if it has reached its lifespan. The flag is set to false when the lifetime runs out or the bubble begins to pop. This significantly reduces unnecessary calculations and prevents triggers from activating multiple times such as the pop trigger when the timer runs out.

State Pattern:

Enum gamestate{
Menu
Gamestarted
Gameswitch
gameend
}

Gamemanger{
Gamestate currentstate;

sceneswitch(){
Set gamestate to game started
}

Activatecombatmode(){
If the state isnt gamestarted, return
Else set the state to switch and switch the music, scenery, and enemies to hostile
}

update(){
If the state is gameswitch, start the enemy spawning timer and checks
}

Playerdies(){
If the state isnt gameend, set state to game end and start UI populating
}

}

Enemy{
Attack(){
If the game managers state is gameswitch they deal damage to the player
}
}

State pattern is primarily used in the game manager to check what state of gameplay the player has engaged to prevent checks from being triggered twice or from being triggered at the wrong time. Further, it prevents superfluous calculations from being done when the state doesnt require them, such as the timers. The game can be easily divided into two major states, before the theme swap and after the theme swap (before and after king bunny is killed), with a menu state and game over state to further prevent checks. Implementing the pattern in this way removed the need for a significant number of booleans which increased code readability and useability.


Improvements:

We found that there were not many substantial improvements that could be made to our implementations from the progress demo, therefore we elected to make other scene and code improvements.
pickups
Before

<img width="317" height="397" alt="Screenshot 2025-11-26 162443" src="https://github.com/user-attachments/assets/2fd6f07b-c2a9-47e1-9c01-13b83fe766e4" />

After:

<img width="839" height="470" alt="Screenshot 2025-11-26 162540" src="https://github.com/user-attachments/assets/3aaf6e4f-a339-463c-8073-bc95cc61fe1f" />

Instead of instantiating the appropriate icon for each pickup when it is spawned in, it now has all the icons already on the prefab but they are not enabled until it is spawned. This reduces unneeded instantiations and makes the code cleaner. 

audio loading
before

<img width="841" height="442" alt="Screenshot 2025-11-26 162939" src="https://github.com/user-attachments/assets/c8fe3c68-66ba-4056-8904-db28f2520672" />
<img width="288" height="215" alt="Screenshot 2025-11-26 163108" src="https://github.com/user-attachments/assets/0f462e88-cef1-4fea-b019-35e9807671c5" />

after

<img width="868" height="329" alt="Screenshot 2025-11-26 163625" src="https://github.com/user-attachments/assets/383b3132-8caf-4e9c-81a7-1c8192cf4aaa" />
<img width="296" height="228" alt="Screenshot 2025-11-26 163715" src="https://github.com/user-attachments/assets/3d26e55b-08a8-4520-8a27-eabe6bcc0dea" />
<img width="441" height="471" alt="Screenshot 2025-11-26 163739" src="https://github.com/user-attachments/assets/44289d17-0b1d-4226-b29e-a2ec69125510" />

We set the music to load in background rather than when its first used which completely eliminated a lag spike when the music switched. It was difficult to get screenshots of the exact moment, but when the music tried to load originally it took up the main thread which would cause a noticeable freeze which looked really bad.

Profiling:
Pre bloodsplatter object pooling

<img width="833" height="885" alt="Screenshot 2025-11-26 161659" src="https://github.com/user-attachments/assets/bdaeaadd-a030-4d61-a092-983b50c2ff8f" />

post object pooling

<img width="695" height="640" alt="Screenshot 2025-11-26 161326" src="https://github.com/user-attachments/assets/2b6427f3-1514-4b88-9636-a430073ed7c7" />

You will see that after adding object pooling to the bloodsplatter, there are no longer massive spikes in the performance. This is due to the fact that it is no longer trying to instantiate and destroy objects, only changing their status, position and visibility.

Our DLL does not increase performance, instead it makes modifications to the code easier. By creating a list of the possible spawns and their drop chances, you can edit that file rather than recompile the code every time you want to make a change. This significantly reduces the amount of time you are waiting for unity to compile the project. 

We also set the environment meshes to static, split the canvases into a static and updating canvas to reduce draw calls and removed unneeded debug andprint statements from code. All of these improve performance in small ways by reducing draw calls and reducing garbage.

Dynamic batching and GPU instancing were added to the rabbits and bubbles. They are simple meshes and shared between many many objects so we can reduce the load on the GPU by using these techniques. 
