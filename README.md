INSTRUCTIONS TO PLAY:<br>
...<br>

This is the code for a basic console conversation engine. It comprises code written in C# (.cs files) and text to read from (.txt files).<br>
It allows the console to read from text files, including adding color, indentation, etc. as the programmer deems fit, and take user input through MCQs (with a variable number of options).<br><br>
The 'story' of this game, here, is a conversation of the player with someone inside the computer called 'Ohio.' It was inspired by the genre of 'visual novels,' which is essentially the same thing, just not in a console. In fact, the motivation was for me to work on an actual visual novel, with artwork and music and all, after my experience with this game.<br><br>
While this repository is tailored for a specific game and set of text files that have been created by me, it won't be tough to modify this for however someone would request.<br>
<br>
The following are the details of each file.<br>
<br>
1. typer.cs<br>
Most of the code relies on this. It defines the following classes-<br>
i. Script: the basic reading-writing code that the console relies on. It's functions are:<br>
a. print : takes user input of text, that it prints with a time-gap of 'timer' between each character (instead of instantly printing the entire text), mostly for dramatic effect.<br>
b. printline: identical to print, but leaves a line after printing. These two were used for texting purposes, for the most part.<br>
c. say : takes arguments for location of text file to read from, 'point' i.e. a location within the text file from where it is required to read from in a given function call, and 'timer' which specifies the amount of time (in microseconds) between each character being printed, mostly used for dramatic/emotional effect.<br>
d. ask : takes similar arguments as say, but the text file in questions has multiple options that the user can choose from by navigating between them using arrow keys.<br>
ii. Colors: This object was created to change the foreground/background color of the console easily within the rest of the program.<br>
a. list : a dictionary mapping numbers to colors. These numbers were used in the text files to specify where the color was to be changed.<br>
b. forecol : takes integer input corresponding to the list mentioned above, and changes foreground color to the corresponding color.<br>
c. backcol : identical to forecol, but for background color.<br>
<br>
2. scene_ii.txt<br>
It contained the text for the consoles 'dialogues'. It's components were:<br>
a. ///n/// : specified the 'point' from which say function would read at a given call (see 1.i.c)<br>
b. # : tells the console to pause for 40 ms for dramatic/emotional effect<br>
c. $ : syntax for changing foreground color. $n$ tells console to change color to color code 'n', while $$ tells console to revert back to default color.<br>
typer.cs was coded accordingly to accomodate all these components.<br>
<br>
3. ask-ii.txt<br>
Similar to scene_ii.txt, except it contained the list of options and corresponding answers for the user to choose from to influence where the game goes.<br>
a. ///n///, #, $ : identical to scene_ii.txt<br>
b. \n\ : specified the list of possible options given to the user to choose from.<br>
c. \n/ : specifies consoles outputs corresponding to each user input.

4. hellos.cs
This is the intro to our program. It involves a series of successively contorted prints of 'Hello, World'. From the narrative point of view, it was meant to show the computer 'malfunctioning', allowing Ohio to talk to the user for the first time.

5. Program.cs
Essentially the 'mother' program. It runs all the functions mentioned above.
