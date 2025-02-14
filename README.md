# ProjectStartUp

A Unity 3D split-screen co-op game, product of a group project, in which Engineers, Designers and Artist come together to develop a game in 3 weeks.

<p align="center">
  <img src="Media/demo.gif"><br/>
  *<i>Low frame rate caused by gif limitations</i>*
</p>

## Overview

A 3D split-screen co-op game, in which there are two main tasks - cooking and hunting, but in Hell :). Players receive orders from clients and they have to fulfill them in time in order to gain valuable points that influence their end score. To do that, firstly, they will need to hunt monsters that if neutralized, drop ingredients used later for preparing the ordered dish. Secondly, once the ingredients have been gathered, players can start cooking them, however they are not normal ingredients - they do not hold back: they act against you in various ways with the sole purpose of slowing the players down from their cooking.

A legendary hunter kills a powerful beast in a remote forest. What appears to be an ordinary animal, is an ancient creature possessed by a dark force, making it a prized possession of Lucifer. The hunter, unaware of its significance, takes the creature’s meat to his friend, a renowned chef, to create a unique dish. As they prepare the meal, the kitchen shakes violently. The ground cracks open, and they are both dragged into the fiery depths of Hell. A voice echoes around them. “I am Lucifer. You’ve killed my prized possession. For this, you must face the consequences.” Lucifer offers them a deal: if they can create dishes worthy of each of Hell’s circles, they may escape. The hunter must track and defeat Hell’s most fearless and warmonger monsters to gather cursed ingredients, while the chef transforms them into exquisite meals. Only by defeating each circle’s boss with a perfect dish can they move forward. But the final challenge is no other than Lucifer himself. Only a meal truly worthy of the Devil will grant them freedom.

## Features

### Gameplay (Engineers)

- **Two-player split-screen co-op mechanic:** Utilizing the new Unity Input System for handling both keyboard and controller keybindings, as well as taking care of the split-screen functionality
- **Role-switching:** Each player can switch between being a cook and a chef, done by taking advantage of the Strategy Design Pattern
- **Cooking/Hunting Equipment (Knife, Oven Mitts, Pan):** Again, by using the Strategy Pattern, each equipment can easily swap between its cook and hunter functionality depending on the role of the player currently using it
- **Monsters' behaviours:** Three unique monsters, each affecting the player in a different way:
  1. River monster: tries to lure the player towards the lava river by applying gravity pull force towards it; The Player needs to throw a knife in a boomerang fashion and upon successful hit, collect the dropped ingredients; If having stayed too long in the lava river, the Player gets hurt and loses an item from his inventory
  2. Graveyard zombies: the Player has to dig out graves to try and find necessary ingredients; However there is a chance he stumbles upon a zombie that grabs him fiercely, fow which he would have to use oven mitts to free himself; If having been grabbed for too long, again, the Player gets hurt and loses an item from his inventory
  3. Wingless demon: a patroling creature, waiting to detect the Player to start shooting away projectiles, which closely follow the Player until they hit or get reflected with the Pan; If reflected, they fly in the direction of the Player's sight with the possibility of colliding with the demon and making it drop the corresponding ingredients; If the Player gets hit by the projectile, again, he gets hurt and loses an item from his inventory
- **Inventory system:** The two players have a shared inventory which they both can operate and manipulate in the following ways:
  1. Items can be picked up and put in the slots of the inventory until full
  2. Items can be selected from the inventory and brought out in the given Player's hand to use, meaning only one Player can use a given item at a time.
  3. Items in hand can be swapped with other items from the inventory
  4. Ingredients in hand can be placed on cooking slots to start their preparation for cooking a given dish
  5. Items in hand can be dropped on the floor or put back in an empty inventory slot
- **Hostile item behaviours:** Evil ingredients start acting against the Player when put on cooking slots, slowing him down from cooking:
  1. Blood: gets spilled all over the place, making it slippery for the Player and hard to control
  2. Beef and Limbs: starts running around the kitchen, making the Player have to catch and return it to continue cooking the dish
  3. Wings: periodically slap the Player, pushing him away from the cooking spot and making it difficult for him to cook
- **Cooking:** There are three different cooking spots that require different equipment to cook dishes; Along with that, there are various recipes that Players need to follow strictly to successfully complete a dish
- **Orders:** Randomised orders from clients that require the Players to cook a given dish, for a given time and recieve points for doing so, which adds up to a final score displayed after all dishes have been completed or their time ran out

### 3D Models and VFX (Artists)

- **Characters, Monsters, Ingredients, Dishes**
- **Environment and VFX:** Arena, Kitchen, Particle effects

### Game Design, UI/UX, Music and SFX (Designers)

- **Game HUD, Controls showcase**
- **Sounds**
  1. Background sounds
  2. Sound effects

## Models and VFX
Done by: [Ivana Slavova](https://www.linkedin.com/in/ivana-slavova-2162442ba/), [Kristiyan Karaisaev](https://www.linkedin.com/in/kristiyan-karaisaev-979471258/)

## Game/UI Design and Audio
Done by: [Alexander Marchev](https://www.linkedin.com/in/alexander-marchev-75833733b/), [Alice Leonard Widodo](https://www.linkedin.com/in/alice-leonard-widodo-1755b62ba/)
