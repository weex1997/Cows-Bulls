<!-- PROJECT LOGO -->
<div>
<h3><img align="left" width="200" height="200" src="images/f11805456d9f3d08.png"> <br/> Cows & Bulls - Guessing Number Game
</div>   
<a href="https://www.youtube.com/watch?v=E1-fTTuxCIU">Game Overview</a> 
  <br/> <a href="https://www.youtube.com/watch?v=WN8-BFGJ8NA">How to create Lineups</a><br/> </h3>   
  <br/>
<br/>
  <br/>
<br/>

</div>   

###Introduction:
Cows and Bulls is a captivating word-guessing game that I developed as a personal project, showcasing my skills in game development, and design. Cows and Bulls offers a fun and intellectually stimulating gameplay experience. The primary objective of the project stemmed from my friend's passion for the game and their request for me to develop it. In this project, I aimed to create a unique approach for displaying cows and bulls results. I came up with the idea of using an invoice machine to showcase the outcomes. I envisioned the invoice machine printing the cows and bulls results, and to my delight, it worked successfully.

##Main Mechanics:
The game relies on generating four numbers within the range of 0 to 9, The numbers should have unique digits, meaning each digit should be different from the other.

To determine whether there is a cow or a bull, I require knowledge of the number's position. A cow indicates that the number is present among the hidden numbers, but its location is incorrect. On the other hand, a bull signifies that the number is both correct and in the correct position.

### High Score API:
I utilized the PlayFab API to implement a feature where player names and their high-score data could be saved. Following the completion of the server setup, I incorporated a service manager into Unity, enabling the transmission of requests to the server. This functionality facilitated the storage of match-high scores and the presentation of a comparative list of top scores. Additionally, I added some details that allow players to select random names.
```
- Unity Version: 6000.0.2f1
- ECS Version: 1.2.1
```

<a href="Documentation/Battle Simulator-wedad.pdf">Technical Documentation</a>
