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

---

### Introduction:
Cows and Bulls is a captivating word-guessing game that I developed as a personal project, showcasing my skills in game development, and design. Cows and Bulls offers a fun and intellectually stimulating gameplay experience. The primary objective of the project stemmed from my friend's passion for the game and their request for me to develop it. In this project, I aimed to create a unique approach for displaying cows and bulls results. I came up with the idea of using an invoice machine to showcase the outcomes. I envisioned the invoice machine printing the cows and bulls results, and to my delight, it worked successfully.

### Main Mechanics:
The game relies on generating four numbers within the range of 0 to 9, The numbers should have unique digits, meaning each digit should be different from the other.

To determine whether there is a cow or a bull, I require knowledge of the number's position. A cow indicates that the number is present among the hidden numbers, but its location is incorrect. On the other hand, a bull signifies that the number is both correct and in the correct position.

### Features:

## High Score API:
<img align="left" width="200" height="200" src="Screenshot 2024-01-27 220106.png">
<img align="left" width="200" height="200" src="images/Screenshot 2024-01-27 220254.png">
The end result of the UI, both for saving player name and displaying the high score list.

I utilized the PlayFab API to implement a feature where player names and their high-score data could be saved. Following the completion of the server setup, I incorporated a service manager into Unity, enabling the transmission of requests to the server. This functionality facilitated the storage of match-high scores and the presentation of a comparative list of top scores. Additionally, I added some details that allow players to select random names.

## Share The Result On Social Media:
<img align="left" width="200" height="200" src="images/photo_2024-01-27_22-12-28.jpg">
To enhance the game's interactivity, I Added a feature that enables players to share their results on social media platforms. To facilitate this, I devised a mechanism to capture the entire guessing process, including the results and the player's progress. By creating copies of all the results and combining them into a single camera view for screenshots, I enabled players to include their progress alongside a shared message when posting on social media.

## Some Datils:
<img align="left" width="200" height="200" src="images/Screenshot 2024-01-29 110840.png">
i add some analtics ditals to help players guess easly they can click on spicfic numbe to foucs on it add to type of mark "X" and "O"

