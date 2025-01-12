using System;
using SipOfTea;
using static System.Runtime.InteropServices.JavaScript.JSType;



Starter hellos = new Starter();
Script play = new Script();

Console.ForegroundColor = ConsoleColor.White;
Console.BackgroundColor = ConsoleColor.Black;


// ask
play.printline("You're about to read something not meant for everyone. So I must ask.", 75);
play.print("Password?    ", 100);

string pswd = Console.ReadLine();
int which;
if (pswd == "Holly and Phoenix Feather") { which = 1; } //nofam
else if (pswd == "Willow and Unicorn Hair") { which = 2; } //fam
else { which = 0; Console.WriteLine("Sorry. Bye-bye."); }
for(int i = 0; i < 3; i++) { Console.WriteLine(); }
for (int i = 0; i < 3; i++) { play.print("*** "); }
for (int i = 0; i < 3; i++) { Console.WriteLine(); } 

//int which = 1;


void playgame(int which)
{
    
    // Scene I - hellos
    hellos.startup();

    // Scene II - intro
    play.say("scene_ii", 1);
    play.ask("ask-ii", 1);

    play.say("scene_ii", 2);
    play.ask("ask-ii", 2);

    play.say("scene_ii", 3);
    play.ask("ask-ii", 3);

    play.say("scene_ii", 4);
    play.ask("ask-ii", 4);

    play.say("scene_ii", 5);
    play.ask("ask-ii", 5);

    play.say("scene_ii", 6);
    play.ask("ask-ii", 6); 

    play.say("scene_ii", 7);
    play.ask("ask-ii", 7);

    play.say("scene_ii", 8, 50);
    play.ask("ask-ii", 8, 50);

    play.say("scene_ii", 9, 55);
    play.ask("ask-ii", 9, 55);
    
    play.say("scene_ii", 10, 60);
    play.ask("ask-ii", 10, 60);
    
    play.say("scene_ii", 11, 60);
    play.ask("ask-ii", 11, 60);

    play.say("scene_ii", 12, 60);
    play.ask("ask-ii", 12, 60);
    
    play.say("scene_ii", 13, 60);
    play.ask("ask-ii", 13, 60);
    
    play.say("scene_ii", 14, 60);
    play.ask("ask-ii", 14, 60);
    
    play.say("scene_ii", 15, 60);
    play.ask("ask-ii", 15, 60);

    play.say("scene_ii", 16, 60);
    play.ask("ask-ii", 16, 60);

    if (which == 1) { play.say("scene_ii", 17, 60);  }
    else { play.say("scene_ii", 21, 60); }
    play.ask("ask-ii", 17, 60);
    
    for (int i = 18; i<=19; i++) { play.say("scene_ii", i, 65); play.ask("ask-ii", i, 65); }

    play.say("scene_ii", 20, 65);
}

if ((which == 1) || (which == 2)) { playgame(which); }