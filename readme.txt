LevyWatch AKA Noob Radar
Created by William Moodhe / JetBoom
williammoodhe@gmail.com
http://www.github.com/jetboom

As of Oct. 2015, a patch has been released for the game that removes the Levy percentages on the world map (the fault of which is mostly this program). Only players in the clan that owns them can see the percentages, making this program not very useful.

You can grab pre-built releases at: http://www.noxiousnet.com/jetboom/levywatch/releases/


This is a program that keeps track of the Levys in Darkfall Unholy Wars.
It alerts you when levys go up and approximates how popular they are, thus giving you a general idea of where people may be in the wild.
In order to avoid reading game memory, the program uses optical character recognition software (google it) to convert images in to text, which the program can then turn in to levy percentages.

The number on the very right of each levy line is the popularity. This is the percentage increase since the start of the program. Relative popularity determines the color of the levy, making it easy to see which ones have people at them. Popularity decays over time (default 180 seconds).

See config.cfg for program settings.

THIS PROGRAM DOES _NOT_ READ OR WRITE TO ANY MEMORY OF THE GAME OR READ OR WRITE TO ANY GAME FILES.
IT MERELY TAKES AND FEEDS PREPROCESSED SCREENSHOTS TO AN OPTICAL CHARACTER RECOGNITION PROGRAM.
THE "MOST" IT DOES IS READ THE ACTIVE WINDOW NAME TO MAKE SURE YOU'RE TABBED IN TO THE GAME, SOMETHING PEOPLE DO IN AUTOHOTKEY SCRIPTS.
THIS PROGRAM DOES NOT VIOLATE THE EULA BUT I DO NOT TAKE ANY RESPONSIBILITY FOR BANNED ACCOUNTS.


REQUIREMENTS:
- tesseract-ocr
  You probably want the windows installer from https://code.google.com/p/tesseract-ocr/downloads/list
  It must be installed so that it's added to the PATH
- .NET Framework 4.5
  You probably have this already. Otherwise google it.

GLOBAL HOTKEYS:
CTRL + F1			-		Start / Stop
CTRL + F2			-		Pause / Show mouse coords
CTRL + F3			-		Copy coords to clipboard
CTRL + F4			-		Close
CTRL + F5			-		Disable all levys with no popularity.
CTRL + F6			-		Enable all levys.

SOUND QUES:
Single beep			-		Levy went up.
Double beep			-		Levy went up and has relatively hot popularity.
Single tone			-		Levy was requisitioned.

COLORS:
White				-		No increase.
Black				-		Failed to read OCR last check and cannot currently track.
Purple (percentage)	-		Levy is full. Cannot track popularity for this levy until it is requisitioned.
Dull blue			-		AOI has a relatively very slow increase. Probably is a harvester.
Green				-		AOI may have people (relatively slow increase).
Yellow				-		AOI most likely has people (relatively good increase).
Red					-		AOI almost definitely has people (relatively large increase).

TO USE, YOU MUST BE:
- On the map screen.
- Fully zoomed out.
- Disable monster spawns, political map, and chat (it blocks the character reader).
- You currently must be in borderless window at 1920x1080 resolution - nothing else will work.

BUGS:
- The program does not know if you are properly zoomed out or even on the map screen. It merely assumes.
- Occasionally, OCR will be unable to read percentages if there is too much noise or you have party members in the zone blocking the text. I've tried to preprocess the image fed to OCR as much as possible to remove noise but for the most part it works 99% of the time.

COMPILING:
Shouldn't require anything out of the ordinary and compiles fine in stock Visual Studio 2012
